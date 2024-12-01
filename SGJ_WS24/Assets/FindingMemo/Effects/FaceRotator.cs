using UnityEngine;
using UnityEngine.InputSystem;

public class FaceRotator : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;

    [Header("Face Rotation Settings")]
    public Transform faceTransform; // Assign the face GameObject here
    public float maxRotationAngle = 15f; // Maximum angle the face can rotate
    public float rotationSpeed = 5f; // How fast the face rotates

    private float moveDirection = 0f;
    private Controls actions;

    private void OnEnable()
    {
        actions = new Controls();
        // Subscribe to input actions
        actions.Default.Move.performed += OnMoveInput;
    }

    private void OnDisable()
    {
        // Unsubscribe from input actions
        actions.Default.Move.performed -= OnMoveInput;
    }
    
    
    private void OnMoveInput(InputAction.CallbackContext input)
    {
        moveDirection = input.ReadValue<float>();
    }

    private void OnMoveLeft(InputAction.CallbackContext context)
    {
        Debug.Log("Test: move left");
        moveDirection = -1f; // Move left
    }

    private void OnMoveRight(InputAction.CallbackContext context)
    {
        Debug.Log("Test: move right");
        moveDirection = 1f; // Move right
    }

    private void Update()
    {
        // Apply movement (if necessary for other parts of your game)
        // transform.Translate(Vector3.right * moveDirection * speed * Time.deltaTime);

        // Handle face rotation
        RotateFace();
    }

    private void RotateFace()
    {
        if (faceTransform == null)
        {
            Debug.LogWarning("Face Transform not assigned!");
            return;
        }

        // Calculate target angle based on movement direction
        float targetAngle = -moveDirection * maxRotationAngle;

        // Smoothly rotate the face to the target angle
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);
        faceTransform.localRotation = Quaternion.Slerp(faceTransform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}