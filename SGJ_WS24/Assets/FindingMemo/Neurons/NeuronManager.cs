using System;
using System.Collections.Generic;
using FindingMemo.Player;
using UnityEngine;

namespace FindingMemo.Neurons
{
    public class NeuronManager : MonoBehaviour
    {
        [SerializeField] private GameObject neuronPrefab;
        [SerializeField] private HitNeurons hitNeurons;
        [SerializeField] private Transform testNeuron;
        [SerializeField] private Score scoreManager;
        

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

        private Vector2 GetDistanceToNearestNeuronFrom(Vector3 position)
        {
            var nearestNeuron = GetNextNeuron();

            Vector2 distance = position - nearestNeuron.position;
            distance.x = Math.Abs(distance.x);
            distance.y = Math.Abs(distance.y);

            return distance;
        }

        private Vector2 GetDistanceToNearestNeuronFromPlayer()
        {
            return GetDistanceToNearestNeuronFrom(hitNeurons.transform.position);
        }

        private void OnHitPressed()
        {
            var distance = GetDistanceToNearestNeuronFromPlayer();
            scoreManager.HitNeuron(distance);
            // TODO: call score method
            print($"distance: {distance}");
        }
    }
}