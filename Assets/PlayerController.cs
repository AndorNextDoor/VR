using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public float moveSpeed = 5f; // Adjust the speed as needed
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Freeze rotation so the player doesn't tip over
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        // Get input from WASD keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction based on input
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Rotate player towards movement direction
        if (moveDirection != Vector3.zero)
        {
            animator.SetBool("IsMoving", true);
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        // Move the player
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);
    }
}