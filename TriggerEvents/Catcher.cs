using UnityEngine;

public class Catcher : MonoBehaviour
{
    // Reference to the Collider2D component attached to this object
    [SerializeField]
    private Collider2D capsuleCollider;

    // This method is called when another collider enters the trigger zone of this object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the entering collider belongs to an object with the "Catcher" tag
        if (collision.gameObject.CompareTag("Catcher"))
        {
            // Enable the capsule collider attached to this object
            capsuleCollider.enabled = true;
        }
    }
}
