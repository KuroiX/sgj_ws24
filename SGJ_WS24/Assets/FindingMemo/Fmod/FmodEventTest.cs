using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FmodEventTest : MonoBehaviour
{

    [SerializeField] private GameObject blinck;

    private bool b;
    private void Start()
    {
        BeatTrackerr.fixedBeatUpdate += () =>
        {
            blinck.GetComponent<Renderer>().material.color = b ? Color.black : Color.white;
            blinck.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.05f).SetLoops(2,LoopType.Yoyo).SetEase(Ease.Linear);
            b = !b;
        };
        
        
        BeatTrackerr.upBeatUpdate += () =>
        {
            //blinck.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.05f).SetLoops(2,LoopType.Yoyo).SetEase(Ease.Linear);

            //blinck.GetComponent<Renderer>().material.color = b ? Color.black : Color.white;
            //b = !b;
        };
        
        BeatTrackerr.markerUpdated += () =>
        {
            //Debug.Log("Maker");
        };

        BeatTrackerr.tempoChanged += interval =>
        {
            //Debug.Log("tempoChanged");
        };
    }
}
