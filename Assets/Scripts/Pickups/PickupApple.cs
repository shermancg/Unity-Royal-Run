using UnityEngine;

public class PickupApple : Pickup
{
    [Header("References")]
    [SerializeField] AudioSource appleSound; // Audio source for the coin pickup sound
    [SerializeField] ParticleSystem appleParticles; // Particle system for the apple pickup effect
    [SerializeField] float particleSpawnYoffset = 1f; // Y offset for the particle spawn position
    LevelGenerator levelGenerator;

    public void Init(LevelGenerator levelGenerator)
    {
        this.levelGenerator = levelGenerator;
    }

    protected override void OnPickup()
    {
        levelGenerator.ChangeChunkMoveSpeed(levelGenerator.increaseSpeed);

        PlayAppleSound();


        Vector3 appleVFXspawnPOS = new Vector3(transform.position.x, transform.position.y + particleSpawnYoffset, transform.position.z);
        // Instantiate the particles and keep a reference to the instance
        ParticleSystem particlesInstance = Instantiate(appleParticles, appleVFXspawnPOS, appleParticles.transform.rotation);
        Destroy(particlesInstance.gameObject, particlesInstance.main.duration);

        Destroy(gameObject); // Destroy the apple prefab immediately
    }

    private void PlayAppleSound()
    {
        // Create a temporary GameObject for the sound
        // I needed to do this because the appleSound is a prefab and it was destroying the sound before it could play
        // With the coin this wasn't an issue because the coin's audio file was so short that it didn't make detaching from the
        // parent look broken, but since the apple sound is longer, detaching it from the parent would cause it to look broken
        GameObject tempGO = new GameObject("TempAppleSound");
        AudioSource aSource = tempGO.AddComponent<AudioSource>();
        aSource.clip = appleSound.clip;
        aSource.volume = appleSound.volume;
        aSource.spatialBlend = 0f; // 2D sound
        aSource.Play();
        Destroy(tempGO, aSource.clip.length);
    }
}
