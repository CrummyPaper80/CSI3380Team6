using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceObstacle : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private float power;
    
    // Private references
    private Rigidbody2D playerRigibody;

    private void Start()
    {
        // Set private references
        playerRigibody = FindObjectOfType<PlayerController>().GetComponent<Rigidbody2D>();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            //playerRigibody.AddForce(new Vector2(0f,power), ForceMode2D.Impulse);
            playerRigibody.velocity = new Vector2(playerRigibody.velocity.x, power);
        }
    }
}
