using UnityEngine;
using UnityEngine.InputSystem;

namespace Kerstin
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

        private void OnHit(InputAction.CallbackContext callbackContext)
        {
            print($"hit success: {isInNeuronArea}");
        }
    }
}