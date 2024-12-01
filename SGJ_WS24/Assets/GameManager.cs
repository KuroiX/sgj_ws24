using System;
using System.Collections.Generic;
using FindingMemo;
using FindingMemo.Neurons;
using FindingMemo.Player;
using FMODUnity;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action<GameState> OnGameStateChanged;

    [SerializeField] private HitNeurons hitNeurons;

    public GameState GameState
    {
        get => _gameState;
        set
        {
            _gameState = value;
            OnGameStateChanged?.Invoke(_gameState);
        }
    }
    
    private GameState _gameState = GameState.Waiting;
    
    [SerializeField]
    private EventReference music;
    public FMOD.Studio.EventInstance musicPlayEvent;
    public List<MoveDown> moveDown;

    private void OnEnable()
    {
        foreach (var down in moveDown)
        {
            BeatTrackerr.fixedBeatUpdate += down.StartScrolling;
        }
        
        hitNeurons.OnHitted += OnHitted;
    }

    private void OnHitted()
    {
        if (_gameState != GameState.Waiting) return;
        
        GameState = GameState.Playing;
        StartGame();
    }

    private void OnDisable()
    {
        foreach (var down in moveDown)
        {
            BeatTrackerr.fixedBeatUpdate -= down.StartScrolling;
        }
        
        hitNeurons.OnHitted -= OnHitted;
    }

    /*
    private void Start()
    {
        FMOD.Studio.EventDescription des;

        musicPlayEvent.getDescription(out des);

        des.loadSampleData();

        musicPlayEvent = RuntimeManager.CreateInstance(music);


        foreach (var down in moveDown)
        {
            BeatTrackerr.fixedBeatUpdate += down.StartScrolling;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            StartGame();
        }
    }
    */


    [ContextMenu("Start scrolling")]
    public void StartGame()
    {
        BeatTrackerr.instance.StartMusic();
        
        //moveDown.StartScrolling();
    }
}
