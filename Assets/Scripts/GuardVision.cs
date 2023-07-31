using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuardVision : MonoBehaviour
{
    public float viewDistance = 10f; // How far the guard can see
    public float viewAngle = 45f; // The angle of the guard's field of view

    private GameObject player; // The player object
    public GameManager gameManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Assumes your player object has the tag "Player"
    }

    void Update()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position; // The direction from the guard to the player
        float angle = Vector3.Angle(directionToPlayer, transform.forward); // The angle between the guard's forward direction and the direction to the player

        // Check if the player is within the guard's field of view
        if (directionToPlayer.magnitude < viewDistance && angle < viewAngle)
        {
            // Cast a ray towards the player
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToPlayer.normalized, out hit, viewDistance))
            {
                // If the player is the first thing the raycast hits, the guard can "see" the player
                if (hit.collider.gameObject == player)
                {
                    Debug.Log("Guard sees player!");
                    gameManager.DetectedSoundPlay();
                    gameManager.GameOver(); // Trigger "Game Over" state
                }
            }
            // Draw the ray for debugging purposes (only visible in Scene view, not Game view)
            Debug.DrawRay(transform.position, directionToPlayer.normalized * viewDistance, Color.red);
        }
    }
}

