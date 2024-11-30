using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class StatsManager : MonoBehaviour
{
    [SerializeField] private TMP_Text ScoreText;
    [SerializeField] private TMP_Text PerfectText;
    [SerializeField] private TMP_Text GreatText;
    [SerializeField] private TMP_Text GoodText;
    [SerializeField] private TMP_Text BadText;
    [SerializeField] private TMP_Text MissText;

    [SerializeField] private DOTweenAnimation ScoreDotweenAnimation;

    [ContextMenu(nameof(Test))]
    public void Test()
    {
        SetScore(5000);
    }

    public void SetScore(int score)
    {
        ScoreDotweenAnimation.optionalString = score.ToString();
        //scoreText.text = score.ToString();
        ScoreDotweenAnimation.DORestart();
    }

    public void SetPerfect(int score)
    {
        PerfectText.text = "x" + score;
        PerfectText.rectTransform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.3f, 13, 0.5f);
    }

    public void SetGreat(int score)
    {
        GreatText.text = "x" + score;
        GreatText.rectTransform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.3f, 13, 0.5f);
    }

    public void SetGood(int score)
    {
        GoodText.text = "x" + score;
        GoodText.rectTransform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.3f, 13, 0.5f);
    }

    public void SetBad(int score)
    {
        BadText.text = "x" + score;
        BadText.rectTransform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.3f, 13, 0.5f);
    }

    public void SetMiss(int score)
    {
        MissText.text = "x" + score;
        MissText.rectTransform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.3f, 13, 0.5f);
    }
}