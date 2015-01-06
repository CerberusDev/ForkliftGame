// Forklift Game
//
// Script author: Maciej Pryc
// Created: 2015/01/06

using UnityEngine;
using System.Collections;

public class ZombieNeckCollision : MonoBehaviour {

	ZombieCollision zombieCollisionScript;
	
	void Start() {
		zombieCollisionScript = transform.parent.GetComponent<ZombieCollision> ();
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		zombieCollisionScript.OnNeckCollision (coll);
	}
}
