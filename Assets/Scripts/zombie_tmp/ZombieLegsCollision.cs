// Forklift Game
//
// Script author: Maciej Pryc
// Created: 2015/01/09

using UnityEngine;
using System.Collections;

public class ZombieLegsCollision : MonoBehaviour {

	ZombieCollision zombieCollisionScript;
	Transform groundCheck;
	bool bGrounded;

	void Start() {
		zombieCollisionScript = transform.parent.GetComponent<ZombieCollision> ();
		groundCheck = transform.FindChild ("GroundCheck");
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		zombieCollisionScript.OnLegsCollision (coll);
		UpdateGroundedState ();
	}

	void OnCollisionExit2D(Collision2D coll) {
		UpdateGroundedState ();
	}

	void UpdateGroundedState() {
		Collider2D coll = Physics2D.OverlapPoint (groundCheck.position);
		bGrounded = coll != null && coll.gameObject.tag == "Ground";
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (bGrounded && other.gameObject.tag == "Fork")
			zombieCollisionScript.OnLegsTriggerEnter (other);
	}
}
