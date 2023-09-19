using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatMovement:MonoBehaviour {
	
	//change #f for movement speed
	float dirX, moveSpeed = 8f;
	bool moveRight = true;

	//change #f for x to x movement
	void Update () {
		if (transform.position.x > 48.44f)
			moveRight = false;
		if (transform.position.x < 1.91f)
			moveRight = true;
		if (moveRight)
			transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
		else
			transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
	}
}
