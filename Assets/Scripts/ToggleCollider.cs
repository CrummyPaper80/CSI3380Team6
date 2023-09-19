using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCollider : MonoBehaviour
{
    //public GameObject player;

    BoxCollider2D  ccollider;
    SpriteRenderer sr;
    
    // Start is called before the first frame update
    void Start()
    {
        ccollider = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

	private float nextActionTime = 0.0f;
	public float period = 2.0f;
	private bool fallthru=false;
 
	void Update () {
    	//ccollider.enabled = ccollider.enabled;
    	if (Time.time > nextActionTime ) {
        		nextActionTime += period;
        	if(ccollider.enabled == true){
				if (string.Equals(gameObject.name, "FallThrough")){
            		fallthru=true;
				}
				if(!fallthru){
					ccollider.enabled=false;
				}
            	sr.color = new Color(123,123,123,0.3f);
        	}else{
				if (string.Equals(gameObject.name, "FallThrough")){
            		fallthru=true;
				}
				if(!fallthru){
					ccollider.enabled=true;
				}
            	sr.color = new Color(123,123,123,1);
        	}
    	}    
	}

}
