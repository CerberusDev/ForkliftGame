// Forklift Game
//
// Script author: Maciej Pryc
// Created: 2015/01/04

using UnityEngine;
using System.Collections;

public class ZombieCollision : MonoBehaviour {

	public GameObject zombieHeadPrefab;

	Animator anim;
	ZombieMovement zombieMovementScript;

	float destroyDelay = 10.0f;
	bool bAttack = false;

	void Start () {
		anim = GetComponent<Animator> ();
		zombieMovementScript = GetComponent<ZombieMovement> ();
	}

	public void OnHeadCollision(Collision2D coll) {
		if (coll.collider.gameObject.tag == "Fork") {
			KillZombie();
		} else if (coll.collider.gameObject.tag == "Player") {
			bAttack = true;
			anim.SetBool("Attack", true);
		}
	}

	public void OnHeadCollisionOff(Collision2D coll) {
		if (bAttack) {
			bAttack = false;
			anim.SetBool("Attack", false);
		}
	}

	public void OnNeckCollision(Collision2D coll) {
		if (coll.collider.gameObject.tag == "Fork") {
			KillZombie(true);
		}
	}

	void KillZombie(bool bHeadless = false) {
		int deadEnemyLayer = LayerMask.NameToLayer("DeadEnemy");

		gameObject.layer = deadEnemyLayer;
		transform.FindChild ("ZombieLegs").gameObject.layer = deadEnemyLayer;
		transform.FindChild ("ZombieHead").gameObject.layer = deadEnemyLayer;
		transform.FindChild ("ZombieNeck").gameObject.layer = deadEnemyLayer;
		transform.FindChild ("ZombieTorso").gameObject.layer = deadEnemyLayer;

		if (bHeadless) {
			anim.SetTrigger ("HeadlessDeath");
			GameObject head = (GameObject)Instantiate(zombieHeadPrefab, transform.position, transform.rotation);
			head.GetComponent<Rigidbody2D>().AddForce(new Vector2(150.0f, 300.0f));
		} else {
			anim.SetTrigger ("Death");
		}

		zombieMovementScript.EnableMovement (false);

		Destroy (gameObject, destroyDelay);
	}
}
