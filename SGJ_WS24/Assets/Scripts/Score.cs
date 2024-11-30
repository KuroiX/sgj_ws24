using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum HitType {Perfect, Great, Good, Bad, Miss}

public class Score : MonoBehaviour
{
	// constants
	[SerializeField] private static readonly float MaxHit = 100f;
	private static readonly Dictionary<uint, HitType> HitScoreToHitType = new Dictionary<uint, HitType> {
		{100, HitType.Perfect},
		{80, HitType.Great},
		{60, HitType.Good},
		{40, HitType.Bad},
		{0, HitType.Miss}
	};
	
	// privates
	private static HitType[] _hitHistory;
	private static uint _totalScore;
	
	// publics
	public static uint TotalScore { 
		get => _totalScore;
		private set
		{
			_totalScore = value;
			OnScoreChange?.Invoke(_totalScore);
		}
	}
	public static event Action<uint> OnScoreChange;
	public static event Action<HitType> OnHit;
	
	// event functions
	private void Start()
	{
		_hitHistory = new[] {HitType.Miss, HitType.Miss, HitType.Miss, HitType.Miss, HitType.Miss};
		_totalScore = 0;
	}
	
	// public functions
	public static void HitNeuron(Vector2 difference)
	{
		uint hitScore = DifferenceToHitScore(difference);
		HitType hitType = HitScoreToHitType[hitScore];
		CycleHitHistory(hitType);  // TODO: event for combos
		uint scoreAddition = HitScoreToScoreAddition(hitScore);
		
		OnHit?.Invoke(hitType);
		TotalScore += scoreAddition;
	}
	
	// score management functions
	private static uint DifferenceToHitScore(Vector2 difference)
	{
		float hitScore = MaxHit - difference.y;  // difference.magnitude?
		return (uint)Mathf.RoundToInt(Math.Max(0, hitScore));  // hinge loss
	}

	private static uint HitScoreToScoreAddition(uint hitScore)  // combos
	{
		float multiplyer = 1;  // TODO: multiplyer system using hit history
		Assert.IsTrue(multiplyer > 0);
		return (uint)Mathf.RoundToInt(hitScore * multiplyer);
	}
	
	// helper functions
	private static void CycleHitHistory(HitType hitType)
	{
		for (int i = 0; i < _hitHistory.Length - 1; i++)
		{
			_hitHistory[i] = _hitHistory[i + 1];
		}
		_hitHistory[^1] = hitType;
	}
	
}
