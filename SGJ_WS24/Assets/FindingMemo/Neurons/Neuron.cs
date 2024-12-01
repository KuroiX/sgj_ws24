using FindingMemo.Player;
using UnityEngine;

namespace FindingMemo.Neurons
{
    public class Neuron : MonoBehaviour
    {
        [SerializeField] private float abziehen;
        public bool buildConnectionToNextNeuron = true;

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
            if (transform.position.y < movementYPos) NeuronManager.Instance.RemoveFirstNeuron();
        }

        public void SetAlreadyRemovedFlag()
        {
            wasAlreadyRemoved = true;
        }
    }
}