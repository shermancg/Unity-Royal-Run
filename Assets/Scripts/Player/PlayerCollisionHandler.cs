using UnityEngine;
using System.Collections;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;
    [SerializeField] float gotHitCooldown = 1f;
    [SerializeField] float initialInvulnPeriod = 0.5f;

    const string gotHitString = "gotHit";
    bool canBeHit = false; // Start as false to prevent hits at game start

    LevelGenerator levelGenerator;
    ObstacleSpawner obstacleSpawner;

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
            Debug.Log("Collision detected with: " + collision.gameObject.name);

            levelGenerator.ChangeChunkMoveSpeed(levelGenerator.reduceSpeed); // Decrease the chunk move speed on hit
            // obstacleSpawner.AdjustSpawnInterval(obstacleSpawner.spawnSlower); // Increase the spawn interval on hit

            StartCoroutine(GotHitCooldownCo());
        }
    }

    IEnumerator GotHitCooldownCo()
    {
        canBeHit = false;
        yield return new WaitForSeconds(gotHitCooldown);
        canBeHit = true;
    }

}
