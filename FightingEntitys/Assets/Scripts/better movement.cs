using UnityEngine;

public class bettermovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 8f;
    public float jumpForce = 5f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;

    private Rigidbody rb;
    private float horizontalInput;
    private bool isGrounded;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Freeze rotation so the player doesn't fall over
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        // 1. Get Input (A/D or Left/Right Arrow)
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // 2. Jumping Input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, 0f);
        }
    }

    void FixedUpdate()
    {
        // 3. Ground Check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        // 4. Move Left/Right, maintaining vertical velocity for gravity/jumping
        rb.linearVelocity = new Vector3(horizontalInput * moveSpeed, rb.linearVelocity.y, 0f);
    }
}
