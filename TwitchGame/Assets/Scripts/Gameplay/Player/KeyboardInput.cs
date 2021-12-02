using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    [System.Serializable]
    private struct InputCommand
    {
        public Command command;
        public KeyCode key;
    }

    [System.Serializable]
    private struct PlayerInputs
    {
        public string playerName;
        public List<InputCommand> inputCommands;
    }

    [SerializeField] private List<PlayerInputs> players; // Create keyboard players in the inspector

    private void Update()
    {
        foreach (PlayerInputs playerInputs in players)
        {
            ManagePlayer(playerInputs.playerName, playerInputs.inputCommands);
        }
    }

    private void ManagePlayer(string playerName, List<InputCommand> inputCommands)
    {
        foreach (var inputCommand in inputCommands.Where(inputCommand => Input.GetKeyDown(inputCommand.key)))
        {
            CommandManager.Instance.AddCommand(new CommandObject(playerName, inputCommand.command));
            return;
        }
    }
}
