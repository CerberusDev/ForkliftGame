// Forklift Game
//
// Script author: Maciej Pryc, Alan Kwiatkowski
// Created: 2015/01/26

using UnityEngine;
using System.Collections;

public class EndLevelOnCollision : MonoBehaviour 
{
	void OnCollisionEnter2D(Collision2D coll) 
	{
		if( coll.gameObject.tag == "Player")
		{
			coll.gameObject.GetComponent<PlayerMovement>().LevelCompleted();
		}
	}
}
