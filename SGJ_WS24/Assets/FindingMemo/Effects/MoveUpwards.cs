using UnityEngine;

public class MoveUpward : MonoBehaviour
{
    // Speed of the upward movement
    public float upwardSpeed = 5f;
    
    // Speed of horizontal movement
    public float horizontalSpeed = 5f;

    void Update()
    {
        // Constant upward movement
        transform.Translate(Vector3.up * upwardSpeed * Time.deltaTime);

        // Horizontal movement based on player input
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * horizontalSpeed * Time.deltaTime);
    }
}