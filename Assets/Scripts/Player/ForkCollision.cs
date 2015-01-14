// Forklift Game
//
// Script author: Alan Kwiatkowski
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
}
