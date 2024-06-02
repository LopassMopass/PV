using System.Collections;
using UnityEngine;

public class NextScene : MonoBehaviour
{
    // Variables to hold references
    private Collider2D objectCollider2D; // Collider of objects in 2D
    private Camera mainCamera; // Reference to the main camera
    private GameObject[] spawns; // Array to hold spawn points
    private float move; // Variable to control camera movement

    // Reference to opponent player game object
    [SerializeField]
    private GameObject opponentPlayer;

    // Array to hold colliders
    [SerializeField]
    private GameObject[] colliders;

    // Array to hold spawn points' transforms
    [SerializeField]
    private Transform[] spawnPoints;

    // Called at the start of the script
    public void Start()
    {
        // Get reference to the main camera
        mainCamera = Camera.main;

        // Set the move to default position float of camera
        move = Camera.main.transform.position.x;

        // Find all game objects with the tag "RespawnPoint" and store them
        spawns = GameObject.FindGameObjectsWithTag("RespawnPoint");

        // Initialize the array to hold spawn points' transforms
        spawnPoints = new Transform[spawns.Length];

        // Iterate through each spawn point and store their transforms
        for (int i = 0; i < spawns.Length; i++)
        {
            spawnPoints[i] = spawns[i].transform;
        }

        // Check visibility of spawn points
        CheckSpawnPointsVisibility();
    }

