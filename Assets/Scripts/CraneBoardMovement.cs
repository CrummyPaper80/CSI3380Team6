using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneBoardMovement:MonoBehaviour {
	
	//change #f for movement speed
	float dirX, moveSpeed = 4f;
	bool moveRight = true;

	//change #f for x to x movement
	void Update () {
		if (transform.position.x > 14.7f)
			moveRight = false;
		if (transform.position.x < -11.11f)
			moveRight = true;
		if (moveRight)
			transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
		else
			transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
	}
}
