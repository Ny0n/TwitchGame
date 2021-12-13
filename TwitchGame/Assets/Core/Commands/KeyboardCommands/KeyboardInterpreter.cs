using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeyboardInterpreter : MonoBehaviour
{
    [System.Serializable]
    private struct PlayerInputs
    {
        public string playerName;
        public PlayerKeyboardCommands keyboardCommands;
    }

    [SerializeField] private List<PlayerInputs> _players; // Create keyboard players in the inspector

    private void Update()
    {
        foreach (PlayerInputs playerInputs in _players)
            ManagePlayer(playerInputs.playerName, playerInputs.keyboardCommands);
    }

    private void ManagePlayer(string playerName, PlayerKeyboardCommands keyboardCommands)
    {
        foreach (var keyboardCommand in keyboardCommands.Value.Where(keyboardCommand => keyboardCommand.Find()))
        {
            CommandManager.Instance.AddCommand(new GameCommandObject(playerName, keyboardCommand.GetGameCommand()));
            return;
        }
    }
}
