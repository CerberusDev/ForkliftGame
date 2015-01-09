// Forklift Game
//
// Script author: Maciej Pryc
// Created: 2015/01/04

using UnityEngine;
using System.Collections;

public class ZombieCollision : MonoBehaviour {

	public GameObject zombieHeadPrefab;

	enum BodyZone {HEAD, NECK, TORSO};

	Animator anim;
	ZombieMovement zombieMovementScript;
	Transform socketHeadSpawnPoint;
	Transform myTransform;
	GameObject fork;

	float destroyDelay = 10.0f;
	bool bAttack = false;
	bool bPierced = false;
	bool bAlive = true;

	void Start () {
		anim = GetComponent<Animator> ();
		zombieMovementScript = GetComponent<ZombieMovement> ();
		socketHeadSpawnPoint = transform.FindChild ("SocketHeadSpawnPoint");
		myTransform = transform;
	}

	public void OnHeadCollision(Collision2D coll) {
		float hitStrength = coll.relativeVelocity.magnitude;
		if (bAlive && coll.collider.gameObject.tag == "Fork" && IsAMortalWound(hitStrength)) {
			KillZombie(BodyZone.HEAD, hitStrength);
		} else if (coll.collider.gameObject.tag == "Player") {
			bAttack = true;
			anim.SetBool("Attack", true);
		}
	}

	public void OnHeadTopCollision(Collision2D coll) {
		if (bAlive && coll.collider.gameObject.tag == "Fork") {
			KillZombie(BodyZone.HEAD, 0.0f);
		}
	}

	public void OnHeadCollisionOff(Collision2D coll) {
		if (bAttack) {
			bAttack = false;
			anim.SetBool("Attack", false);
		}
	}

	public void OnNeckCollision(Collision2D coll) {
		float hitStrength = coll.relativeVelocity.magnitude;
		if (bAlive && coll.collider.gameObject.tag == "Fork" && IsAMortalWound(hitStrength)) {
			KillZombie(BodyZone.NECK, hitStrength);
		}
	}

	public void OnTorsoCollision(Collision2D coll) {
		float hitStrength = coll.relativeVelocity.magnitude;
		if (bAlive && coll.collider.gameObject.tag == "Fork" && IsAMortalWound(hitStrength)) {
			fork = coll.collider.gameObject;

			KillZombie (BodyZone.TORSO, hitStrength);
			Destroy (rigidbody2D);
			myTransform.Translate(new Vector2(0.0f, 0.02f));
			Invoke ("AttachToFork", Random.Range(0.1f, 0.2f));
		}
	}

	public void OnLegsCollision(Collision2D coll) {
		if (bPierced) {
			anim.SetTrigger ("PiercedAndSmashed");
			Destroy (gameObject, destroyDelay);
			Deattach();
		}
	}

	void AttachToFork() {
		myTransform.parent = fork.transform;
	}

	void Deattach() {
		myTransform.parent = null;
	}

	bool IsAMortalWound(float hitStrength) {
		return (hitStrength > 1.0f);	
	}

	void KillZombie(BodyZone woundZone, float hitStrength) {
		int deadEnemyLayer = LayerMask.NameToLayer("DeadEnemy");

		gameObject.layer = deadEnemyLayer;
		myTransform.FindChild ("ZombieLegs").gameObject.layer = deadEnemyLayer;
		myTransform.FindChild ("ZombieHeadTop").gameObject.layer = deadEnemyLayer;
		myTransform.FindChild ("ZombieHead").gameObject.layer = deadEnemyLayer;
		myTransform.FindChild ("ZombieNeck").gameObject.layer = deadEnemyLayer;
		myTransform.FindChild ("ZombieTorso").gameObject.layer = deadEnemyLayer;

		bAlive = false;
		zombieMovementScript.EnableMovement (false);

		switch (woundZone) {
		case BodyZone.HEAD:
			anim.SetTrigger ("Death");

			Destroy (gameObject, destroyDelay);
			break;

		case BodyZone.NECK:
			anim.SetTrigger ("HeadlessDeath");
			
			GameObject head = (GameObject)Instantiate(zombieHeadPrefab, 
			                                          socketHeadSpawnPoint.position, 
			                                          socketHeadSpawnPoint.rotation);
			
			Rigidbody2D headRigidbody2D = head.GetComponent<Rigidbody2D>();
			headRigidbody2D.AddForce(new Vector2(Random.Range(15.0f, 35.0f) * hitStrength, 
			                                     Random.Range(40.0f, 60.0f) * hitStrength));
			headRigidbody2D.AddTorque(Random.Range(-5.0f, 20.0f));

			Destroy (gameObject, destroyDelay);
			break;

		case BodyZone.TORSO:
			anim.SetTrigger ("Pierced");
			bPierced = true;
			break;
		}
	}
}
