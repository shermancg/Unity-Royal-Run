using UnityEngine;
using System.Collections;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;
    [SerializeField] float gotHitCooldown = 1f;
    [SerializeField] float initialInvulnPeriod = 2f;
    const string gotHitString = "gotHit";
    bool canBeHit = false; // Start as false to prevent hits at game start

    void Start()
    {
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
