using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundTrigger : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private PlayerController playerController; // Reference to the player controller script

    [Header("Effects")] 
    [SerializeField] private GameObject landEffect;
    
    /* Specifications:
     * -> Checks for when the player's feet have come into contact with the ground
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            // Instantiates the land effect at the player's feet
            Instantiate(landEffect, transform.position + (Vector3.up * .01f)/* other.ClosestPoint(playerController.transform.position)*/, Quaternion.identity);
        }
    }
    
    /* Specifications:
     * -> Checks for if the player's feet are in contact with the ground
     * -> Runs ground function on player controller to ground the player
     */
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            if (playerController.dashes < 1 && !playerController.dashing)
                playerController.dashes++;
            StartCoroutine(playerController.Ground(true));
        }
    }
    
    /* Specifications:
     * -> Checks for when the player's feet have lost contact with the ground
     * -> Runs ground function on player controller to unground the player
     */
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            StartCoroutine(playerController.Ground(false));
        }
    }
}
