using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Serialized field to specify the input manager's attack button
    [SerializeField]
    private string inputManagerAttack;

    // Reference to the Animator component
    private Animator animator;
    // Reference to the attack area GameObject
    private GameObject attackArea;

    // Time interval between attacks
    private float timeBtwAttacks = 1F;

    // Counter to track time between attacks
    private float attackTimeCounter;

    // Property to check if the player should be attacking
    public bool ShouldBeAttacking { get; private set; }

    void Start()
    {
        // Get the attack area which is the first child of the player GameObject
        attackArea = transform.GetChild(0).gameObject;
        // Get the Animator component attached to the player
        animator = GetComponent<Animator>();
        // Initialize the attack time counter
        attackTimeCounter = timeBtwAttacks;
    }

    void Update()
    {
        // If the player should not be attacking, disable the attack area
        if (!ShouldBeAttacking)
        {
            attackArea.SetActive(false);
        }
        // Handle attack controls
        AttackControls();
    }

    // Function to handle attack controls
    private void AttackControls()
    {
        // Check if the attack button is pressed and enough time has passed since the last attack
        if (Input.GetButtonDown(inputManagerAttack) && attackTimeCounter >= timeBtwAttacks)
        {
            // Reset the attack time counter
            attackTimeCounter = 0F;
            // Trigger the attack animation
            animator.SetTrigger("Attack");
            // Execute the attack
            Attack();
        }
        // Increment the attack time counter
        attackTimeCounter += Time.deltaTime;
    }

    // Function to execute the attack
    private void Attack()
    {
        // Set the attacking flag to true
        ShouldBeAttacking = true;
        // Enable the attack area
        attackArea.SetActive(true);
    }

    // Function to reset the attacking flag
    public void ShouldBeDamagingFalse()
    {
        ShouldBeAttacking = false;
    }
}
