using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float sideMoveSpeed = 5f; // Speed of the player
    [SerializeField] float forwardMoveSpeed = 5f; // Speed of the player
    [SerializeField] float xClamp = 5f; // Maximum x position
    [SerializeField] float zClamp = 5f; // Maximum z position

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

        // Apply different speeds for side and forward movement
        float moveX = movement.x * sideMoveSpeed * Time.fixedDeltaTime;
        float moveZ = movement.y * forwardMoveSpeed * Time.fixedDeltaTime;

        Vector3 moveDirection = new Vector3(moveX, 0, moveZ);
        Vector3 newPosition = currentPosition + moveDirection; // Calculate the new position

        // Clamp the new position to the bounds of the play area
        newPosition.x = Mathf.Clamp(newPosition.x, -xClamp, xClamp);
        newPosition.z = Mathf.Clamp(newPosition.z, -zClamp, zClamp);

        rb.MovePosition(newPosition); // Move the Rigidbody to the new position
    }

}
