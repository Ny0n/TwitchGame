using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Timers;
using UnityEngine;
using System.ComponentModel; 

[RequireComponent(typeof(TwitchInterpreter))]
public class TwitchConnexion : MonoBehaviour
{
    private TcpClient twitchClient;
    private StreamReader reader;
    private StreamWriter writer;

    public string username, password, channelName;

    private TwitchInterpreter _twitchInterpreter;

    // Start is called before the first frame update
    void Start()
    {
        _twitchInterpreter = GetComponent<TwitchInterpreter>();
        Connect();
    }

    // Update is called once per frame
    void Update()
    {
        if (!twitchClient.Connected)
            Connect();
        
        ReadChat();
    }

    private void Connect()
    {
        twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);
        reader = new StreamReader(twitchClient.GetStream());
        writer = new StreamWriter(twitchClient.GetStream());

        writer.WriteLine("PASS " + password);
        writer.WriteLine("NICK " + username);
        writer.WriteLine("USER " + username + " 8 * :" + username);
        writer.WriteLine("JOIN #" + channelName);
        writer.Flush(); 
    }

    private void ReadChat()
    {
        if (twitchClient.Available > 0)
        {
            var message = reader.ReadLine();
            if (message.Contains("PRIVMSG"))
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

    private void SendIrcMessage(string message)
    {
        try
        {
            writer.WriteLine(message);
            writer.Flush();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void SendPublicChatMessage(string message)
    {
        try
        {
            SendIrcMessage(":" + username + "!" + username + "@" + username + ".tmi.twitch.tv PRIVMSG #" + channelName +
                           " :" + message);
        }
        catch ( Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
