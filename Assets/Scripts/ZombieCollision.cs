using UnityEngine;
using System.Collections;

public class ZombieCollision : MonoBehaviour {

	Animator anim;
	ZombieMovement zombieMovementScript;

	void Start () {
		anim = GetComponent<Animator> ();
		zombieMovementScript = GetComponent<ZombieMovement> ();
	}

	public void OnHeadCollision(Collision2D coll) {
		if (zombieMovementScript.IsAlive ()) {
			KillZombie();
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
		zombieMovementScript.OnDeath();
	}
}
