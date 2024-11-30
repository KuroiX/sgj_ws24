using System;
using System.Collections.Generic;
using FindMemo.Player;
using UnityEngine;

namespace FindMemo.Neurons
{
    public class NeuronManager : MonoBehaviour
    {
        [SerializeField] private GameObject neuronPrefab;
        [SerializeField] private HitNeurons hitNeurons;
        [SerializeField] private Transform testNeuron;

        private readonly Queue<Transform> neurons = new();

        private void Awake()
        {
            neurons.Enqueue(testNeuron);
        }

        private void Start()
        {
            hitNeurons.OnHitted += OnHitPressed;
        }


        private Transform GetNextNeuron()
        {
            if (neurons.Count == 0) return null;
            return neurons.Peek();
        }

        private void OnHitPressed(Vector3 position)
        {
            var nearestNeuron = GetNextNeuron();

            Vector2 distance = position - nearestNeuron.position;
            distance.x = Math.Abs(distance.x);
            distance.y = Math.Abs(distance.y);
            // TODO: call score method
        }
    }
}