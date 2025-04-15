using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f; // Speed of the player

    Vector2 movement;
    Rigidbody rb; // Reference to the player's Rigidbody component

    void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component attached to the player
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
        // Debug.Log("Move: " + movement);
    }

    private void HandleMovement()
    {
        // Move the player based on the input
        Vector3 currentPosition = rb.position; // Get the current position of the Rigidbody
        Vector3 moveDirection = new Vector3(movement.x, 0, movement.y);
        Vector3 newPosition = currentPosition + moveDirection * (moveSpeed * Time.fixedDeltaTime); // Calculate the new position based on the movement direction and speed
        rb.MovePosition(newPosition); // Move the Rigidbody to the new position
    }

}
