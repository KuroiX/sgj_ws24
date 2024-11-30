using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Duration of the shake in seconds
    public float shakeDuration = 0.5f;
    // Magnitude of the shake (how far it moves)
    public float shakeMagnitude = 0.2f;
    // Damping speed to reduce shake over time
    public float dampingSpeed = 1.0f;

    // Original position of the camera
    private Vector3 initialPosition;
    // Current duration of the shake
    private float currentShakeDuration = 0f;

    void Awake()
    {
        // Save the initial position of the camera
        initialPosition = transform.localPosition;
    }

    void OnEnable()
    {
        // Ensure the camera resets its position when enabled
        initialPosition = transform.localPosition;
    }

    public void TriggerShake(float duration = -1f)
    {
        // Start the shake with custom or default duration
        currentShakeDuration = duration > 0f ? duration : shakeDuration;
    }

    void Update()
    {
        // Only shake if there's time left
        if (currentShakeDuration > 0)
        {
            // Apply random offset within a sphere for shake effect
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            // Decrease the shake duration over time
            currentShakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            // Reset the camera to its original position
            currentShakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }
}