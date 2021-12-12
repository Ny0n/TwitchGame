using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(TwitchInterpreter))]
public class TwitchClient : MonoBehaviour
{
    public bool Connected => _tcpClient is {Connected: true};
    
    [Header("Twitch connection")]
    [SerializeField] private string _username;
    [SerializeField] private string _password;
    [SerializeField] private string _channelName;
    [SerializeField] private KeyCode _toggleConnectionKeybind = KeyCode.T;
    
    [Header("Default messages")]
    [SerializeField] private string _connectionMessage = "<Chat connected to the game>";
    [SerializeField] private string _disconnectionMessage = "<Chat disconnected from the game>";

    private TcpClient _tcpClient;
    private StreamReader _streamReader;
    private StreamWriter _streamWriter;
    
    private CancellationTokenSource _tokenSource;
    private CancellationToken _token;
    
    private TwitchInterpreter _twitchInterpreter;

    private void Start() => _twitchInterpreter = GetComponent<TwitchInterpreter>();
    private void OnDisable() => DisconnectAsync();

    private bool _crashRequested; // false by default
    private void Update()
    {
        if (Input.GetKeyDown(_toggleConnectionKeybind))
            ToggleConnection();

        if (Input.GetKeyDown(KeyCode.C))
            _crashRequested = true;
    }

    #region Twitch Connection

    private void ToggleConnection()
    {
        if (!Connected)
            ConnectAsync();
        else
            DisconnectAsync();
    }

    private async void ConnectAsync()
    {
        if (Connected) return;
        
        _tcpClient = new TcpClient();
        await _tcpClient.ConnectAsync("irc.chat.twitch.tv", 6667);
        
        _streamReader = new StreamReader(_tcpClient.GetStream());
        _streamWriter = new StreamWriter(_tcpClient.GetStream());

        await _streamWriter.WriteLineAsync("PASS " + _password);
        await _streamWriter.WriteLineAsync("NICK " + _username);
        await _streamWriter.WriteLineAsync("USER " + _username + " 8 * :" + _username);
        await _streamWriter.WriteLineAsync("JOIN #" + _channelName);
        await _streamWriter.FlushAsync();
        
        StartThreadReadChat();
        
        await SendChatMessageAsync(_connectionMessage);
    }
    
    private async void DisconnectAsync()
    {
        if (!Connected) return;
        
        _tokenSource?.Cancel();
        
        await SendChatMessageAsync(_disconnectionMessage);
        
        _streamReader?.Close();
        _streamWriter?.Close();
        _tcpClient?.Close();
    }

    #endregion
    
    #region Read Chat Messages (Thread)

    private async void StartThreadReadChat()
    {
        _tokenSource = new CancellationTokenSource();
        _token = _tokenSource.Token;

        while (true) // if the thread ever crashes, we restart it
        {
            try
            {
                await Task.Run(ReadChatThread, _token);
            }
            catch
            {
                Debug.LogWarning("Twitch chat reader thread crashed, restarting...");
            }
            
            if (_tokenSource.IsCancellationRequested)
                break; // unless we manually requested the cancellation
        }
    }
    
    private void ReadChatThread() // thread func
    {
        while (!_tokenSource.IsCancellationRequested)
        {
            ReadChat();
            Debug.Log("read");
            
            if (_crashRequested)
            {
                _crashRequested = false;
                throw new ThreadInterruptedException();
            }
        }
    }

    private void ReadChat()
    {
        if (_tcpClient?.Available > 0)
        {
            var message = _streamReader?.ReadLine();
            if (message != null && message.Contains("PRIVMSG"))
            {
                var splitPoint = message.IndexOf("!", 1);
                var chatName = message.Substring(0, splitPoint);
                chatName = chatName.Substring(1);

                splitPoint = message.IndexOf(":", 1);
                message = message.Substring(splitPoint + 1);

                NewMessage(chatName, message);
            }
        }
    }
    
    private void NewMessage(string playerName, string message)
    {
        _twitchInterpreter.Interpret(playerName, message);
    }
    
    #endregion

    #region Send Chat Messages

    private async Task SendChatMessageAsync(string message)
    {
        if (!Connected) return;
        
        var format = ":" + _username + "!" + _username + "@" + _username + ".tmi.twitch.tv PRIVMSG #" + _channelName + " :";
        
        try
        {
            await _streamWriter.WriteLineAsync(format + message);
            await _streamWriter.FlushAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    #endregion
}
