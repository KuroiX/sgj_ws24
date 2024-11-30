using System;
using System.Collections.Generic;
using FindMemo.Player;
using UnityEngine;

namespace FindMemo.Neurons
{
    public class NeuronManager : MonoBehaviour
    {
        [SerializeField] private int amountInFirstWave = 4;
        [SerializeField] private GameObject neuronPrefab;
        [SerializeField] private Transform heightLine;
        [SerializeField] private float xVariation;
        [SerializeField] private HitNeurons hitNeurons;

        private Queue<Transform> neurons = new();
        private float yPos;

        private void Awake()
        {
            yPos = heightLine.position.y;
            // TODO: spawn first x
        }

        private void Start()
        {
            hitNeurons.OnHitted += OnHitPressed;
        }

        private void SpawnNewNeuron()
        {
            Transform neuron = Instantiate(neuronPrefab, GetSpawnPosition(), Quaternion.identity).transform;
            neurons.Enqueue(neuron);
        }

        private Vector3 GetSpawnPosition()
        {
            return heightLine.position;
        }

        private Transform GetNextNeuron()
        {
            if (neurons.Count == 0) return null;
            return neurons.Peek();
        }

        private void OnHitPressed(Vector3 position)
        {
            print($"NeuronManager OnHit with position {position}. Next Neuron is {GetNextNeuron()}");
        }
    }
}