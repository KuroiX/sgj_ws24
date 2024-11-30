using FindingMemo.Player;
using UnityEngine;

namespace FindingMemo.Neurons
{
    public class Neuron : MonoBehaviour
    {
        private SidewaysMovement sidewaysMovement;
        private float movementYPos;

        private void Awake()
        {
            sidewaysMovement = FindObjectOfType<SidewaysMovement>();
            movementYPos = sidewaysMovement.transform.position.y;
        }

        private void Update()
        {
            if (transform.position.y < movementYPos)
            {
                print($"{name} is below player, removing from list and destroying me.");
                NeuronManager.Instance.RemoveNeuronFromList(this);
                Destroy(gameObject);
            }
        }
    }
}