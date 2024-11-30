using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Score : MonoBehaviour
{
	// constants
	[SerializeField] private float maxHit = 10f;
	
	private readonly Dictionary<uint, HitType> displayScoreToHitType = new Dictionary<uint, HitType> {
		{100, HitType.Perfect},
		{80, HitType.Great},
		{60, HitType.Good},
		{40, HitType.Bad},
		{0, HitType.Miss}
	};
	
	// privates
	private uint streak;
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
	public event Action<float> OnMultiplierChange;
	public event Action<HitType> OnHit;
	
	// event functions
	private void Start()
	{
		totalScore = 0;
		streak = 0;
	}
	
	// public functions
	public void HitNeuron(Vector2 difference)
	{
		float normScore = DifferenceToNormScore(difference);
		uint displayScore = NormScoreToDisplayScore(normScore);
		HitType hitType = displayScoreToHitType[displayScore];
		OnHit?.Invoke(hitType);
		uint comboScore = DisplayScoreToComboScore(displayScore, hitType);
		TotalScore += comboScore;
	}
	
	// score management functions
	private float DifferenceToNormScore(Vector2 difference)
	{
		float normScore = Math.Abs(maxHit - difference.y) / maxHit;
		return normScore;
	}

	private uint NormScoreToDisplayScore(float normScore)
	{
		if (normScore >= 0.95)
		{
			return 100;
		}
		if (normScore >= 0.8)
		{
			return 80;
		}
		if (normScore >= 0.6)
		{
			return 60;
		}
		if (normScore >= 0.4)
		{
			return 30;
		}
		else
		{
			return 0;
		}
	}

	private uint DisplayScoreToComboScore(uint displayScore, HitType hitType)  // combos
	{
		HashSet<HitType> goodHits = new HashSet<HitType> {HitType.Perfect, HitType.Great, HitType.Good};
		if (goodHits.Contains(hitType))
		{
			streak++;
		}
		else
		{
			streak = 0;
		}
		float multiplier = Mathf.Max(1, streak / 2);  // interger division intended
		OnMultiplierChange?.Invoke(multiplier);
		return (uint) (displayScore * multiplier);
	}
	
}
