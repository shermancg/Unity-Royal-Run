using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator playerAnimator;
    [SerializeField] AudioSource hitAudioSource;
  
    [Header("Settings")]
    [SerializeField] float gotHitCooldown = 1f;
    [SerializeField] float initialInvulnPeriod = 0.5f;

    [SerializeField] float shakeIntensityModifier = 10f; // Global modifier for shake intensity if needed
    
    const string gotHitString = "gotHit";
    bool canBeHit = false; // Start as false to prevent hits at game start

    LevelGenerator levelGenerator;
    ObstacleSpawner obstacleSpawner;

    CinemachineImpulseSource cinemachineImpulseSource;
    private void Awake()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
        if (levelGenerator == null)
        {
            Debug.LogError("LevelGenerator not found in the scene.");
            return;
        }

        StartCoroutine(InitialInvulnCo());
    }

    // This coroutine allows the player to be invulnerable for a short time at the start
    IEnumerator InitialInvulnCo()
    {
        yield return new WaitForSeconds(initialInvulnPeriod);
        canBeHit = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (canBeHit)
        {
            playerAnimator.SetTrigger(gotHitString);
            // Debug.Log("Collision detected with: " + collision.gameObject.name);

            levelGenerator.ChangeChunkMoveSpeed(levelGenerator.reduceSpeed); // Decrease the chunk move speed on hit
            
            hitAudioSource.Play();

            CameraShake();

            StartCoroutine(GotHitCooldownCo());
        }
    }

private void CameraShake()
    {
        cinemachineImpulseSource.GenerateImpulse(shakeIntensityModifier);
    }

    IEnumerator GotHitCooldownCo()
    {
        canBeHit = false;
        yield return new WaitForSeconds(gotHitCooldown);
        canBeHit = true;
    }

}
