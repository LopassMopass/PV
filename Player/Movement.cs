using UnityEngine;

public class Movement : MonoBehaviour
{
    // References to the Rigidbody2D, Grounded script and Animator component
    private Rigidbody2D body;
    private Grounded grounded;
    private Animator animator;

    // Horizontal input value
    private float xInput;

    // Serialized fields for input manager axes names
    [SerializeField]
    private string inputManagerHorizontal;

    [SerializeField]
    private string inputManagerVertical;

    // Serialized fields for movement, jump multipliers and ground friction decay
    [SerializeField]
    private float groundSpeedMultiplier;

    [SerializeField]
    private float jumpSpeedMultiplier;

    [SerializeField]
    [Range(0f, 1f)]
    private float groundDecay;

    // Initialization
    private void Start()
    {
        // Get the Rigidbody2D, Grounded and Animator components attached to the GameObject
        body = GetComponent<Rigidbody2D>();
        grounded = GetComponent<Grounded>();
        animator = GetComponent<Animator>();
    }

    // Called once per frame
    private void Update()
    {
        IdelAnimation(); // Play idle animation
        JumpAnimation(); // Update jump animation
        GetInput(); // Get player input
        Controls(); // Handle player controls
    }

    // Called at fixed intervals for physics updates
    private void FixedUpdate()
    {
        Friction(); // Apply ground friction
    }

    // Get player input for horizontal movement
    private void GetInput()
    {
        xInput = Input.GetAxis(inputManagerHorizontal); // Example axes: HorizontalAD || HorizontalLeftRight
    }

    // Handle player movement and jumping controls
    private void Controls()
    {
        // If there is horizontal input, move the character
        if (Mathf.Abs(xInput) > 0)
        {
            // Set animation state to walking
            animator.SetInteger("AnimationState", 1);

            // Update the character's velocity based on input and ground speed multiplier
            body.velocity = new Vector2(xInput * groundSpeedMultiplier, body.velocity.y);

            // Adjust the character's scale based on movement direction
            float direction = Mathf.Sign(xInput) * 0.3F;
            transform.localScale = new Vector3(direction, 0.3F, 1);
        }

        // Handle jumping input
        if (Input.GetButtonDown(inputManagerVertical) && grounded.onGround) // Example buttons: JumpW || JumpUp
        {
            // Play jump animation
            animator.Play("Jump");

            // Update the character's vertical velocity based on jump speed multiplier
            body.velocity = new Vector2(body.velocity.x, jumpSpeedMultiplier);

            // Adjust the character's scale based on movement direction
            float direction = Mathf.Sign(xInput) * 0.3F;
            transform.localScale = new Vector3(direction, 0.3F, 1);
        }
    }

    // Apply ground friction to the character when not moving
    private void Friction()
    {
        if (grounded.onGround && xInput == 0 && body.velocity.y <= 0)
        {
            // Gradually reduce the character's velocity
            body.velocity *= groundDecay;
        }
    }

    // Set the idle animation state
    private void IdelAnimation()
    {
        animator.SetInteger("AnimationState", 0);
    }

    // Update the grounded animation state
    private void JumpAnimation()
    {
        if (grounded.onGround == true)
        {
            animator.SetBool("Grounded", true);
        }
        else
        {
            animator.SetBool("Grounded", false);
        }
    }
}
