using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public event Action<int> OnScoreChanged;

    public int Score
    {
        get => _score;
        private set
        {
            _score = value;
            OnScoreChanged?.Invoke(_score);
        }
    }

    private int _score;
    
    public static event Action<bool> PauseTriggered
    {
        add => _boolEvent.EventTriggered += value;
        remove => _boolEvent.EventTriggered -= value;
    }

    public static bool IsPaused
    {
        get => _boolEvent.IsActive;
        private set => _boolEvent.IsActive = value;
    }

    private static BoolEvent _boolEvent;
    
    [Header("Set in Editor")] 
    [SerializeField] private bool pauseTimeScale;
    
    [Header("Assign in Editor")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject volumePanel;

    private float _timeScale;

    private void Awake()
    {
        pauseMenu.SetActive(false);
        pauseMenuPanel.SetActive(true);
        volumePanel.SetActive(false);
        
        _timeScale = Time.timeScale;
    }

    private void Update()
    {
        if (!Keyboard.current.escapeKey.wasReleasedThisFrame) return;

        TriggerPauseMenu();
    }

    public void TriggerPauseMenu()
    {
        IsPaused = !IsPaused;
        
        pauseMenu.SetActive(IsPaused);
        pauseMenuPanel.SetActive(true);
        volumePanel.SetActive(false);
        
        if (pauseTimeScale)
        {
            Time.timeScale = IsPaused ? 0 : _timeScale;
        }
    }

    public void VolumeButton(bool volumeActivated)
    {
        pauseMenuPanel.SetActive(!volumeActivated);
        volumePanel.SetActive(volumeActivated);
    }

    public void MainMenuButton()
    {
        Time.timeScale = _timeScale;
        IsPaused = !IsPaused;
        FindObjectOfType<SceneLoader>().LoadSceneByIndex(0);
    }
}
