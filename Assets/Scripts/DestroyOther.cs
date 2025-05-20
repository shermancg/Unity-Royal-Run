using UnityEngine;

public class DestroyOther : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        Debug.Log("Destroyed: " + other.gameObject.name);
    }
}
