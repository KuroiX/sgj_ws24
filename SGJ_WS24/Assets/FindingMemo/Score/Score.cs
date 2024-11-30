using System;
using System.Collections.Generic;
using System.Linq;
using FindingMemo.Neurons;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

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
		Debug.Log($"difference {difference}");
		float normScore = Math.Max(0, radiusScale - difference.magnitude) / radiusScale;
		Debug.Log($"normScore {normScore}");
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
		Debug.Log($"scoreInterval {scoreInterval}");

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

	
	private void OnDrawGizmos()
	{
		//List<Neuron> neurons = NeuronManager.Instance.neurons;
		
		/*
		 * perfect: 2
		 * great: 4
		 * good: 6
		 * bad: 8
		 * miss: 10
		 *
		 */

		
		Color[] colors = {
			new Color(1,1,1, 0.25f), // Red with 50% transparency
			new Color(1,1,1, 0.33f), // Yellow with 50% transparency
			new Color(1,1,1, 0.5f), // Blue with 50% transparency
			new Color(1,1,1, 0.66f), // Magenta with 50% transparency
			new Color(1,1,1, 0.9f)  // Cyan with 50% transparency
		};
		
		//Gizmos.DrawSphere(playerTransform.position, 1 * radiusScale);
		
		for (int i = scoreIntervals.Length - 1; i >= 0; i--)
		{
			Gizmos.color = colors[i];
			Gizmos.DrawSphere(playerTransform.position, (1- scoreIntervals[i]) * radiusScale);
		}
		
	}
	
}
