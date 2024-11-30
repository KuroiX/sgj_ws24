using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

public class Score : MonoBehaviour
{
	// constants
	[SerializeField] private float radius = 10f;
	private readonly Dictionary<uint, HitType> displayScoreToHitType = new() {
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
		float normScore = Math.Max(0, radius - difference.y) / radius;
		return normScore;
	}

	private uint NormScoreToDisplayScore(float normScore)
	{
		float normScoreExp = Mathf.Exp(normScore * 4.5f) / Mathf.Exp(4.5f) - 1f / Mathf.Exp(4.5f);

		int intervals = 5;
		float intervalSize = 1f / intervals;
		float scoreInterval = 0;
		for (int i = intervals - 1; i >= 0; i++)
		{
			if (normScoreExp >= intervalSize * i)
			{
				scoreInterval = i;
				break;
			}
		}

		return scoreInterval switch
		{
			4 => 100 // perfect
			,
			3 => 75 // great
			,
			2 => 50 // good
			,
			1 => 25 // bad
			,
			0 => 0 // miss
			,
			_ => throw new Exception("Impossible! Perhaps the archives are incomplete?")
		};
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
		float multiplier = Mathf.Max(1, streak / 2);  // integer division intended
		OnMultiplierChange?.Invoke(multiplier);
		return (uint) (displayScore * multiplier);
	}
	
}
