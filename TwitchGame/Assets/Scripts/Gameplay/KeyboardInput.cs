using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    public string player1Name = "player1";
    public string player2Name = "player2";
    public string player3Name = "player3";

    Dictionary<string, KeyCode> player1Inputs;
    Dictionary<string, KeyCode> player2Inputs;
    Dictionary<string, KeyCode> player3Inputs;

    // Start is called before the first frame update
    void Start()
    {
        player1Inputs = new Dictionary<string, KeyCode>();
        player1Inputs["REGISTER"] = KeyCode.E;
        player1Inputs["LEAVE"] = KeyCode.A;
        player1Inputs["UP"] = KeyCode.Z;
        player1Inputs["DOWN"] = KeyCode.S;
        player1Inputs["LEFT"] = KeyCode.Q;
        player1Inputs["RIGHT"] = KeyCode.D;

        player2Inputs = new Dictionary<string, KeyCode>();
        player2Inputs["REGISTER"] = KeyCode.P;
        player2Inputs["LEAVE"] = KeyCode.I;
        player2Inputs["UP"] = KeyCode.O;
        player2Inputs["DOWN"] = KeyCode.L;
        player2Inputs["LEFT"] = KeyCode.K;
        player2Inputs["RIGHT"] = KeyCode.M;

        player3Inputs = new Dictionary<string, KeyCode>();
        player3Inputs["REGISTER"] = KeyCode.Keypad9;
        player3Inputs["LEAVE"] = KeyCode.Keypad7;
        player3Inputs["UP"] = KeyCode.Keypad8;
        player3Inputs["DOWN"] = KeyCode.Keypad5;
        player3Inputs["LEFT"] = KeyCode.Keypad4;
        player3Inputs["RIGHT"] = KeyCode.Keypad6;
    }

    // Update is called once per frame
    void Update()
    {
        ManagePlayer(player1Name, player1Inputs);
        ManagePlayer(player2Name, player2Inputs);
        ManagePlayer(player3Name, player3Inputs);
    }

    void ManagePlayer(string name, Dictionary<string, KeyCode> inputs)
    {
        if (Input.GetKeyDown(inputs["REGISTER"]))
            CommandManager.Instance.ProcessCommand(name, Enums.Command.REGISTER);

        if (Input.GetKeyDown(inputs["LEAVE"]))
            CommandManager.Instance.ProcessCommand(name, Enums.Command.UNREGISTER);

        if (Input.GetKeyDown(inputs["UP"]))
            CommandManager.Instance.ProcessCommand(name, Enums.Command.UP);

        else if (Input.GetKeyDown(inputs["DOWN"]))
            CommandManager.Instance.ProcessCommand(name, Enums.Command.DOWN);

        else if (Input.GetKeyDown(inputs["LEFT"]))
            CommandManager.Instance.ProcessCommand(name, Enums.Command.LEFT);

        else if (Input.GetKeyDown(inputs["RIGHT"]))
            CommandManager.Instance.ProcessCommand(name, Enums.Command.RIGHT);
    }
}
