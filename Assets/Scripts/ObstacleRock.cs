using System;
using System.Collections;
using System.IO.Compression;
using Unity.Cinemachine;
using UnityEngine;

public class ObstacleRock : MonoBehaviour
{
    [SerializeField] ParticleSystem collisionParticles;
    [SerializeField] AudioSource boulderSmashAudioSource;
    [SerializeField] float shakeIntensityModifier = 10f; // Global modifier for shake intensity if needed
    [SerializeField] float collisionCooldown = 0.3f;
    private float lastCollisionTime = 0f;

    CinemachineImpulseSource cinemachineImpulseSource;
    private void Awake()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Update()
    {
        lastCollisionTime += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (lastCollisionTime < collisionCooldown) return;
        
        CameraShake();
        PlayCollisionVFX(collision);
        boulderSmashAudioSource.Play();
        lastCollisionTime = 0f; // Reset the cooldown timer
    }

    private void PlayCollisionVFX(Collision collision)
    {
        ContactPoint contactPoint = collision.contacts[0];
        collisionParticles.transform.position = contactPoint.point;
        collisionParticles.Play();
    }

    private void CameraShake()
    {
        float cameraDistance = Vector3.Distance(this.transform.position, Camera.main.transform.position);
        float shakeIntensity = (1f / cameraDistance) * shakeIntensityModifier; // Inverse relationship to distance
        shakeIntensity = MathF.Min(shakeIntensity, 1f); // Cap the intensity to a maximum of 1

        cinemachineImpulseSource.GenerateImpulse(shakeIntensity);
    }
}
