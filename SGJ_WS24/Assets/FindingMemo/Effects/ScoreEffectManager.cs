using System;
using UnityEngine;
using UnityEngine.Events;

public class ScoreEffectManager : MonoBehaviour
{
    [SerializeField] private Score scoreManager;
    
    [Header("schrift")]
    public ParticleSystem miss;
    public ParticleSystem bad;
    public ParticleSystem good;
    public ParticleSystem great;
    public ParticleSystem perfect;

    [Header("wegfliegen")] 
    public ParticleSystem badweg;
    public ParticleSystem goodweg;
    public ParticleSystem greatweg;
    public ParticleSystem perfectweg;
    
    [Header("zusatz")]
    public CameraShake cameraShake;
    public ParticleSystem hitEffect;

    private void OnEnable()
    {
        scoreManager.OnHit += TriggerEffect;
    }

    private void OnDisable()
    {
        scoreManager.OnHit -= TriggerEffect;
    }

    public void TriggerEffect(HitType type)
    {
        switch (type)
        {
            case HitType.Miss:
                miss.Stop();
                miss.Play();
                hitEffect.Play();
                break;
            case HitType.Bad:
                bad.Stop();
                bad.Play();
                badweg.Play();
                break;
            case HitType.Good:
                good.Stop();
                good.Play();
                goodweg.Play();
                break;
            case HitType.Great:
                great.Stop();
                great.Play();
                greatweg.Play();
                break;
            case HitType.Perfect:
                perfect.Stop();
                perfect.Play();
                perfectweg.Play();
                break;
        }
        
        cameraShake.TriggerShake(0.2f);
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
