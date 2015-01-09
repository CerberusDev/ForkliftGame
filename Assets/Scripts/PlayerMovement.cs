// Forklift Game
//
// Script author: Maciej Pryc, Alan Kwiatkowski
// Created: 2014/12/20

using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	Rigidbody2D playerRigidBody;

	//////////////// 
	// MOVEMENT PLAYER
	////////////////

	public float movementSpeed = 4300;
	Vector2 movementForce;
	bool bWasLastDirectionRight;
	public float changeDirectionTreshold = 2.0f;

	//////////////// 
	// MOVEMENT FORK
	////////////////

	public float forkSpeed = 2.0f;
	Vector2 forkMovement;
	Transform forkTransform;
	Transform socketForkPosition;
	Transform socketForkTop;
	Transform socketForkBottom;

	//////////////// 
	// STATUS
	////////////////

	int Health = 100;

	//////////////// 
	// METHODS
	////////////////
	/// 
	void Awake () 
	{
		playerRigidBody = GetComponent<Rigidbody2D> ();
		forkTransform = transform.FindChild ("ForkliftFork");
		socketForkPosition = forkTransform.FindChild("SocketForkPosition");
		socketForkTop = transform.FindChild ("SocketForkTop");
		socketForkBottom = transform.FindChild ("SocketForkBottom");

		forkMovement = new Vector2 (0.0f, 0.0f);
		movementForce = new Vector2 (0.0f, 0.0f);

		// make mass center low
		playerRigidBody.centerOfMass.Set(playerRigidBody.centerOfMass.x, 0);
		playerRigidBody.drag = 5;
		playerRigidBody.fixedAngle = true;
	}

	void FixedUpdate () 
	{
		forkMovement.x = 0.0f;
		forkMovement.y = 0.0f;
		movementForce.x = 0.0f;

		float currForkHeight = socketForkPosition.position.y;

		if (Input.GetKey(KeyCode.RightArrow)) 
		{
			if(CanChangeDirection(true))
			{
				movementForce.x = movementSpeed;
				bWasLastDirectionRight = true;
			}
		}

		if (Input.GetKey (KeyCode.LeftArrow)) 
		{
			if(CanChangeDirection(false))
			{
				movementForce.x = -movementSpeed;
				bWasLastDirectionRight = false;
			}
		}

		if (Input.GetKey(KeyCode.UpArrow) && currForkHeight < socketForkTop.position.y) 
		{
			forkMovement.y = forkSpeed * Time.deltaTime;
		}

		if (Input.GetKey (KeyCode.DownArrow) && currForkHeight > socketForkBottom.position.y) 
		{
			forkMovement.y -= forkSpeed * Time.deltaTime;
		}

		//move fork
		forkTransform.Translate(forkMovement);
		// move player
		playerRigidBody.AddForce(movementForce);
	}

	bool CanChangeDirection( bool desiredDirectionRight )
	{
		return Mathf.Abs(playerRigidBody.velocity.x) < changeDirectionTreshold || bWasLastDirectionRight == desiredDirectionRight;
	}
	
	bool IsAlive()
	{
		return Health > 0;
	}

	void TakeDamage(int damage, GameTypes.DamageType dmgType, GameObject instigator)
	{
		Debug.Log ("Damage: " + damage + " type: " +dmgType + " from: " +instigator);

		if (IsAlive ()) 
		{
			Health -= damage;
		}

		if( Health <= 0)
		{
			Died();
		}
	}

	/// <summary>
	/// If called, player forklift died
	/// </summary>
	void Died()
	{
		OnDeath ();
		Destroy (gameObject);
	}

	/// <summary>
	/// Function clears stuff just right before Owner destroy
	/// </summary>
	void OnDeath()
	{
		print ("Im dying!" + gameObject.name);
	}
}
