using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);        
    }
}
