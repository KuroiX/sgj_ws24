using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FindingMemo.Player
{
    [ExecuteAlways] public class SidewaysMovement : MonoBehaviour
    {
        [SerializeField] private float accelerationPerFrame = .1f;
        [SerializeField] private float maxSpeed = 10;
        [SerializeField] private bool doAccelerate = true;
        [SerializeField] private float sideLimit = 8.5f;

        [Header("Lanes")] [SerializeField] private bool useLanes = true;
        [SerializeField] private float spacingBetweenLines = 2f;

        private Controls actions;

        private bool isMoving = false;
        private float currentSpeed = 0;
        private int sign = 0;
        private Vector3 initialPosition;

        private const int NumberOfLanes = 3;
        private int currentLane = 0;


        private void Awake()
        {
            initialPosition = transform.position;
        }

        private void OnEnable()
        {
            actions = new Controls();

            if (useLanes)
            {
                actions.Default.MoveLeftOnLanes.performed += OnMoveLeft;
                actions.Default.MoveRightOnLanes.performed += OnMoveRight;
            }
            else
            {
                actions.Default.Move.performed += OnMoveInput;
                actions.Default.Move.canceled += OnCancelMoveInput;
            }

            actions.Default.Enable();
        }

        private void OnDisable()
        {
            actions?.Default.Disable();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(initialPosition + Vector3.right * sideLimit, .1f);
            Gizmos.DrawSphere(initialPosition + Vector3.left * sideLimit, .1f);
        }
#endif

        private void OnMoveLeft(InputAction.CallbackContext callbackContext)
        {
            ChangeLaneInDirection(-1);
        }

        private void OnMoveRight(InputAction.CallbackContext callbackContext)
        {
            ChangeLaneInDirection(1);
        }

        private void ChangeLaneInDirection(int signValue)
        {
            currentLane = Math.Clamp(currentLane + signValue, -1, 1);
            transform.position = new Vector3(initialPosition.x + currentLane * spacingBetweenLines,
                transform.position.y, transform.position.z);
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
            if (useLanes) return;

            currentSpeed = doAccelerate
                ? Math.Clamp(currentSpeed + accelerationPerFrame, 0, maxSpeed)
                : maxSpeed;

            transform.position += sign * currentSpeed * Time.deltaTime * Vector3.right;
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, -sideLimit, sideLimit),
                transform.position.y,
                transform.position.z);
        }

        private void OnCancelMoveInput(InputAction.CallbackContext input)
        {
            currentSpeed = 0;
            isMoving = false;
            sign = 0;
        }
    }
}