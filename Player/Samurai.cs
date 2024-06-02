using System.Collections;
using UnityEngine;

public class Samurai : MonoBehaviour
{
    // Dash settings
    private float dashingPower = 24F; // The power or speed of the dash
    private float dashingTime = 0.2F; // Duration of the dash
    private float dashingCooldown = 1F; // Cooldown period after dashing before another dash can be performed

    // Flags to control dashing state
    private bool isDashing; // Whether the samurai is currently dashing
    private bool canDash = true; // Whether the samurai can dash

    // Variable to store the original gravity scale
    private float originalGravity;

    // Reference to the Rigidbody2D component
    [SerializeField]
    Rigidbody2D rigidBody2D;

    // Update is called once per frame
    private void Update()
    {
        // If currently dashing, exit the update function
        if (isDashing)
        {
            return;
        }

        // Check if the Dash button is pressed and if dashing is allowed
        if (Input.GetButtonDown("Dash") && canDash)
        {
            // Start the dash coroutine
            StartCoroutine(Dash());
        }
    }

    // Coroutine to handle the dash behavior
    private IEnumerator Dash()
    {
        // Set flags to indicate dashing state
        canDash = false;
        isDashing = true;

        // Save the original gravity scale and set gravity to zero during the dash
        originalGravity = rigidBody2D.gravityScale;
        rigidBody2D.gravityScale = 0F;

        // Apply dash velocity in the direction the samurai is facing
        rigidBody2D.velocity = new Vector2(transform.localScale.x * dashingPower, 0F);

        // Wait for the duration of the dash
        yield return new WaitForSeconds(dashingTime);

        // Restore the original gravity scale and reset dashing flag
        rigidBody2D.gravityScale = originalGravity;
        isDashing = false;

        // Wait for the cooldown period before allowing another dash
        yield return new WaitForSeconds(dashingCooldown);

        // Reset the canDash flag to allow dashing again
        canDash = true;
    }
}
