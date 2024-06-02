using UnityEngine;

public class AttackArea : MonoBehaviour
{
    // Damage amount to be applied when the attack connects
    [SerializeField]
    private float damageAmount;

    // Called when this object's collider triggers with another collider
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Check if the collided object is tagged as "Player"
        if (collider.gameObject.CompareTag("Player"))
        {
            // If collided object is a player, apply damage
            GiveDamage(collider);
        }
    }

    // Apply damage to the collided object
    private void GiveDamage(Collider2D collider)
    {
        // Retrieve the Health component from the collided object and apply damage to it
        collider.GetComponent<Health>().TakeDamage(damageAmount);
    }
}
