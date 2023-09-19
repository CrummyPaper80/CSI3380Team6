using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
	
	public GameObject LevelCompleteUI;
	public GameObject TimerUI;
	public GameObject sprite;

	private Rigidbody2D rigidbody;
	public bool complete1;
	public bool complete2;
	public bool complete3;
	public bool complete4;

	bool betterTime;


	private void Start()
	{
		rigidbody = GetComponent<Rigidbody2D>();
	}

	

	private void OnTriggerEnter2D(Collider2D collision)
	{
    	if (collision.tag == "Finish")
		{
			Timer.instance.UpdateBestTime(); //UpdateBestTime

			rigidbody.velocity = new Vector3(0,0,0); //Reset velocity
	    	rigidbody.constraints = RigidbodyConstraints2D.FreezeAll; //freeze player position
			sprite.SetActive(false); //hide player sprite




			LevelCompleteUI.SetActive(true); //displays the levelcompelete UI
			TimerUI.SetActive(false); //hides the timerUI

			
			//Updates the playerpref for checking whether a player has completed a level or not
			if (SceneManager.GetActiveScene().buildIndex==2){
				complete1 = true;
				PlayerPrefs.SetInt("Level1Complete", (complete1 ? 1 : 0));
			}
			if (SceneManager.GetActiveScene().buildIndex==3){
				complete2 = true;
				PlayerPrefs.SetInt("Level2Complete", (complete2 ? 1 : 0));
			}
			if (SceneManager.GetActiveScene().buildIndex==4){
				complete3 = true;
				PlayerPrefs.SetInt("Level3Complete", (complete3 ? 1 : 0));
			}
			if (SceneManager.GetActiveScene().buildIndex==5){
				complete4 = true;
				PlayerPrefs.SetInt("Level4Complete", (complete4 ? 1 : 0));
			}
			
	  	}
		
		
	}	
	
}
                        