using System;
using System.Collections.Generic;
using FindingMemo.Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FindingMemo.Neurons
{
    public class NeuronManager : MonoBehaviour
    {
        public event Action<Neuron> OnNeuronChanged;
        
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

        private void TriggerOnNeuronChanged(Neuron neuron)
        {
            OnNeuronChanged?.Invoke(neuron);
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
            for (int i = 0; i + 1 < neurons.Count; i++)
            {
                var neuron0Position = neurons[i].transform.position;
                var neuron1Position = neurons[i + 1].transform.position;

                var positionInBetweenNeurons = neuron0Position + 0.5f * (neuron1Position - neuron0Position);

                var yDistance = neuron1Position.y - neuron0Position.y;
                var distance = Vector2.Distance(neuron1Position, neuron0Position);

                var rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Acos(yDistance / distance) *
                                                      (neuron1Position.x < neuron0Position.x
                                                          ? 1
                                                          : -1));

                int randomIndex = Random.Range(0, 2);
                var neuronLine = Instantiate(neuronLines[randomIndex], positionInBetweenNeurons, rotation);
                var scale = distance / lineHeights[randomIndex];
                neuronLine.transform.localScale = Vector3.one * scale * 0.5f * 0.8f;
                neuronLine.transform.parent = map;
            }
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
            if (neurons.Count == 0) return;

            var distance = GetDistanceToNearestNeuronFromPlayer();
            scoreManager.HitNeuron(distance);
            neurons.RemoveAt(0);
            TriggerOnNeuronChanged(neurons[0]);
        }

        public void RemoveNeuronFromList(Neuron neuron)
        {
            if (neurons.Count == 0) return;
            if (neurons.Contains(neuron))
            {
                neurons.Remove(neuron);
                TriggerOnNeuronChanged(neurons[0]);
            }
        }
    }
}