using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class KeyboardInterpreter : MonoBehaviour
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

    [FormerlySerializedAs("players")] [SerializeField]
    private List<PlayerInputs> _players; // Create keyboard players in the inspector

    private void Update()
    {
        foreach (PlayerInputs playerInputs in _players)
            ManagePlayer(playerInputs.playerName, playerInputs.inputCommands);
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
