using UnityEngine;

public class ParticleTest : MonoBehaviour
{
    public ParticleSystem particleToTest;
    public CameraShake cameraShake;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            particleToTest.Play();
            cameraShake.TriggerShake(0.2f);
        }
    }
}
