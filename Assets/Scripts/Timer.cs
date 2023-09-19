using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public static Timer instance; //Timer script instance

    private float startTime;
    public bool isCounting = false;

    public Text timerText; //time UI text
    public float time; //time value

    public Text bestTimeText; //best-time UI text
    public float bestTime; //best-time value
    private String levelName;

    private SpriteRenderer spriteRenderer;
    public Image medal;
    public Sprite goldMedal; // Drag your first sprite here
    public Sprite silverMedal; // Drag your second sprite here
    public Sprite bronzeMedal;
    
	public Text LevelCompleteText;
    public Text LevelCompleteText2;


    private void Awake(){
        instance=this;

        levelName=SceneManager.GetActiveScene().name; //gets the current level name

        if(PlayerPrefs.HasKey("BestTime"+levelName)){ //if such a value exists,
            bestTime = PlayerPrefs.GetFloat("BestTime"+levelName); //get the player preference best time value for the specific level

            //formatting purposes:
            string minutes = ((int) bestTime / 60).ToString("f0");
            string seconds = (bestTime % 60).ToString("f3");
            bestTimeText.text = "Best time: "+minutes.ToString() + "." + seconds.ToString(); //set the best time text to the current best time
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateDisplay();
    }

    // Start/restart the timer from 0
    public void TimerStart()
    {
        startTime = Time.time;
        isCounting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCounting)
        {
            time = Time.time - startTime;
            UpdateDisplay();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            ClearHighScore();
        }
    }

    // Update the timer display
    public void UpdateDisplay()
    {
        string minutes = ((int) time / 60).ToString("f0");
        string seconds = (time % 60).ToString("f3");

        timerText.text = "Time: "+minutes.ToString() + ":" + seconds.ToString();

        minutes = ((int) bestTime / 60).ToString("f0");
        seconds = (bestTime % 60).ToString("f3");
        bestTimeText.text = "Best time: "+minutes.ToString() + ":" + seconds.ToString(); //set the best time text to the current best time
    }

    // Update the best time and the best time display
    public void UpdateBestTime(){
        //grabs the current level name
        levelName=SceneManager.GetActiveScene().name;
        
        Finish(levelName);
    }

    // Stops the timer and saves new times
    public void Finish(String levelName)
    {
        //Update the bestTime value
        LevelCompleteText.text="You completed the level in "+ FormatTime(time) +" seconds.";
        if (bestTime==0){
            bestTime=time;
            LevelCompleteText2.text="Your first best time is "+ FormatTime(bestTime) +"!";
        }else if(time<bestTime){
            bestTime=time;
            LevelCompleteText2.text="You beat your previous best time! Your new best time is "+ FormatTime(bestTime) +"!";
        }else if(time>bestTime){
            LevelCompleteText2.text="You did not beat your previous best time of "+ FormatTime(bestTime);
        }
        
        changeMedal(2, 34, 50);
        changeMedal(3, 60, 120);
        changeMedal(4, 60, 90);
        changeMedal(5, 33, 60);

        //for formatting purposes:
        string minutes = ((int) bestTime / 60).ToString("f0");
        string seconds = (bestTime % 60).ToString("f3");
        bestTimeText.text = "Best time: "+minutes.ToString() + ":" + seconds.ToString();//Update the best time text to the current best time

        //Set the player preference best time to the best time on the specific level
        PlayerPrefs.SetFloat("BestTime"+levelName,bestTime);
    }

    public void changeMedal(int index, int x, int y){
        if(SceneManager.GetActiveScene().buildIndex==index){
            if (bestTime<=x){
                medal.GetComponent<Image>().sprite = goldMedal;
            }else if ((bestTime>x) && (bestTime<=y)){
                medal.GetComponent<Image>().sprite = silverMedal;
            }else if(bestTime>y){
                medal.GetComponent<Image>().sprite = bronzeMedal;
            }
        }
        
    }

    public void ClearHighScore(){
        PlayerPrefs.DeleteKey("BestTime"+levelName);

        bestTime=0;
        string minutes = ((int) bestTime / 60).ToString("f0");
        string seconds = (bestTime % 60).ToString("f3");
        bestTimeText.text = "Best time: "+minutes.ToString() + ":" + seconds.ToString();//Update the best time text to the current best time
    }

    private string FormatTime(float t)
    {
        string minutes = ((int) t / 60).ToString("f0");
        string seconds = (t % 60).ToString("f3");
        return minutes.ToString() + ":" + seconds.ToString(); // Return formatted version of the time
    }
}
