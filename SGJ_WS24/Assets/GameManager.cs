using System;
using System.Collections;
using System.Collections.Generic;
using FindingMemo.Neurons;
using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private EventReference music;
    public FMOD.Studio.EventInstance musicPlayEvent;
    public MoveDown moveDown;

    private void Start()
    {
        FMOD.Studio.EventDescription des;

        musicPlayEvent.getDescription(out des);

        des.loadSampleData();

        musicPlayEvent = RuntimeManager.CreateInstance(music);


        BeatTrackerr.fixedBeatUpdate += moveDown.StartScrolling;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            StartGame();
        }
    }


    [ContextMenu("Start scrolling")]
    public void StartGame()
    {
        BeatTrackerr.instance.StartMusic();
        
        //moveDown.StartScrolling();
    }
}
