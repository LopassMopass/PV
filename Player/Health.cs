using UnityEngine;

public class Health : MonoBehaviour
{
    // Animator component for handling animations.
    private Animator animator;

    // Current health of the player.
    private float currentHealth;

    // Maximum health of the player, set to 1 by default.
    private float maxHealth = 1F;

    // Reference to the collider component, serialized to be set in the inspector.
    [SerializeField]
    private Collider2D capsuleCollider;

    // Reference to the opponent's flag, serialized to be set in the inspector.
    [SerializeField]
    private Flag opponentFlag;

    // Reference to the opponent's player game object, serialized to be set in the inspector.
    [SerializeField]
    private GameObject opponentPlayer;

    // Flag indicating whether the player has captured the flag.
    public bool hasFlag = false;

    // Flag indicating whether the player is dead.
    public bool isDead = false;

    // Start is called before the first frame update.
    public void Start()
    {
        // Initialize the animator component.
        animator = GetComponent<Animator>();

        // Set the current health to the maximum health at the start.
        currentHealth = maxHealth;
    }

    // Method to handle taking damage.
    public void TakeDamage(float damage)
    {
        // Reduce the current health by the damage amount.
        currentHealth -= damage;

        // Trigger the hurt animation.
        animator.SetTrigger("Hurt");

        // Check if the current health is less than or equal to 0.
        if (currentHealth <= 0)
        {
            // If health is zero or less, the player dies.
            Die();
        }
    }

    // Method to handle the player's death.
    public void Die()
    {
        // Set the death animation.
        animator.SetBool("Death", true);

        // Set the isDead flag to true.
        isDead = true;

        // Disable the capsule collider to prevent further interactions.
        capsuleCollider.enabled = false;

        // Disable this script to stop further execution.
        this.enabled = false;

        // If the player has the flag, reset the flag's position.
        if (hasFlag)
        {
            opponentFlag.ResetFlag();
        }
    }
}
