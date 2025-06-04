using UnityEngine;

public class ObstacleAudioPlayer : MonoBehaviour
{

    [Header("References")]
    [SerializeField] AudioSource audioSource;

    [Header("Settings")]
    [SerializeField] float collisionCooldown = 0.6f;

    private float lastCollisionTime = 0f;

    void Awake()
    {
        if (audioSource == null)
        {
            Debug.LogError("ObstacleAudioPlayer: AudioSource is not assigned!");
        }
    }

    void Update()
    {
        lastCollisionTime += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (lastCollisionTime < collisionCooldown) return;

        audioSource.Play();
        lastCollisionTime = 0f; // Reset the cooldown timer
    }

}
