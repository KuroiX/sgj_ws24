using System;
using FindingMemo.Player;
using UnityEngine;

namespace FindingMemo.Neurons
{
    public class Neuron : MonoBehaviour
    {
        [SerializeField] private float abziehen;
        
        private SidewaysMovement sidewaysMovement;
        private float movementYPos;
        private bool wasAlreadyRemoved;

        private void Awake()
        {
            sidewaysMovement = FindObjectOfType<SidewaysMovement>();
            movementYPos = sidewaysMovement.transform.position.y;
            movementYPos -= abziehen;
        }

        private void Update()
        {
            if (wasAlreadyRemoved) return;
            if (transform.position.y < movementYPos)
            {
                Debug.Log($"beat: removed {DateTime.Now:HH:mm:ss.fff} + {this.gameObject.name}");
                NeuronManager.Instance.RemoveFirstNeuron();
            }
        }

        public void SetAlreadyRemovedFlag()
        {
            wasAlreadyRemoved = true;
        }
    }
}