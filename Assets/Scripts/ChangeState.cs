using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeState : MonoBehaviour
{
    //private SpriteRenderer m_s;
    SpriteRenderer sr;
    BoxCollider2D  ccollider;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        //m_s = Sprite.GetComponent<SpriteRenderer>();
        ccollider = GetComponent<BoxCollider2D>();
        InvokeRepeating("functia",1f,2f);
    }

    private float nextActionTime = 0.0f;
    public float period = 3.0f;

    public GameObject bolt;
   // public GameObject obj2;
    //public GameObject obj3;

    //true=safe
    //false=unsafe
    //bool c1=true;
	//bool c2=false;
	//bool c3=true;
 
    void functia () {
       if (Time.time > nextActionTime ) {
            nextActionTime += period;
            if(gameObject.tag == "Ground"){
                bolt.GetComponent<SpriteRenderer>().enabled=true;
                gameObject.tag="Spike";
                sr.color = new Color(0.3019608f,0.2784314f,0.2784314f);
            }else{
                gameObject.tag="Ground";
                bolt.GetComponent<SpriteRenderer>().enabled=false;
                sr.color = new Color(0.4811321f,0.4811321f,0.4811321f);
            }
        }
        /*print("Initially:");
            print(c1);
            print(c2);
            print(c3);
            obj2.GetComponent<SpriteRenderer>().color=new Color(0,0,0);
        //obj2.sr.color = new Color(0,0,0);
        StartCoroutine(Change(2f));
        if(c2==false){
            c2=true;
            obj2.tag="Ground";
            obj2.GetComponent<SpriteRenderer>().color=new Color(0.5f,0.5f,0.5f);
        }else{
            obj1.tag="Ground";
            obj1.GetComponent<SpriteRenderer>().color=new Color(0.5f,0.5f,0.5f);
            obj3.tag="Ground";
            obj3.GetComponent<SpriteRenderer>().color=new Color(0.5f,0.5f,0.5f);
            c1=true;
            c3=true;
        }
        print("After 2 seconds:");
            print(c1);
            print(c2);
            print(c3);
        StartCoroutine(Change(3f));
        if(c1==true && c3==true){
            obj1.tag="Spike";
            obj1.GetComponent<SpriteRenderer>().color = new Color(0,0,0);
            obj3.tag="Spike";
            obj3.GetComponent<SpriteRenderer>().color = new Color(0,0,0);
            c1=false;
            c3=false;
        }else{
            c2=true;
            obj2.tag="Ground";
            obj2.GetComponent<SpriteRenderer>().color=new Color(0.5f,0.5f,0.5f);
        }
         print("After 3 seconds:");
            print(c1);
            print(c2);
            print(c3);
        


       /* if(gameObject.name=="ChangeStateObstacle1"){
            StartCoroutine(Change(5f));
            if(gameObject.tag == "Ground"){
                gameObject.tag="Spike";
                sr.color = new Color(0,0,0);
            }else{
                gameObject.tag="Ground";
                sr.color = new Color(0.5f,0.5f,0.5f);
            }
        }
        if(gameObject.name=="ChangeStateObstacle2"){
            StartCoroutine(Change(3f));
            if(gameObject.tag == "Spike"){
                gameObject.tag="Ground";
                sr.color = new Color(0.5f,0.5f,0.5f);
            }else{
                gameObject.tag="Spike";
                sr.color = new Color(0,0,0);
            }
        }
        if(gameObject.name=="ChangeStateObstacle3"){
            StartCoroutine(Change(1f));
            if(gameObject.tag == "Ground"){
                gameObject.tag="Spike";
                sr.color = new Color(0,0,0);
            }else{
                gameObject.tag="Ground";
                sr.color = new Color(0.5f,0.5f,0.5f);
            }
        }*/



    }


    private IEnumerator Change(float seconds)
    {
	    yield return new WaitForSeconds(seconds); //wait a number of seconds
		
    }

}


