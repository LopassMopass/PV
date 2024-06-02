using UnityEngine;

public class Grounded : MonoBehaviour
{
    // Boolean to indicate whether the object is currently on the ground
    public bool onGround;

    // Called when a 2D collision begins
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has the tag "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Set onGround to true since the object is now on the ground
            onGround = true;
        }
    }

    // Called when a 2D collision ends
    private void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the collided object has the tag "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Set onGround to false since the object is no longer on the ground
            onGround = false;
        }
    }
}
