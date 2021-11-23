using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    public string player1Name = "player1";
    public string player2Name = "player2";

    Dictionary<string, KeyCode> player1Inputs;
    Dictionary<string, KeyCode> player2Inputs;

    private void Awake()
    {
        player1Inputs = new Dictionary<string, KeyCode>();
        player2Inputs = new Dictionary<string, KeyCode>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player1Inputs["REGISTER"] = KeyCode.E;
        player1Inputs["LEAVE"] = KeyCode.A;
        player1Inputs["UP"] = KeyCode.Z;
        player1Inputs["DOWN"] = KeyCode.S;
        player1Inputs["LEFT"] = KeyCode.Q;
        player1Inputs["RIGHT"] = KeyCode.D;
        
        player2Inputs["REGISTER"] = KeyCode.P;
        player2Inputs["LEAVE"] = KeyCode.I;
        player2Inputs["UP"] = KeyCode.O;
        player2Inputs["DOWN"] = KeyCode.L;
        player2Inputs["LEFT"] = KeyCode.K;
        player2Inputs["RIGHT"] = KeyCode.M;
    }

    // Update is called once per frame
    void Update()
    {
        ManagePlayer(player1Name, player1Inputs);
        ManagePlayer(player2Name, player2Inputs);
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
