using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCallerObject : MonoBehaviour
{
    [SerializeField]
    AudioClip testClip;

    bool m_Play;
    bool startPlay;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(startPlay == true) 
        {


            startPlay = false;
        }
    }

    void OnGUI()
    {
        //Switch this toggle to activate and deactivate the parent GameObject
        m_Play = GUI.Toggle(new Rect(100, 100, 100, 30), m_Play, "Fire");

        //Detect if there is a change with the toggle
        if (GUI.changed)
        {
            SoundPlayer._instance.onCallPlayClip(testClip);
        }
    }
}
