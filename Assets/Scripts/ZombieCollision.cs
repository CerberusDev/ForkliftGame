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
	Transform socketHeadSpawnPoint;

	float destroyDelay = 10.0f;
	bool bAttack = false;

	void Start () {
		anim = GetComponent<Animator> ();
		zombieMovementScript = GetComponent<ZombieMovement> ();
		socketHeadSpawnPoint = transform.FindChild ("SocketHeadSpawnPoint");
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
			KillZombie(true, coll.relativeVelocity.magnitude);
		}
	}

	void KillZombie(bool bHeadless = false, float headThrowPower = 0.0f) {
		int deadEnemyLayer = LayerMask.NameToLayer("DeadEnemy");

		gameObject.layer = deadEnemyLayer;
		transform.FindChild ("ZombieLegs").gameObject.layer = deadEnemyLayer;
		transform.FindChild ("ZombieHead").gameObject.layer = deadEnemyLayer;
		transform.FindChild ("ZombieNeck").gameObject.layer = deadEnemyLayer;
		transform.FindChild ("ZombieTorso").gameObject.layer = deadEnemyLayer;

		if (bHeadless) {
			anim.SetTrigger ("HeadlessDeath");

			GameObject head = (GameObject)Instantiate(zombieHeadPrefab, 
			                                          socketHeadSpawnPoint.position, 
			                                          socketHeadSpawnPoint.rotation);

			Rigidbody2D headRigidbody2D = head.GetComponent<Rigidbody2D>();
			headRigidbody2D.AddForce(new Vector2(25.0f * headThrowPower, 50.0f * headThrowPower));
			headRigidbody2D.AddTorque(Random.Range(-5.0f, 20.0f));
		} else {
			anim.SetTrigger ("Death");
		}

		zombieMovementScript.EnableMovement (false);

		Destroy (gameObject, destroyDelay);
	}
}
