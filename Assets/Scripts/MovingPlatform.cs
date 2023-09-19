using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform: MonoBehaviour {
	
	//change #f for movement speed
	float dirX, moveSpeed = 10f;
	bool moveRight = true;
	bool moveUp = true;
	bool reset = false;

	//change #f for x to x movement
	void Update () {
		//Sky level
		if(gameObject.name=="MoveRight"){
			if (transform.position.x > 46f)
				moveRight = false;
			if (transform.position.x < -46f)
				moveRight = true;
			if (moveRight)
				transform.position = new Vector2(transform.position.x + 12 * Time.deltaTime, transform.position.y);
			else
				transform.position = new Vector2(transform.position.x - 12 * Time.deltaTime, transform.position.y);
		}
		if(gameObject.name=="ZoomRight"){
			if (transform.position.x >47f)
			{
				reset=true;
			}
			if (reset)
			{
				transform.position = new Vector3(-47f,85f,0f);
				reset=false;
			}
			else
				transform.position = new Vector2(transform.position.x + 20 * Time.deltaTime, transform.position.y);
		}

		if(gameObject.name=="ZoomLeft"){
			if (transform.position.x <-47f)
			{
				reset=true;
			}
			if (reset)
			{
				transform.position = new Vector3(47f,88f,0f);
				reset=false;
			}
			else
				transform.position = new Vector2(transform.position.x - 20 * Time.deltaTime, transform.position.y);
			
		}

		if(gameObject.name=="MoveUp"){
			if (transform.position.y > 116f)
				moveUp = false;
			if (transform.position.y < 102f)
				moveUp = true;
			if (moveUp)
				transform.position = new Vector2(transform.position.x , transform.position.y + moveSpeed * Time.deltaTime);
			else
				transform.position = new Vector2(transform.position.x , transform.position.y - moveSpeed * Time.deltaTime);
		}

		if(gameObject.name=="BirdUp1"){
			if (transform.position.y > 15)
				moveUp = false;
			if (transform.position.y < 5.07)
				moveUp = true;
			if (moveUp)
				transform.position = new Vector2(transform.position.x , transform.position.y + moveSpeed * Time.deltaTime);
			else
				transform.position = new Vector2(transform.position.x , transform.position.y - moveSpeed * Time.deltaTime);
		}

		if(gameObject.name=="BirdUp2"){
			if (transform.position.y > 32)
				moveUp = false;
			if (transform.position.y < 23)
				moveUp = true;
			if (moveUp)
				transform.position = new Vector2(transform.position.x , transform.position.y + moveSpeed * Time.deltaTime);
			else
				transform.position = new Vector2(transform.position.x , transform.position.y - moveSpeed * Time.deltaTime);
		}
		

		if(gameObject.name=="ZigZag"){
			if (transform.position.x > 46f)
				moveRight = false;
			if (transform.position.x < -46f)
				moveRight = true;
			if (moveRight)
				transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
			else
				transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
			if (transform.position.y > 100)
				moveUp = false;
			if (transform.position.y < 90)
				moveUp = true;
			if (moveUp)
				transform.position = new Vector2(transform.position.x , transform.position.y + moveSpeed * Time.deltaTime);
			else
				transform.position = new Vector2(transform.position.x , transform.position.y - moveSpeed * Time.deltaTime);
		}
		//UFO Level
		if(gameObject.name=="Side2Side1"){
			if (transform.position.x > 13f)
				moveRight = false;
			if (transform.position.x < -2.9f)
				moveRight = true;
			if (moveRight)
				transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
			else
				transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
		}
		if(gameObject.name=="Side2Side2"){
			if (transform.position.x > 6f)
				moveRight = false;
			if (transform.position.x < -7f)
				moveRight = true;
			if (moveRight)
				transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
			else
				transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
		}

		if(gameObject.name=="RotaryBlade"){
			if (transform.position.x > 33f)
				moveRight = false;
			if (transform.position.x < 8f)
				moveRight = true;
			if (moveRight)
				transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
			else
				transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
		}
		bool reset1 = false;
		bool reset2 = false;
		bool reset3 = false;
		if(gameObject.name=="Shots1"){
			if (transform.position.x > 3.6f)
				reset1 = true;
			if (reset1)
				transform.position = new Vector3(-16f,43.5f,0f);
			if (moveRight)
				transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
			else
				transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
			if (transform.position.y > 43.5f)
				moveUp = false;
			if (transform.position.y < 40.5f)
				moveUp = true;
			if (moveUp)
				transform.position = new Vector2(transform.position.x , transform.position.y + moveSpeed * Time.deltaTime);
			else
				transform.position = new Vector2(transform.position.x , transform.position.y - moveSpeed * Time.deltaTime);
		}
		if(gameObject.name=="Shots2"){
			if (transform.position.x > 10.5f)
				moveRight = false;
			if (transform.position.x < 5.5f)
				moveRight = true;
			if (moveRight)
				transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
			else
				transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
			if (transform.position.y < 40f)
				reset2=true;
			if (reset2)
				transform.position = new Vector3(8f,50.5f,0f);
			if (moveUp)
				transform.position = new Vector2(transform.position.x , transform.position.y - 3 * Time.deltaTime);
			else
				transform.position = new Vector2(transform.position.x , transform.position.y - 3 * Time.deltaTime);
		}
		if(gameObject.name=="Shots3"){
			if (transform.position.x < 14f)
				reset1 = true;
			if (reset1)
				transform.position = new Vector3(45.5f,44f,0f);
			if (moveRight)
				transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
			else
				transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
			if (transform.position.y > 42f)
				moveUp = false;
			if (transform.position.y < 40f)
				moveUp = true;
			if (moveUp)
				transform.position = new Vector2(transform.position.x , transform.position.y + moveSpeed * Time.deltaTime);
			else
				transform.position = new Vector2(transform.position.x , transform.position.y - moveSpeed * Time.deltaTime);
		}

		if(gameObject.name=="Smasher"){
			if (transform.position.y > 24.54f)
				moveUp = false;
			if (transform.position.y < 19)
				moveUp = true;
			if (moveUp)
				transform.position = new Vector2(transform.position.x , transform.position.y + 3 * Time.deltaTime);
			else
				transform.position = new Vector2(transform.position.x , transform.position.y - 15 * Time.deltaTime);
		}
		
	}
}
