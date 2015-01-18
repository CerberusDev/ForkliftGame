// Forklift Game
//
// Script author: Maciej Pryc
// Created: 2015/01/03

using UnityEngine;
using System.Collections;

public class ZombieMovement : MonoBehaviour {
	
	public float speed = 1.0f;
	
	Rigidbody2D myRigidbody2D;
	bool bShouldMove = true;
	
	void Awake () {
		myRigidbody2D = GetComponent<Rigidbody2D> ();
	}
	
	void FixedUpdate () {
		if (bShouldMove && myRigidbody2D.velocity.x <= 0.0f && myRigidbody2D.velocity.x > -speed)
			myRigidbody2D.velocity = new Vector2(-speed, myRigidbody2D.velocity.y);
	}

	public void EnableMovement(bool newMovementState) {
		bShouldMove = newMovementState;
	}

	public bool ShouldMove() {
		return bShouldMove;
	}
}
