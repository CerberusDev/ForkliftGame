// Forklift Game
//
// Script author: Maciej Pryc
// Created: 2014/12/20

using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float playerSpeed = 1000.0f;
	public float forkSpeed = 1.0f;

	Rigidbody2D playerRigidBody;
	Transform forkTransform;
	Vector2 inputForce;
	Vector2 forkMovement;
	Transform socketForkPosition;
	Transform socketForkTop;
	Transform socketForkBottom;
	
	void Awake () {
		playerRigidBody = GetComponent<Rigidbody2D> ();
		forkTransform = transform.FindChild ("ForkliftFork");
		socketForkPosition = forkTransform.FindChild("SocketForkPosition");
		socketForkTop = transform.FindChild ("SocketForkTop");
		socketForkBottom = transform.FindChild ("SocketForkBottom");

		inputForce = new Vector2 (0.0f, 0.0f);
		forkMovement = new Vector2 (0.0f, 0.0f);
	}

	void FixedUpdate () {
		inputForce.x = 0.0f;
		inputForce.y = 0.0f;

		forkMovement.x = 0.0f;
		forkMovement.y = 0.0f;

		float currForkHeight = socketForkPosition.position.y;

		if (Input.GetKey(KeyCode.RightArrow)) 
		{
			inputForce.x = playerSpeed;
		}
		if (Input.GetKey (KeyCode.LeftArrow)) 
		{
			inputForce.x -= playerSpeed;
		}
		if (Input.GetKey(KeyCode.UpArrow) && currForkHeight < socketForkTop.position.y) 
		{
			forkMovement.y = forkSpeed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.DownArrow) && currForkHeight > socketForkBottom.position.y) 
		{
			forkMovement.y -= forkSpeed * Time.deltaTime;
		}

		forkTransform.Translate(forkMovement);
		playerRigidBody.AddForce (inputForce);
	}
}
