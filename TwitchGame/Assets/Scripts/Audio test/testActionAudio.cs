using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testActionAudio : MonoBehaviour
{
    [SerializeField]
    private GameEvent _testEventAudio;

    private bool _soundCanPlay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_soundCanPlay == true)
        {
            //raise event
            _testEventAudio.Raise();


            _soundCanPlay = false;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            _soundCanPlay = true;
        }
    }
}
