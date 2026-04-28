using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SideScrollEnemy : MonoBehaviour
{
    [Header("Targeting")]
    public Transform player; // Assign player in inspector

    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpForce = 8f;
    public float gravity = 20f;
    public float stopDistance = 1.0f; // Distance to stop from player

    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        // 1. Calculate direction on X-axis only
        Vector3 directionToPlayer = player.position - transform.position;

        // 2. Determine move direction (right or left)
        float moveX = 0f;
        if (Mathf.Abs(directionToPlayer.x) > stopDistance)
        {
            moveX = directionToPlayer.x > 0 ? 1f : -1f;
        }

        // 3. Keep X movement, reset Y to gravity, keep Z alignment
        moveDirection.x = moveX * speed;
        moveDirection.z = directionToPlayer.z; // Keeps alignment if player is not directly on line

        // Apply Gravity
        if (controller.isGrounded)
        {
            moveDirection.y = -0.5f; // Small force to keep grounded

            // 4. Simple Jump Logic: If player is higher and close
            if (directionToPlayer.y > 1.0f && Mathf.Abs(directionToPlayer.x) < 2.0f)
            {
                moveDirection.y = jumpForce;
            }
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // 5. Move the controller
        controller.Move(moveDirection * Time.deltaTime);
    }
}