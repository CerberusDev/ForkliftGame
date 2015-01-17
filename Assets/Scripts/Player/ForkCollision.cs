// Forklift Game
//
// Script author: Alan Kwiatkowski, Maciej Pryc
// Created: 2015/01/04

using UnityEngine;
using System.Collections;

public class ForkCollision : MonoBehaviour 
{
	PlayerMovement playerScript;
	
	void Start() 
	{
		playerScript = transform.parent.GetComponent<PlayerMovement> ();
	}
	
	void OnCollisionEnter2D(Collision2D coll) 
	{
		playerScript.OnForkCollision(coll);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Box")
		{
			other.rigidbody2D.mass = 1000.0f;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Box")
		{
			other.rigidbody2D.mass = 50.0f;
		}
	}
}
