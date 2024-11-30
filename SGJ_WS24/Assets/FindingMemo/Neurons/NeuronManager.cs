using System;
using System.Collections.Generic;
using FindingMemo.Player;
using UnityEngine;

namespace FindingMemo.Neurons
{
    public class NeuronManager : MonoBehaviour
    {
        [SerializeField] private HitNeurons player;
        [SerializeField] private Transform map;
        [SerializeField] private Score scoreManager;

        [Header("NeuronLines")] [SerializeField]
        private GameObject[] neuronLines;

        private readonly float[] lineHeights = {5.98f, 6.57f, 3.76f};

        public static NeuronManager Instance;
        private readonly List<Neuron> neurons = new();

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
            neurons.Clear();
            AddAllNeuronsToQueue();
            AddConnectionsToNeurons();
        }

        private void AddAllNeuronsToQueue()
        {
            var neuronsInScene = FindObjectsOfType<Neuron>();

            foreach (var neuron in neuronsInScene) neurons.Add(neuron);

            neurons.Sort((a, b) => a.transform.position.y < b.transform.position.y
                ? -1
                : 1);
        }

        private void AddConnectionsToNeurons()
        {
            // TODO: add more than first two
            var neuron0Position = neurons[0].transform.position;
            var neuron1Position = neurons[1].transform.position;

            var positionInBetweenNeurons = neuron0Position + 0.5f * (neuron1Position - neuron0Position);

            var yDistance = neuron1Position.y - neuron0Position.y;
            var distance = Vector2.Distance(neuron1Position, neuron0Position);
            var scale = distance / lineHeights[2];

            print(Mathf.Acos(yDistance / distance));
            var rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Acos(yDistance / distance) *
                                                  (neuron1Position.x < neuron0Position.x
                                                      ? 1
                                                      : -1));

            var neuronLine = Instantiate(neuronLines[2], positionInBetweenNeurons, rotation);
            neuronLine.transform.localScale = Vector3.one * scale * 0.5f * 0.8f;
        }

        private void Start()
        {
            player.OnHitted += OnHitPressed;
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
            return GetDistanceToNearestNeuronFrom(player.transform.position);
        }

        private void OnHitPressed()
        {
            var distance = GetDistanceToNearestNeuronFromPlayer();
            scoreManager.HitNeuron(distance);
            neurons.RemoveAt(0);
        }

        public void RemoveNeuronFromList(Neuron neuron)
        {
            if (neurons.Contains(neuron)) neurons.Remove(neuron);
        }
    }
}