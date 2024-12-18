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

    private void Start()
    {
        ActivateNeuronEffect(neuronManager.Neurons[0]);
    }

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
        Debug.Log($"Activate neuron {neuron}");
        
        if (lastNeuronParticleEffect)
        {
            lastNeuronParticleEffect.Stop();
            lastNeuronParticleEffect.gameObject.SetActive(false);
        }

        if (!neuron) return;

        lastNeuronParticleEffect = neuron.gameObject.GetComponentInChildren<ParticleSystem>();
        lastNeuronParticleEffect.Play();
    }
}
