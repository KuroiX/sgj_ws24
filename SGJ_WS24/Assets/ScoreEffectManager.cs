using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HitType
{
    Perfect, Great, Good, Bad, Miss
}

public class ScoreEffectManager : MonoBehaviour
{
    public ParticleSystem miss;
    public ParticleSystem bad;
    public ParticleSystem good;
    public ParticleSystem great;
    public ParticleSystem perfect;

    public void TriggerEffect(HitType type)
    {
        switch (type)
        {
            case HitType.Miss:
                miss.Play();
                break;
            case HitType.Bad:
                bad.Play();
                break;
            case HitType.Good:
                good.Play();
                break;
            case HitType.Great:
                great.Play();
                break;
            case HitType.Perfect:
                perfect.Play();
                break;
        }
    }

    [ContextMenu("Trigger Miss")]
    public void TriggerMiss()
    {
        TriggerEffect(HitType.Miss);
    }
    [ContextMenu("Trigger Bad")]
    public void TriggerBad()
    {
        TriggerEffect(HitType.Bad);
    }
    [ContextMenu("Trigger Good")]
    public void TriggerGood()
    {
        TriggerEffect(HitType.Good);
    }
    [ContextMenu("Trigger Great")]
    public void TriggerGreat()
    {
        TriggerEffect(HitType.Great);
    }
    [ContextMenu("Trigger Perfect")]
    public void TriggerPerfect()
    {
        TriggerEffect(HitType.Perfect);
    }
}
