using UnityEngine;

public class Flag : MonoBehaviour
{
    [SerializeField]
    private Transform flagHomePosition; // The position where the flag returns when reset.

    [SerializeField]
    private GameObject flagParent; // The parent object of the flag when it's not carried.

    public GameObject carrier; // The current carrier of the flag.

    // This method is called when another collider enters the trigger collider attached to this object.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Checks if the colliding object is tagged as "Player" and if it's the current carrier.
        if (other.CompareTag("Player") && carrier == other.gameObject)
        {
            // Marks the carrier as having the flag.
            other.GetComponent<Health>().hasFlag = true;

            // Sets the flag's parent to the carrier and positions it relative to the carrier.
            transform.SetParent(carrier.transform); // Sets the flag as a child of parent
            transform.localPosition = new Vector3(0, 3, 0); // Adjusts the flag's position relative to the carrier.
        }
    }

    // Drops the flag at a given position.
    public void DropFlag(Vector3 dropPosition)
    {
        if (carrier != null)
        {
            // Marks the carrier as not having the flag anymore.
            carrier.GetComponent<Health>().hasFlag = false;

            // Resets the carrier to null, indicating the flag is not being carried anymore.
            carrier = null;

            // Sets the flag's parent back to the flagParent object and positions it at the given drop position.
            transform.SetParent(flagParent.transform);
            transform.position = dropPosition;
        }
    }

    // Resets the flag to its home position.
    public void ResetFlag()
    {
        // Calls DropFlag method with the home position as the drop position.
        DropFlag(flagHomePosition.position);
    }
}
