using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpdateBT : MonoBehaviour
{
    private String level1="Lvl 1 - Sewers";
    public Text bestTimeTextLvl1; //best-time UI text
    public float bestTimeLvl1; //best-time value

    private String level2="Lvl 2 - Buildings";
    public Text bestTimeTextLvl2; //best-time UI text
    public float bestTimeLvl2; //best-time value

    private String level3="Lvl 3 - Sky";
    public Text bestTimeTextLvl3; //best-time UI text
    public float bestTimeLvl3; //best-time value

    private String level4="Lvl 4 - UFO";
    public Text bestTimeTextLvl4; //best-time UI text
    public float bestTimeLvl4; //best-time value

    //Updates the best time displays on the level select screen
    private void Awake(){

        if(PlayerPrefs.HasKey("BestTime"+level1)){ //if such a value exists,
            bestTimeLvl1 = PlayerPrefs.GetFloat("BestTime"+level1); //get the player preference best time value for the specific level

            //formatting purposes:
            string minutes = ((int) bestTimeLvl1 / 60).ToString("f0");
            string seconds = (bestTimeLvl1 % 60).ToString("f3");
            bestTimeTextLvl1.text = "Best time: "+minutes.ToString() + "." + seconds.ToString(); //set the best time text to the current best time
        }
        if(PlayerPrefs.HasKey("BestTime"+level2)){ //if such a value exists,
            bestTimeLvl2 = PlayerPrefs.GetFloat("BestTime"+level2); //get the player preference best time value for the specific level

            //formatting purposes:
            string minutes = ((int) bestTimeLvl2 / 60).ToString("f0");
            string seconds = (bestTimeLvl2 % 60).ToString("f3");
            bestTimeTextLvl2.text = "Best time: "+minutes.ToString() + "." + seconds.ToString(); //set the best time text to the current best time
        }
        if(PlayerPrefs.HasKey("BestTime"+level3)){ //if such a value exists,
            bestTimeLvl3 = PlayerPrefs.GetFloat("BestTime"+level3); //get the player preference best time value for the specific level

            //formatting purposes:
            string minutes = ((int) bestTimeLvl3 / 60).ToString("f0");
            string seconds = (bestTimeLvl3 % 60).ToString("f3");
            bestTimeTextLvl3.text = "Best time: "+minutes.ToString() + "." + seconds.ToString(); //set the best time text to the current best time
        }
        if(PlayerPrefs.HasKey("BestTime"+level4)){ //if such a value exists,
            bestTimeLvl4 = PlayerPrefs.GetFloat("BestTime"+level4); //get the player preference best time value for the specific level

            //formatting purposes:
            string minutes = ((int) bestTimeLvl4 / 60).ToString("f0");
            string seconds = (bestTimeLvl4 % 60).ToString("f3");
            bestTimeTextLvl4.text = "Best time: "+minutes.ToString() + "." + seconds.ToString(); //set the best time text to the current best time
        }
        
    }



}
