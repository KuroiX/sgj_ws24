using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HightScoreManager : MonoBehaviour
{
    [NonSerialized] public static HightScoreManager instance;
    [NonSerialized] public List<ScoreObj> scoreObjs = new List<ScoreObj>();
    
    public Score score;
    public GameObject askForDataUI;
    public TMP_Text nameText;

    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        
        askForDataUI.SetActive(false);
    }


    private void FindeScore()
    {
        score = GameObject.FindObjectOfType<Score>();
    }
    private void UpdateHightScore(string name)
    {
        scoreObjs.Add(new ScoreObj(){Level = SceneManager.GetActiveScene().name, Name = name, Score = score.TotalScore});
    }

    public void OpenAskForHightScore()
    {
        askForDataUI.SetActive(true);
    }

    // wird genutz von einem Button
    public void SetNewScore()
    {
        if(score == null)
            FindeScore();
        
        if(score == null)
            return;
        
        UpdateHightScore(nameText.text);
        
        askForDataUI.SetActive(false);
    }

    public List<ScoreObj> GetSoredList(string Level)
    {
        return scoreObjs
            .FindAll(x => x.Level == Level)
            .OrderByDescending(x => x.Score)
            .ToList();
    }
}


public class ScoreObj
{
    public string Level;
    public string Name;
    public uint Score;
}