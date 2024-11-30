using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Score : MonoBehaviour
{
	// constants
	[SerializeField] private float maxHit = 100f;
	
	private readonly Dictionary<uint, HitType> hitScoreToHitType = new Dictionary<uint, HitType> {
		{100, HitType.Perfect},
		{80, HitType.Great},
		{60, HitType.Good},
		{40, HitType.Bad},
		{0, HitType.Miss}
	};
	
	// privates
	private HitType[] hitHistory;
	private uint totalScore;
	
	// publics
	public uint TotalScore { 
		get => totalScore;
		private set
		{
			totalScore = value;
			OnScoreChange?.Invoke(totalScore);
		}
	}
	public event Action<uint> OnScoreChange;
	public event Action<HitType> OnHit;
	
	// event functions
	private void Start()
	{
		hitHistory = new[] {HitType.Miss, HitType.Miss, HitType.Miss, HitType.Miss, HitType.Miss};
		totalScore = 0;
	}
	
	// public functions
	public void HitNeuron(Vector2 difference)
	{
		uint hitScore = DifferenceToHitScore(difference);
		//HitType hitType = hitScoreToHitType[hitScore];
		HitType hitType = GetHitTypeTemp(hitScore);
		CycleHitHistory(hitType);  // TODO: event for combos
		uint scoreAddition = HitScoreToScoreAddition(hitScore);
		
		OnHit?.Invoke(hitType);
		TotalScore += scoreAddition;
	}
	
	// score management functions
	private uint DifferenceToHitScore(Vector2 difference)
	{
		float hitScore = maxHit - difference.y;  // difference.magnitude?
		return (uint)Mathf.RoundToInt(Math.Max(0, hitScore));  // hinge loss
	}

	private HitType GetHitTypeTemp(uint hitScore)
	{
		if (hitScore * 20 >= 95)
		{
			return HitType.Perfect;
		}
		if (hitScore * 20 >= 80)
		{
			return HitType.Great;
		}
		if (hitScore * 20 >= 60)
		{
			return HitType.Good;
		}
		if (hitScore * 20 >= 40)
		{
			return HitType.Bad;
		}
		
		return HitType.Miss;
	}

	private uint HitScoreToScoreAddition(uint hitScore)  // combos
	{
		float multiplyer = 1;  // TODO: multiplyer system using hit history
		Assert.IsTrue(multiplyer > 0);
		return (uint)Mathf.RoundToInt(hitScore * multiplyer);
	}
	
	// helper functions
	private void CycleHitHistory(HitType hitType)
	{
		for (int i = 0; i < hitHistory.Length - 1; i++)
		{
			hitHistory[i] = hitHistory[i + 1];
		}
		hitHistory[^1] = hitType;
	}
	
}
