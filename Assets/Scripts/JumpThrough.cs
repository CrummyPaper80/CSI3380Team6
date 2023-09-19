using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpThrough : MonoBehaviour
{
	bool tr=false;	

    private void OnTriggerEnter2D(Collider2D collision){

		if(collision.tag=="Trigger2"){
			if(GetComponent<BoxCollider2D>().isTrigger==false /*&& tr==false*/){
				//Debug.Log(tr);
				GetComponent<BoxCollider2D>().isTrigger=true;
				tr=true;
				//Debug.Log(tr);
			}else{
				
				//tr=false;
				//Debug.Log(tr);
			}
		}

		if (collision.tag == "Trigger1"){
			if(GetComponent<BoxCollider2D>().isTrigger==true){
				GetComponent<BoxCollider2D>().isTrigger=false;
			}
    	}

	}

}
