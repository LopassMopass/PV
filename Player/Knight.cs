using System.Collections;
using UnityEngine;

public class Knight : MonoBehaviour
{
    // Duration for how long the block will last
    private float blockDuration = 1.0f;
    // Cooldown period after blocking before the knight can block again
    private float blockCooldown = 2.0f;
    // Duration for how long the knight will be stunned
    private float stunDuration = 0.5f;

    // Flag to check if the knight is on cooldown after blocking
    private bool isOnCooldown = false;
    // Public flag to check if the knight is currently stunned
    public bool isStunned = false;

    void Update()
    {
        // Check if the "Block" button is pressed and the knight is not on cooldown
        if (Input.GetButtonDown("Block") && !isOnCooldown)
        {
            // Start the blocking coroutine
            StartCoroutine(Block());
        }
    }

    // Coroutine to handle the block action
    private IEnumerator Block()
    {
        //isBlocking = true;

        // Wait for the block duration
        yield return new WaitForSeconds(blockDuration);

        //isBlocking = false;

        // Set the cooldown flag to true after blocking
        isOnCooldown = true;

        // Wait for the cooldown duration
        yield return new WaitForSeconds(blockCooldown);

        // Reset the cooldown flag to allow blocking again
        isOnCooldown = false;
    }

    // Trigger event when another collider enters this knight's collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider belongs to an "AttackArea"
        if (other.CompareTag("AttackArea"))
        {
            // Start the stun coroutine for the knight hit by the attack
            StartCoroutine(Stun(other));
        }
    }

    // Coroutine to handle the stun effect on the knight
    private IEnumerator Stun(Collider2D collider)
    {
        // Set the isStunned flag to true on the knight hit by the attack
        collider.GetComponent<Knight>().isStunned = true;

        // Wait for the stun duration
        yield return new WaitForSeconds(stunDuration);

        // Reset the isStunned flag to false after the stun duration
        collider.GetComponent<Knight>().isStunned = false;
    }
}
