using UnityEngine;

public class FaceRotator : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;

    [Header("Face Rotation Settings")]
    public Transform faceTransform; // Assign the face GameObject here
    public float maxRotationAngle = 15f; // Maximum angle the face can rotate
    public float rotationSpeed = 5f; // How fast the face rotates

    private float moveDirection = 0f;

    void Update()
    {
        // Handle movement
        moveDirection = Input.GetAxis("Horizontal"); // Get input (-1 for left, 1 for right)
        //transform.Translate(Vector3.right * moveDirection * speed * Time.deltaTime);

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