using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using JetBrains.Annotations;
using UnityEngine;

public class GoogleSheetClient : MySingleton<GoogleSheetClient>
{
    protected override bool DoDestroyOnLoad { get; set; } = false;
    
    [SerializeField] private ScriptablePlayersList _playersList;
    [SerializeField] private ScriptableCooldown _writeCooldown;
    
    [SerializeField] private string[] _scopes = {SheetsService.Scope.Spreadsheets};
    
    [SerializeField] private string _spreadsheetId = "1tUrHBurOsWIEmsvu6oldKx5VdQEWwigQ9ujWQm0XPXk";
    
    [SerializeField] private string _credentialsPath = "GoogleSheets";
    [SerializeField] private string _credentialsFile = "credentials.json";
    
    [SerializeField] private string _playersRange = "A2:A51";
    [SerializeField] private string _maxPlayersCell = "D2";
    [SerializeField] private string _roundDurationCell = "D3";
    [SerializeField] private int _defaultTimeout = 4000;

    private readonly List<string> _empty = new List<string>() // could be better, but i don't have enough time
    {
        "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
        "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
        "", "", "", "", "", "", "", "", "", "",
    };

    [field: SerializeField] private bool Connected { get; set; }

    private static SheetsService _service;

    private int _lastPlayersCount = -1;

    private void Start()
    {
        Init();
    }

    private bool _enabled;

    private void OnEnable()
    {
        _enabled = true;
        _playersList.Players.ValueChanged += OnPlayersUpdated;
    }

    private void OnDisable()
    {
        if (_enabled) // bc it's a singleton
            _playersList.Players.ValueChanged -= OnPlayersUpdated;
    }

    private void OnPlayersUpdated() // C# event
    {
        WritePlayers();
    }
    
    // *********************** GOOGLE *********************** //

    [ContextMenu("Connect")]
    public async void Init()
    {
        Debug.Log("Connecting to GoogleSheets...");
        await ConnectAsync();
        Debug.Log("Connected to GoogleSheets!");
        WritePlayers();
    }

    [ContextMenu("Test Read")]
    public async Task<(int maxPlayers, float roundDuration)> ReadGameSettings()
    {
        int.TryParse(await ReadAsync(_maxPlayersCell), out int maxPlayers);
        float.TryParse(await ReadAsync(_roundDurationCell), out float roundDuration);

        return (maxPlayers, roundDuration);
    }
    
    [ContextMenu("Test Write")]
    public async void WritePlayers()
    {
        List<string> names = _playersList.GetNamesList();

        if (_lastPlayersCount == names.Count)
            return;
        _lastPlayersCount = names.Count;
        
        if (!_writeCooldown.IsCooldownDone())
            return;
        _writeCooldown.StartCooldown();
        
        await WriteAsync(_playersRange, _empty);
        await WriteAsync(_playersRange, names);
    }

    #region Connection

    private async Task ConnectAsync()
    {
        string credentialsDirectoryPath = Path.Combine(Application.streamingAssetsPath, _credentialsPath);
        string credentialsPath = Path.Combine(credentialsDirectoryPath, _credentialsFile);

        UserCredential credential;
        GoogleClientSecrets secrets;

        Debug.Log("Reading credentials...");
        using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
        {
            secrets = await GoogleClientSecrets.FromStreamAsync(stream);
        }

        string tokenPath = Path.Combine(Application.persistentDataPath, _credentialsPath);

        Debug.Log("Opening connection...");
        credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
            secrets.Secrets,
            _scopes,
            "user",
            CancellationToken.None,
            new FileDataStore(tokenPath, true));

        Debug.Log($"Credential token saved to: {tokenPath}");
        
        // create the service to read/write
        _service = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential
        });

        Connected = true;
    }

    #endregion

    #region Read
    
    [ItemCanBeNull]
    public async Task<string> ReadAsync(string range)
    {
        string? value = await GetCellData(_spreadsheetId, range, _defaultTimeout);
        if (value != null)
        {
            Debug.Log($"Read: {range} = {value}");
            return value;
        }
        else
        {
            Debug.Log($"Read: nothing");
            return null;
        }
    }

    public async Task<string?> GetCellData(string spreadsheetID, string range, int timeout)
    {
        if (!await AwaitForConnection(timeout))
        {
            return null;
        }
        
        var request = _service.Spreadsheets.Values.Get(spreadsheetID, range);

        ValueRange response = await request.ExecuteAsync();

        IList<IList<object>> values = response.Values;

        if (values != null && values.Count > 0 && values[0].Count > 0)
        {
            return values[0][0].ToString();
        }
        else
        {
            Debug.Log("No data found in this range.");
            return null;
        }
    }

    #endregion

    #region Write

    public async Task<bool> WriteAsync(string range, List<string> names)
    {
        // await WriteLineData(_spreadsheetId, range, _defaultTimeout, names);
        bool success = await WriteColumnData(_spreadsheetId, range, _defaultTimeout, names);
        if (success)
        {
            Debug.Log($"Write: Success");
        }
        else
        {
            Debug.Log($"Write: failure");
        }

        return success;
    }

    private async Task<bool> WriteData(bool line, string spreadsheetID, string range, int timeout, List<string> data)
    {
        if (!await AwaitForConnection(timeout))
        {
            return false;
        }
        
        IList<IList<object>> values = new List<IList<object>>();

        if (line)
        {
            IList<object> list = data.Cast<object>().ToList();
            values.Add(list);
        }
        else
        {
            foreach (var str in data)
            {
                IList<object> list = new List<object>();
                list.Add(str);
                values.Add(list);
            }
        }

        ValueRange valueRange = new ValueRange();
        valueRange.Values = values;

        var valueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
        var request = _service.Spreadsheets.Values.Update(valueRange, spreadsheetID, range);
        request.ValueInputOption = valueInputOption;

        UpdateValuesResponse response = await request.ExecuteAsync();
        return true;
    }
    
    public async Task<bool> WriteLineData(string spreadsheetID, string range, int timeout, List<string> data)
    {
        return await WriteData(true, spreadsheetID, range, timeout, data);
    }
    
    public async Task<bool> WriteColumnData(string spreadsheetID, string range, int timeout, List<string> data)
    {
        return await WriteData(false, spreadsheetID, range, timeout, data);
    }

    #endregion

    private async Task<bool> AwaitForConnection(int timeout)
    {
        CancellationTokenSource tokenSource = new CancellationTokenSource(timeout);

        while (!Connected)
        {
            await Task.Delay(16);
            if (tokenSource.Token.IsCancellationRequested)
            {
                Debug.LogWarning("SheetsService connection timeout");
                return false;
            }
        }

        return true;
    }
}
