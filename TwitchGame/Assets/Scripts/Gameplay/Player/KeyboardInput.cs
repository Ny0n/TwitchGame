using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    [System.Serializable]
    public struct InputCommand
    {
        public Command command;
        public KeyCode key;
    }

    [System.Serializable]
    public struct PlayerInputs
    {
        public string playerName;
        public List<InputCommand> inputCommands;

    }

    [SerializeField] private List<PlayerInputs> players; // Create keyboard players in the inspector

    void Update()
    {
        foreach (PlayerInputs playerInputs in players)
        {
            ManagePlayer(playerInputs.playerName, playerInputs.inputCommands);
        }
    }

    void ManagePlayer(string playerName, List<InputCommand> inputCommands)
    {
        foreach (InputCommand inputCommand in inputCommands)
        {
            if (Input.GetKeyDown(inputCommand.key))
            {
                CommandManager.Instance.AddCommand(new CommandObject(playerName, inputCommand.command));
                return;
            }
        }
    }
}
