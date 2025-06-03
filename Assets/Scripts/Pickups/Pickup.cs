using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f;
    const string playerTag = "Player";


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            OnPickup();
            // Destroy(this.gameObject); // I had to disable this because it was destroying pickups before they coudd play sounds
        }
    }

    protected abstract void OnPickup(); 

}
