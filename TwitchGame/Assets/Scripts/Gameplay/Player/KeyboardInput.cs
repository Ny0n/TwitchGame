using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    [System.Serializable]
    private class Players
    {
        public string name;
        public KeyCode REGISTER;
        public KeyCode UNREGISTER;
        public KeyCode UP;
        public KeyCode DOWN;
        public KeyCode LEFT;
        public KeyCode RIGHT;
        // new commands...
    }
    [SerializeField]
    private Players[] players; // Create keyboard players in the inspector

    private Dictionary<string, Dictionary<Enums.CommandType, KeyCode>> playersInputs;

    void Start()
    {
        playersInputs = new Dictionary<string, Dictionary<Enums.CommandType, KeyCode>>();
        foreach (var player in players)
        {
            if (player.name == "" || playersInputs.ContainsKey(player.name))
            {
                Debug.LogWarning("Warning: Invalid keyboard player name (must be unique and have at least one character)");
                continue;
            }

            playersInputs[player.name] = new Dictionary<Enums.CommandType, KeyCode>
            {
                [Enums.CommandType.REGISTER] = player.REGISTER,
                [Enums.CommandType.UNREGISTER] = player.UNREGISTER,
                [Enums.CommandType.UP] = player.UP,
                [Enums.CommandType.DOWN] = player.DOWN,
                [Enums.CommandType.LEFT] = player.LEFT,
                [Enums.CommandType.RIGHT] = player.RIGHT,
                // new commands...
            };
        }
    }

    void Update()
    {
        foreach (var playerInputs in playersInputs)
        {
            ManagePlayer(playerInputs.Key, playerInputs.Value);
        }
    }

    void ManagePlayer(string name, Dictionary<Enums.CommandType, KeyCode> inputs)
    {
        foreach (var entry in Enums.Commands)
        {
            if (Input.GetKeyDown(inputs[entry.Key]))
            {
                CommandManager.Instance.ProcessCommand(new Command(name, entry.Key));
                return;
            }
        }
    }
}
