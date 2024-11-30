using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FindingMemo.Player
{
    public class HitNeurons : MonoBehaviour
    {
        public Action OnHitted;
        private Controls actions;

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
            OnHitted?.Invoke();
        }
    }
}