// Forklift Game
//
// Script author: Maciej Pryc
// Created: 2015/01/03

using UnityEngine;
using System.Collections;

public class ZombieMovement : MonoBehaviour {
	
	public float speed = 1.0f;
	
	Rigidbody2D zombieRigidBody;
	Vector2 speedVector;
	//Vector2 gravityVector;
	
	void Awake () {
		zombieRigidBody = GetComponent<Rigidbody2D> ();
		speedVector = new Vector2 (-speed, 0.0f);
		//gravityVector = new Vector2 (0.0f, -1.0f);
	}
	
	void FixedUpdate () {
		//zombieRigidBody.MovePosition (zombieRigidBody.position + speedVector * Time.deltaTime);
		//zombieRigidBody.AddForce (gravityVector * zombieRigidBody.gravityScale);
		//zombieRigidBody.AddForce (speedVector);
		//zombieRigidBody.position += speedVector * Time.deltaTime;
		//zombieRigidBody.MovePosition (zombieRigidBody.position + (speedVector + gravityVector) * Time.deltaTime);
		transform.Translate (speedVector * Time.deltaTime);
	}
}
