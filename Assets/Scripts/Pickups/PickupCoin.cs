using UnityEngine;

public class PickupCoin : Pickup
{
    [Header("References")]
    [SerializeField] AudioSource coinPickupSound; // Audio source for the coin pickup sound
    [SerializeField] ParticleSystem coinParticles; // Particle system for the coin pickup effect

    [Header("Coin Settings")]
    [SerializeField] int scoreValue = 1;
    [SerializeField] float particleSpawnYoffset = 0.5f;
    ScoreManager scoreManager; // Reference to the ScoreManager to update the score



    public void Init(ScoreManager scoreManager)
    {
        this.scoreManager = scoreManager;
    }

    protected override void OnPickup()
    {
        scoreManager.IncreaseScore(scoreValue); // Increase the score by the coin's value

        Vector3 coinVFXspawnPOS = new Vector3(transform.position.x, transform.position.y + particleSpawnYoffset, transform.position.z);

        // Instantiate the particles and keep a reference to the instance
        ParticleSystem particlesInstance = Instantiate(coinParticles, coinVFXspawnPOS, coinParticles.transform.rotation);
        Destroy(particlesInstance.gameObject, particlesInstance.main.duration);

        // This detaches its parenting from the spawned chunk, 
        // this was kind of an accident but it actually looks better because the coin won't keep moving after you pick it up
        coinPickupSound.transform.parent = null;
        coinPickupSound.Play();

        Destroy(coinPickupSound.gameObject, coinPickupSound.clip.length); // Destroy the sound object after it finishes playing

    }



}
