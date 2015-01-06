using UnityEngine;
using System.Collections;

public class ZombieCollision : MonoBehaviour {

	Animator anim;
	ZombieMovement zombieMovementScript;
	bool bAttack;

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

	void KillZombie() {
		int deadEnemyLayer = LayerMask.NameToLayer("DeadEnemy");

		gameObject.layer = deadEnemyLayer;
		transform.FindChild ("ZombieLegs").gameObject.layer = deadEnemyLayer;
		transform.FindChild ("ZombieHead").gameObject.layer = deadEnemyLayer;
		transform.FindChild ("ZombieNeck").gameObject.layer = deadEnemyLayer;
		transform.FindChild ("ZombieTorso").gameObject.layer = deadEnemyLayer;

		anim.SetTrigger ("Death");
		zombieMovementScript.EnableMovement (false);
	}
}
