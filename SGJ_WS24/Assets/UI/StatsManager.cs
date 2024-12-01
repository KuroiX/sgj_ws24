using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class StatsManager : MonoBehaviour
{
    [SerializeField] private Score score;
    
    [SerializeField] private TMP_Text StreakText;
    [SerializeField] private TMP_Text MultiplierText;
    [SerializeField] private TMP_Text ScoreText;
    [SerializeField] private TMP_Text PerfectText;
    [SerializeField] private TMP_Text GreatText;
    [SerializeField] private TMP_Text GoodText;
    [SerializeField] private TMP_Text BadText;
    [SerializeField] private TMP_Text MissText;
    
    [SerializeField] private DOTweenAnimation ScoreDotweenAnimation;

    private int missScore = 0;
    private int badScore = 0;
    private int goodScore = 0;
    private int greatScore = 0;
    private int perfectScore = 0;
    

    [ContextMenu(nameof(Test))]
    public void Test()
    {
        SetScore(5000);
    }

    private void OnEnable()
    {
        score.OnHit += SetHitType;
        score.OnScoreChange += SetScore;
        score.OnMultiplierChange += ChangeMultiplier;
        score.OnStreakChange += ChangeStreak;
    }

    private void OnDisable()
    {
        score.OnHit -= SetHitType;
        score.OnScoreChange -= SetScore;
        score.OnMultiplierChange -= ChangeMultiplier;
        score.OnStreakChange -= ChangeStreak;
    }

    public void SetScore(uint score)
    {
        //Debug.Log("Score: "+ score);
        //ScoreDotweenAnimation.optionalString = score.ToString();
        //scoreText.text = score.ToString();
        //ScoreDotweenAnimation.DORestart();
        ScoreText.text = score.ToString();
    }

    private void SetHitType(HitType type)
    {
        switch (type)
        {
            case HitType.Perfect:
                AddUpPerfect();
                break;
            case HitType.Great:
                AddUpGreat();
                break;
            case HitType.Good:
                AddUpGood();
                break;
            case HitType.Bad:
                AddUpBad();
                break;
            case HitType.Miss:
                AddUpMiss();
                break;
        }
        
        
    }
    
    public void ChangeStreak(uint streak)
	{
		StreakText.text = streak.ToString();
		if (streak > 0)
		{
			StreakText.rectTransform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.3f, 13, 0.5f);
		}
	}
    
    public void ChangeMultiplier(uint multiplier)
    {
	    if (multiplier == 1)
	    {
		    MultiplierText.text = "";
	    }
	    else
	    {
		    MultiplierText.text = "x" + multiplier;
		    MultiplierText.rectTransform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.3f, 13, 0.5f);
	    }
    }

    public void AddUpPerfect()
    {
        PerfectText.text = "x" + ++perfectScore;
        PerfectText.rectTransform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.3f, 13, 0.5f);
    }

    public void AddUpGreat()
    {
        GreatText.text = "x" + ++greatScore;
        GreatText.rectTransform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.3f, 13, 0.5f);
    }

    public void AddUpGood()
    {
        GoodText.text = "x" + ++goodScore;
        GoodText.rectTransform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.3f, 13, 0.5f);
    }

    public void AddUpBad()
    {
        BadText.text = "x" + ++badScore;
        BadText.rectTransform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.3f, 13, 0.5f);
    }

    public void AddUpMiss()
    {
        MissText.text = "x" + ++missScore;
        MissText.rectTransform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.3f, 13, 0.5f);
    }
}