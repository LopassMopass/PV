using UnityEngine;

public class Viking : MonoBehaviour
{
    [SerializeField]
    private GameObject weaponPrefab; // The prefab of the weapon that the Viking throws.

    [SerializeField]
    private Transform throwPoint; // The point from which the weapon is thrown.

    private float throwForce = 10f; // The force with which the weapon is thrown.
    private float cooldownTime = 2f; // The time interval between consecutive throws.

    private float lastThrowTime; // The time when the last throw occurred.

    void Update()
    {
        // Check if the "Throw" button is pressed and if enough time has passed since the last throw.
        if (Input.GetButtonDown("Throw") && Time.time > lastThrowTime + cooldownTime)
        {
            // If conditions are met, throw the weapon.
            ThrowWeapon();
            // Update the time of the last throw.
            lastThrowTime = Time.time;
        }
    }

    void ThrowWeapon()
    {
        // Instantiate a new weapon object at the throw point with its rotation.
        GameObject thrownWeapon = Instantiate(weaponPrefab, throwPoint.position, throwPoint.rotation);
        // Get the Rigidbody2D component of the thrown weapon.
        Rigidbody2D rigiBody2D = thrownWeapon.GetComponent<Rigidbody2D>();
        // Add force to the thrown weapon in the direction of the throw point's right direction.
        rigiBody2D.AddForce(throwPoint.right * throwForce, ForceMode2D.Impulse);
    }
}
