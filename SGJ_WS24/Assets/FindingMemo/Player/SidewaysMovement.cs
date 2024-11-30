using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FindingMemo.Player
{
    public class SidewaysMovement : MonoBehaviour
    {
        [SerializeField] private float accelerationPerFrame = .1f;
        [SerializeField] private float maxSpeed = 10;
        [SerializeField] private bool doAccelerate = true;

        private Controls actions;

        private bool isMoving = false;
        private float currentSpeed = 0;
        private int sign = 0;


        private void OnEnable()
        {
            actions = new Controls();
            actions.Default.Move.performed += OnMoveInput;
            actions.Default.Move.canceled += OnCancelMoveInput;
            actions.Default.Enable();
        }

        private void OnDisable()
        {
            actions?.Default.Disable();
        }


        private void OnMoveInput(InputAction.CallbackContext input)
        {
            var inputValue = input.ReadValue<float>();
            isMoving = true;
            sign = inputValue < 0
                ? -1
                : 1;
        }


        private void Update()
        {
            if (!isMoving) return;

            currentSpeed = doAccelerate
                ? Math.Clamp(currentSpeed + accelerationPerFrame, 0, maxSpeed)
                : maxSpeed;
            transform.position += sign * currentSpeed * Time.deltaTime * Vector3.right;
        }

        private void OnCancelMoveInput(InputAction.CallbackContext input)
        {
            currentSpeed = 0;
            isMoving = false;
            sign = 0;
        }
    }
}