using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
	//Game Object references
	public GameObject respawnPoint; //Respawn point reference, usually 0,0,0
	public GameObject checkpoint1; //Checkpoint 1 reference
	public GameObject checkpoint2; //Checkpoint 2 reference
	public GameObject checkpoint3; //Checkpoint 3 reference

	public GameObject respawnEffect; // This is the particle effect that plays when the player respawns
	public GameObject deathEffect; // This is the particle effect that plays when the player dies
	public TrailRenderer trail; // This is the trail renderer on the player
	public GameObject sprite; //Player's sprite object reference

	//Private references
	private SpriteRenderer m_s; 
	private Rigidbody2D rigidbody;
	private Timer timer;

	//Bool variables to keep track of what checkpoint the player is at
	bool c1=false;
	bool c2=false;
	bool c3=false;
	
	//Bool variable to keep track of player state
	private bool isDead = false;

	/* Specifications:
	 * -> Sets private variable references
	 */
	private void Start()
	{
		rigidbody = GetComponent<Rigidbody2D>();
		timer = FindObjectOfType<Timer>();
	}


	private void OnTriggerStay2D(Collider2D collision){
		//checks which checkpoint player is at
		if (collision.tag == "Checkpoint1"){
			c1=true;
			c2=false;
			c3=false;
		}
		if (collision.tag == "Checkpoint2"){
			c2 = true;
			c1=false;
			c3=false;
		}
		if (collision.tag == "Checkpoint3"){
			c3 = true;
			c1=false;
			c2=false;
			if(SceneManager.GetActiveScene().buildIndex==3){
				 Camera.main.orthographicSize = 15;
			}
		}
		//respawns player to the appropriate checkpoint through the Die function
		if(collision.tag=="Spike" && !isDead)
		{
			isDead = true;
			if(c1){
				StartCoroutine(Die(checkpoint1,0.3f, false));
			}else if(c2){
				StartCoroutine(Die(checkpoint2,0.3f, false));
			}else if(c3){
				StartCoroutine(Die(checkpoint3,0.3f, false));
			}else if(c1==false && c2==false && c3==false) {
				StartCoroutine(Die(respawnPoint,0.3f, true));
			}
		}

    }

    private IEnumerator Die(GameObject checkpoint, float seconds, bool isRepsawnPoint)
    {
	    // Handle player physics dying
	    rigidbody.velocity = new Vector3(0,0,0); //Reset velocity
	    rigidbody.constraints = RigidbodyConstraints2D.FreezeAll; //freeze player position
	    
	    // Handle player graphics and effects when dying
	    Instantiate(deathEffect, transform.position, Quaternion.identity); //play death effect
		trail.enabled = false; // Hide player trail
		sprite.SetActive(false); //hide player sprite

		yield return new WaitForSeconds(seconds); //wait a number of seconds
		
		// Handle player physics respawning
		rigidbody.constraints &= ~RigidbodyConstraints2D.FreezePositionY; //unfreeze player Y position
		rigidbody.constraints &= ~RigidbodyConstraints2D.FreezePositionX; //unfreeze player X position
		
		// Teleport the player to the current checkpoint
	    gameObject.transform.position = checkpoint.transform.position; //teleport player to checkpoint
	    
	    // Handle player graphics and effects when respawning
		Instantiate(respawnEffect, transform.position, Quaternion.identity); //play respawn effect
		trail.enabled = true; // Show player trail
		sprite.SetActive(true); //show player sprite
		
		isDead = false; // Reset player alive state
		//Reset the timer if using the respawn point
		if (isRepsawnPoint)
		{
			timer.isCounting = false;
			timer.time = 0f;
			timer.UpdateDisplay();
		}
    }
}
