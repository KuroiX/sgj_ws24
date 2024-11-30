using UnityEngine;

public class ParticlesAlignment : MonoBehaviour
{
    public ParticleSystem particleSystem;
    private Vector3 lastPosition;
    private Vector3 currentVelocity;

    void Start()
    {
        if (particleSystem == null)
            particleSystem = GetComponent<ParticleSystem>();

        // Initialize last position
        lastPosition = transform.position;
    }

    void LateUpdate()
    {
        // Calculate velocity based on transform movement
        currentVelocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;

        // Update particle system with velocity-based rotation
        AlignParticlesToVelocity();
    }

    private void AlignParticlesToVelocity()
    {
        if (currentVelocity.sqrMagnitude < Mathf.Epsilon)
            return; // Skip if velocity is too small (avoids zero-vector issues)

        var particles = new ParticleSystem.Particle[particleSystem.particleCount];
        int count = particleSystem.GetParticles(particles);

        for (int i = 0; i < count; i++)
        {
            // Align particle rotation to the velocity vector
            Vector3 particleDirection = currentVelocity.normalized; // Ensure the vector has unit length
            particles[i].rotation3D = Quaternion.LookRotation(particleDirection).eulerAngles;
        }

        particleSystem.SetParticles(particles, count);
    }
}