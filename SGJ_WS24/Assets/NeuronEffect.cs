using System;
using System.Collections;
using System.Collections.Generic;
using FindingMemo.Neurons;
using Unity.VisualScripting;
using UnityEngine;

public class NeuronEffect : MonoBehaviour
{
    private ParticleSystem lastNeuronParticleEffect;
    public NeuronManager neuronManager;

    private void OnEnable()
    {
        neuronManager.OnNeuronChanged += ActivateNeuronEffect;
    }

    private void OnDisable()
    {
        neuronManager.OnNeuronChanged -= ActivateNeuronEffect;
    }

    public void ActivateNeuronEffect(Neuron neuron)
    {
        if (lastNeuronParticleEffect != null)
        {
            lastNeuronParticleEffect.Stop();
        }

        lastNeuronParticleEffect = neuron.gameObject.GetComponentInChildren<ParticleSystem>();
        lastNeuronParticleEffect.Play();
    }
}
