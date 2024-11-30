using System;
using System.Collections.Generic;
using FindingMemo.Player;
using UnityEngine;

namespace FindingMemo.Neurons
{
    public class NeuronManager : MonoBehaviour
    {
        [SerializeField] private HitNeurons hitNeurons;
        [SerializeField] private Score scoreManager;

        public static NeuronManager Instance;
        private readonly List<Neuron> neurons = new();

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
            neurons.Clear();
            AddAllNeuronsToQueue();
        }

        private void AddAllNeuronsToQueue()
        {
            var neuronsInScene = FindObjectsOfType<Neuron>();

            foreach (var neuron in neuronsInScene) neurons.Add(neuron);

            neurons.Sort((a, b) => a.transform.position.y < b.transform.position.y
                ? -1
                : 1);
        }

        private void Start()
        {
            hitNeurons.OnHitted += OnHitPressed;
        }


        private Neuron GetNextNeuron()
        {
            if (neurons.Count == 0) return null;
            return neurons[0];
        }

        private Vector2 GetDistanceToNearestNeuronFrom(Vector3 position)
        {
            var nearestNeuron = GetNextNeuron();

            Vector2 distance = position - nearestNeuron.transform.position;
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
            neurons.RemoveAt(0);
        }

        public void RemoveNeuronFromList(Neuron neuron)
        {
            neurons.Remove(neuron);
        }
    }
}