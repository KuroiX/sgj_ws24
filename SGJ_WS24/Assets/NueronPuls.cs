using System;
using DG.Tweening;
using UnityEngine;

public class NueronPuls : MonoBehaviour
{
    [SerializeField] private DOTweenAnimation dotweenAnimation;
    void Start()
    {
        Invoke(nameof(Cool), 0.5f);
    }

    private void Cool()
    {
        //Debug.Log("COOL");
        BeatTrackerr.fixedBeatUpdate -= Puls;
        BeatTrackerr.fixedBeatUpdate += Puls;
    }

    private void OnDisable()
    {
        BeatTrackerr.fixedBeatUpdate -= Puls;
    }

    private void Puls()
    {
        dotweenAnimation.DORestart();
    }
}