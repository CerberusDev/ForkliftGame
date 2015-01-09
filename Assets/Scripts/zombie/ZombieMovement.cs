// Forklift Game
//
// Script author: Maciej Pryc
// Created: 2015/01/03

using UnityEngine;
using System.Collections;

public class ZombieMovement : MonoBehaviour {
	
	public float speed = 1.0f;
	
	Transform zombieTransform;
	Vector2 speedVector;
	bool bShouldMove = true;
	//Vector2 gravityVector;
	
	void Awake () {
		zombieTransform = GetComponent<Transform> ();
		speedVector = new Vector2 (-speed, 0.0f);
		//gravityVector = new Vector2 (0.0f, -1.0f);
	}
	
	void FixedUpdate () {
		//zombieRigidBody.MovePosition (zombieRigidBody.position + speedVector * Time.deltaTime);
		//zombieRigidBody.AddForce (gravityVector * zombieRigidBody.gravityScale);
		//zombieRigidBody.AddForce (speedVector);
		//zombieRigidBody.position += speedVector * Time.deltaTime;
		//zombieRigidBody.MovePosition (zombieRigidBody.position + (speedVector + gravityVector) * Time.deltaTime);
		if (bShouldMove)
			zombieTransform.Translate (speedVector * Time.deltaTime);
	}

	public void EnableMovement(bool newMovementState) {
		bShouldMove = newMovementState;
	}

	public bool ShouldMove() {
		return bShouldMove;
	}
}
