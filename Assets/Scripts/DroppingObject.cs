using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingObject: MonoBehaviour {
	
	//change #f for movement speed
	float dirY, moveSpeed = 7f;
	bool reset = false;

	//change #f for x to x movement
	void Update () {
		if(gameObject.name == "FallingTrash")
		{
		//	dirY= 20f;
			//moveSpeed=20f;
			if (transform.position.y <52.57f)
			{
				reset=true;
			}
			if (reset)
			{
				transform.position = new Vector3(46.51f,85.29f,0f);
				reset=false;
			}
			else
				transform.position = new Vector2(transform.position.x, transform.position.y - 20 * Time.deltaTime);
		}else if (gameObject.name == "FallingSpike1"){
			if (transform.position.y <7.5f)
			{
				reset=true;
			}
			if (reset)
			{
				transform.position = new Vector3(11.54004f,17.52f,0f);
				reset=false;
			}
			else
				transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime);
		}else if(gameObject.name == "FallingSpike2"){
			if (transform.position.y <7.5f)
			{
				reset=true;
			}
			if (reset)
			{
				transform.position = new Vector3(20.043f,17.49f,0f);
				reset=false;
			}
			else
				transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime);	
		}else if(gameObject.name == "FallingSpike3"){
			if (transform.position.y <7.5f)
			{
				reset=true;
			}
			if (reset)
			{
				transform.position = new Vector3(28.544f,17.49f,0f);
				reset=false;
			}
			else
				transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime);
			}
		}
		
		
		
		
}
