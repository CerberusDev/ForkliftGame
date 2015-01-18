// Forklift Game
//
// Script author: Maciej Pryc
// Created: 2015/01/09

using UnityEngine;
using System.Collections;

public class ZombieTorsoCollision : MonoBehaviour {

	ZombieCollision zombieCollisionScript;
	
	void Start() {
		zombieCollisionScript = transform.parent.GetComponent<ZombieCollision> ();
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		zombieCollisionScript.OnTorsoCollision (coll);
	}
}
