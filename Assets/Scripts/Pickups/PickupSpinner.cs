using UnityEngine;

public class PickupSpinner : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 10f; // Speed of rotation in degrees per second
    [SerializeField] float bobbingAmplitude = 0.25f; // How far up and down to move
    [SerializeField] float bobbingFrequency = 1f; // How fast to bob

    float startY;

    void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);

        float newY = startY + Mathf.Sin(Time.time * bobbingFrequency) * bobbingAmplitude;
        float deltaY = newY - transform.position.y;
        transform.Translate(0f, deltaY, 0f, Space.World);
    }
}
