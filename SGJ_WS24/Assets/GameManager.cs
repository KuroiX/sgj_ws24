
using System;
using System.Collections;
using System.Collections.Generic;
using FindingMemo;
using FindingMemo.CutScenes;
using FindingMemo.Neurons;
using FindingMemo.Player;
using FMODUnity;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action<GameState> OnGameStateChanged;

    [SerializeField] private HitNeurons hitNeurons;

    [SerializeField] private GameObject spinner;
    [SerializeField] private PlayOutroCutScene playOutroCutScene;
    [SerializeField] private Score scoreManager;

    [SerializeField] private int threshold;
    

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
        hitNeurons.OnHitted += OnHitted;
        OnGameStateChanged += StartSpinner;
    }

    private void Start()
    {
        foreach (var down in moveDown)
        {
            BeatTrackerr.fixedBeatUpdate += down.StartScrolling;
        }
    }

    private void OnHitted()
    {
        if (_gameState != GameState.Waiting) return;

        StartCoroutine(StartGameRoutine());
    }

    IEnumerator StartGameRoutine()
    {
        yield return null;
        GameState = GameState.Playing;
        StartGame();
    }

    private void StartSpinner(GameState state)
    {
        Debug.Log($"State: {state.ToString()}");

        if (state == GameState.Spinning)
        {
            spinner.SetActive(true);
        } 
        else if (state == GameState.End)
        {
            playOutroCutScene.PlayOutro(scoreManager.TotalScore >= threshold);
        }
        
    }
    
    private void OnDisable()
    {
        foreach (var down in moveDown)
        {
            BeatTrackerr.fixedBeatUpdate -= down.StartScrolling;
        }
        
        hitNeurons.OnHitted -= OnHitted;
        OnGameStateChanged -= StartSpinner;
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