    // Triggered when a collision occurs
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If colliding with "NextSceneLeft" and opponent player is dead
        if (collision.gameObject.CompareTag("NextSceneLeft") && opponentPlayer.GetComponent<Health>().isDead == true)
        {
            // Set collision object as trigger
            objectCollider2D = collision.collider;
            objectCollider2D.isTrigger = true;

            // Move camera left and perform necessary actions
            StartCoroutine(MoveCameraLeft());
            OpponentPlayerSpawn();
            CheckSpawnPointsVisibility();
            collision.transform.gameObject.tag = "NextSceneRight";
            OpponentPlayerHandle();
            StartCoroutine(ResetTriggerAfterDelay(0.4F));
        }
        // If colliding with "NextSceneRight" and opponent player is dead
        else if (collision.gameObject.CompareTag("NextSceneRight") && opponentPlayer.GetComponent<Health>().isDead == true)
        {
            // Set collision object as trigger
            objectCollider2D = collision.collider;
            objectCollider2D.isTrigger = true;

            // Move camera right and perform necessary actions
            StartCoroutine(MoveCameraRight());
            CheckSpawnPointsVisibility();
            OpponentPlayerSpawn();
            collision.transform.gameObject.tag = "NextSceneLeft";
            OpponentPlayerHandle();
            StartCoroutine(ResetTriggerAfterDelay(0.4F));
        }
    }

    // Respawn the player at the closest spawn point
    public void Respawn()
    {
        // Find the farthest spawn point
        Transform farthestSpawn = FindFarthestSpawnPoint();
        // Move the player to the farthest spawn point
        transform.position = farthestSpawn.position;
    }

    // Check the visibility of spawn points from the camera
    private void CheckSpawnPointsVisibility()
    {
        // Iterate through each spawn point
        foreach (Transform spawnPoint in spawnPoints)
        {
            // If the spawn point is visible, tag it as active; otherwise, tag it as disabled
            if (IsVisibleFrom(mainCamera, spawnPoint))
            {
                spawnPoint.gameObject.tag = "ActiveSpawnpoint";
            }
            else
            {
                spawnPoint.gameObject.tag = "DisabledSpawnpoint";
            }
        }
        // Activate spawns
        ActivateSpawns();
    }

    // Activate the spawns
    private void ActivateSpawns()
    {
        // Find all game objects with the tag "ActiveSpawnpoint" and store them
        spawns = GameObject.FindGameObjectsWithTag("ActiveSpawnpoint");
        // Initialize the array to hold spawn points' transforms
        spawnPoints = new Transform[spawns.Length];

        // Iterate through each active spawn point and store their transforms
        for (int i = 0; i < spawns.Length; i++)
        {
            spawnPoints[i] = spawns[i].transform;
        }
    }

    // Deactivate the spawns
    private void DeactivateSpawns()
    {
        // Find all game objects with the tag "DisabledSpawnpoint" and store them
        spawns = GameObject.FindGameObjectsWithTag("DisabledSpawnpoint");
        // Initialize the array to hold spawn points' transforms
        spawnPoints = new Transform[spawns.Length];

        // Iterate through each disabled spawn point and store their transforms
        for (int i = 0; i < spawns.Length; i++)
        {
            spawnPoints[i] = spawns[i].transform;
        }
    }

    // Reset all spawn points to disabled
    private void ResetSpawns()
    {
        // Find all game objects with the tag "ActiveSpawnpoint" and set their tag to "DisabledSpawnpoint"
        GameObject[] objects = GameObject.FindGameObjectsWithTag("ActiveSpawnpoint");
        foreach (GameObject objectPoint in objects)
        {
            objectPoint.transform.tag = "DisabledSpawnpoint";
        }
    }

    // Plays all SpawnPoint functions 
    private void OpponentPlayerSpawn()
    {
        opponentPlayer.GetComponent<NextScene>().ResetSpawns();
        opponentPlayer.GetComponent<NextScene>().DeactivateSpawns();
        opponentPlayer.GetComponent<NextScene>().CheckSpawnPointsVisibility();
    }

    // Handles the opponent status
    private void OpponentPlayerHandle()
    {
        opponentPlayer.GetComponent<NextScene>().Respawn();
        opponentPlayer.GetComponent<Health>().isDead = false;
        opponentPlayer.GetComponent<Health>().enabled = true;
        opponentPlayer.GetComponent<Animator>().SetBool("Death", false);
    }

    // Find the farthest spawn point from the player
    private Transform FindFarthestSpawnPoint()
    {
        // Initialize variables
        Transform farthest = null;
        float maximalDistance = float.MinValue;

        // Iterate through each spawn point
        foreach (Transform spawn in spawnPoints)
        {
            // Calculate the distance between the player and the spawn point
            float distance = Vector3.Distance(transform.position, spawn.position);
            // If the distance is greater than the current maximal distance, update the maximal distance and farthest spawn point
            if (distance > maximalDistance)
            {
                maximalDistance = distance;
                farthest = spawn;
            }
        }

        return farthest;
    }

    // Coroutine to move the camera left
    private IEnumerator MoveCameraLeft()
    {
        // Adjust camera position and wait for a delay
        move -= 6.4F;
        mainCamera.transform.position = new Vector3(move, 0F, -20F);
        ResetSpawns();
        DeactivateSpawns();
        yield return new WaitForSeconds(2);
    }

    // Coroutine to move the camera right
    private IEnumerator MoveCameraRight()
    {
        // Adjust camera position and wait for a delay
        move += 6.4F;
        mainCamera.transform.position = new Vector3(move, 0F, -20F);
        ResetSpawns();
        DeactivateSpawns();
        yield return new WaitForSeconds(2);
    }

    // Coroutine


    // Coroutine to reset the collider trigger after a delay
    private IEnumerator ResetTriggerAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);
        // Reset the collider trigger status to false
        objectCollider2D.isTrigger = false;
    }

    // Check if a transform is visible from a camera's perspective
    private bool IsVisibleFrom(Camera camera, Transform transform)
    {
        // Calculate the frustum planes of the camera
        var planes = GeometryUtility.CalculateFrustumPlanes(camera);
        // Get the position of the transform
        var point = transform.transform.position;

        // Iterate through each plane
        foreach (var plane in planes)
        {
            // If the distance from the transform to the plane is negative, it's not visible
            if (plane.GetDistanceToPoint(point) < 0)
            {
                return false;
            }
        }
        // If the transform is not behind any of the camera's planes, it's visible
        return true;
    }

}
