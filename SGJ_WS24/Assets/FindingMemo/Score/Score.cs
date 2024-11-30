using System;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
	[SerializeField] private Transform playerTransform;
	
	// constants
	[SerializeField] private float[] scoreIntervals = {0f, 0.04f, 0.16f, 0.36f, 0.64f};
	[SerializeField] private float radiusScale = 10f;
	private readonly Dictionary<uint, HitType> displayScoreToHitType = new() {
		{100, HitType.Perfect},
		{75, HitType.Great},
		{50, HitType.Good},
		{25, HitType.Bad},
		{0, HitType.Miss}
	};
	
	// privates
	private uint totalScore = 0;
	private uint streak = 0;
	private uint multiplier = 1;
	
	// publics
	public uint TotalScore { 
		get => totalScore;
		private set
		{
			totalScore = value;
			OnScoreChange?.Invoke(totalScore);
		}
	}
	public uint Streak { 
		get => streak;
		private set
		{
			streak = value;
			OnStreakChange?.Invoke(streak);
		}
	}
	public uint Multiplier { 
		get => multiplier;
		private set
		{
			multiplier = value;
			OnMultiplierChange?.Invoke(multiplier);
		}
	}
	public event Action<uint> OnScoreChange;
	public event Action<uint> OnStreakChange;
	public event Action<uint> OnMultiplierChange;
	public event Action<HitType> OnHit;

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
		//Debug.Log($"difference {difference}");
		float normScore = Math.Max(0, radiusScale - difference.magnitude) / radiusScale;
		//Debug.Log($"normScore {normScore}");
		return normScore;
	}

	private uint NormScoreToDisplayScore(float normScore)
	{
		//normScore = Mathf.Pow(normScore, 1);

		float scoreInterval = 0;
		for (int i = scoreIntervals.Length - 1; i >= 0; i--)
		{
			if (normScore >= scoreIntervals[i])
			{
				scoreInterval = i;
				break;
			}
		}
		//Debug.Log($"scoreInterval {scoreInterval}");

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

	private uint DisplayScoreToComboScore(uint displayScore, HitType hitType)
	{
		HashSet<HitType> goodHits = new HashSet<HitType> {HitType.Perfect, HitType.Great, HitType.Good};
		if (goodHits.Contains(hitType))
		{
			streak++;
			uint newStreak = streak - (multiplier-1) * ((multiplier-1) + 1) / 2 - 1;  // triangular number
			if (newStreak >= multiplier)
			{
				multiplier++;
			}
			Debug.Log($"streak {streak}, newStreak {newStreak}, multiplier {multiplier}");
		}
		else
		{
			streak = 0;
			multiplier = 1;
		}
		
		return displayScore * multiplier;
	}

	
	private void OnDrawGizmos()
	{
		Color[] colors = {
			new Color(1,1,1, 0.25f),
			new Color(1,1,1, 0.33f),
			new Color(1,1,1, 0.5f),
			new Color(1,1,1, 0.66f),
			new Color(1,1,1, 0.9f)
		};
		
		for (int i = scoreIntervals.Length - 1; i >= 0; i--)
		{
			Gizmos.color = colors[i];
			Gizmos.DrawSphere(playerTransform.position, (1 - scoreIntervals[i]) * radiusScale);
		}
		
	}
	
}
