using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowButtons : MonoBehaviour
{
    [HideInInspector] 
    public GameObject Level1Btn;
    [HideInInspector] 
    public GameObject Level2Btn;
    [HideInInspector] 
    public GameObject Level3Btn;
    [HideInInspector] 
    public GameObject Level4Btn;

    [HideInInspector] 
    public GameObject Level1BestTime;
    [HideInInspector] 
    public GameObject Level2BestTime;
    [HideInInspector] 
    public GameObject Level3BestTime;
    [HideInInspector] 
    public GameObject Level4BestTime;

    [HideInInspector] 
    public Text Level1BestTimee;
    [HideInInspector] 
    public Text Level2BestTimee;
    [HideInInspector] 
    public Text Level3BestTimee;
    [HideInInspector] 
    public Text Level4BestTimee;

    [HideInInspector] 
    public bool complete1;
    [HideInInspector] 
	public bool complete2;
    [HideInInspector] 
	public bool complete3;
    [HideInInspector] 
	public bool complete4;

    [HideInInspector] 
    public int LevelOne;
    [HideInInspector] 
    public int LevelTwo;
    [HideInInspector] 
    public int LevelThree;
    [HideInInspector] 
    public int LevelFour;

    [HideInInspector] 
    public float bestTime;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ResetAll();
        }
    }

    //Checks whether a player has completed certain levels in order to view the other levels on the level select screen
    private void Awake(){
    
        LevelOne=PlayerPrefs.GetInt("Level1Complete");
        LevelTwo=PlayerPrefs.GetInt("Level2Complete");
        LevelThree=PlayerPrefs.GetInt("Level3Complete");
        LevelFour=PlayerPrefs.GetInt("Level4Complete");

        if(LevelOne==1){ //if level one is completed,
           Level2Btn.SetActive(true);
           Level2BestTime.SetActive(true);
        }else{
           Level2Btn.SetActive(false);
           Level2BestTime.SetActive(false);
        }
        if(LevelTwo==1){ //if level two is completed,
           Level3Btn.SetActive(true);
           Level3BestTime.SetActive(true);
        }else{
           Level3Btn.SetActive(false);
           Level3BestTime.SetActive(false);
        }
        if(LevelThree==1){ //if level three is completed,
           Level4Btn.SetActive(true);
           Level4BestTime.SetActive(true);
        }else{
           Level4Btn.SetActive(false);
           Level4BestTime.SetActive(false);
        }
        if(LevelFour==1){ //if level four is completed,
          // Level5Btn.SetActive(true);
           //Level5BestTime.SetActive(true);
        }else{
          // Level5Btn.SetActive(false);
           //Level5BestTime.SetActive(false);
        }
    }
    
    //Resets all player data that we have so far: best times for all of the levels, and all level completes
    public void ResetAll(){
        //The following lines reset the best times for each level
        bestTime=0;
        string minutes = ((int) bestTime / 60).ToString("f0");
        string seconds = (bestTime % 60).ToString("f3");

        PlayerPrefs.DeleteKey("BestTime"+"Lvl 1 - Sewers");
        Level1BestTimee.text = "Best time: "+minutes.ToString() + "." + seconds.ToString();

        PlayerPrefs.DeleteKey("BestTime"+"Lvl 2 - Buildings");
        Level2BestTimee.text = "Best time: "+minutes.ToString() + "." + seconds.ToString();

        PlayerPrefs.DeleteKey("BestTime"+"Lvl 3 - Sky");
        Level3BestTimee.text = "Best time: "+minutes.ToString() + "." + seconds.ToString();

        PlayerPrefs.DeleteKey("BestTime"+"Lvl 4 - UFO");
        Level4BestTimee.text = "Best time: "+minutes.ToString() + "." + seconds.ToString();

        
        reset(complete1, "Level1Complete", Level2Btn, Level2BestTime); //resets level 1 level compelete

        reset(complete2, "Level2Complete", Level3Btn, Level3BestTime); //resets level 2 level compelete

        reset(complete3, "Level3Complete", Level4Btn, Level4BestTime); //resets level 3 level compelete

        //reset(complete4, "Level4Complete", Level5Btn, Level5BestTime);
        //below lines reset the level 4 level complete, though does not change or hide anything yet because it's the last level as we have it so far
        complete4 = false;
        PlayerPrefs.SetInt("Level4Complete", (complete1 ? 1 : 0));
	}

    //resets the player pref for level compelte back to false
    public void reset(bool complete,String level, GameObject levelbtn, GameObject LvlBstTime){
        complete = false;
        PlayerPrefs.SetInt(level, (complete ? 1 : 0));
        levelbtn.SetActive(false);
        LvlBstTime.SetActive(false);
    }
}
