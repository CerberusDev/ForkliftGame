// Forklift Game
//
// Script author: Maciej Pryc
// Created: 2014/12/20

using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float speed = 6.0f;

	Rigidbody2D playerRigidBody;
	Vector2 inputForce;
	
	void Awake () {
		playerRigidBody = GetComponent<Rigidbody2D> ();
		inputForce = new Vector2 (0.0f, 0.0f);
	}

	void FixedUpdate () {
		inputForce.x = 0.0f;
		inputForce.y = 0.0f;

		if (Input.GetKey(KeyCode.RightArrow)) 
		{
			inputForce.x = 1000.0f;
		}
		if (Input.GetKey (KeyCode.LeftArrow)) 
		{
			inputForce.x -= 1000.0f;
		}

		playerRigidBody.AddForce (inputForce);
	}
}
