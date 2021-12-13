using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Command/Keyboard/PlayerCommands", fileName = "_P_ Keyboard -Commands-")]
public class PlayerKeyboardCommands : ScriptableObject
{
    [SerializeField] private List<PlayerKeyboardCommand> _playerKeyboardCommands;

    public List<PlayerKeyboardCommand> Value => _playerKeyboardCommands;
}
