using UnityEngine;
using UnityEngine.InputSystem;

namespace FindMemo.Player
{
    public class HitNeurons : MonoBehaviour
    {
        private Controls actions;

        private bool isInNeuronArea = false;

        private void OnEnable()
        {
            actions = new Controls();
            actions.Default.Hit.performed += OnHit;
            actions.Default.Enable();
        }

        private void OnDisable()
        {
            actions?.Default.Disable();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Neuron")) return;
            isInNeuronArea = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Neuron")) return;
            isInNeuronArea = false;
        }

        private void OnHit(InputAction.CallbackContext callbackContext)
        {
            print($"hit success: {isInNeuronArea}");
        }
    }
}