using UnityEngine;

namespace FindingMemo.Neurons
{
    public class Neuron : MonoBehaviour
    {
        private void Start()
        {
            NeuronManager.Instance.AddToQueue(this);
        }
    }
}