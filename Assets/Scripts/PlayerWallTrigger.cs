using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallTrigger : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private bool right; // Determines whether this is the right trigger or not
    
    [Header("References")] 
    [SerializeField] private PlayerController playerController; // Reference to the player controller script
    
    /* Specifications:
     * -> Checks for when the player has come into contact with the wall
     * -> Runs climb function on player controller to set climbing to true and pass in this side
     */
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            StartCoroutine(playerController.Climb(true, right));
        }
    }
    
    /* Specifications:
     * -> Checks for when the player has lost contact with the wall
     * -> Runs climb function on player controller to set climbing to false and pass in this side
     */
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            StartCoroutine(playerController.Climb(false, right));
        }
    }
}
