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
		//foreach (Collider c in GetComponentInChildren<Collider>())
		//	c.enabled = false;

		//transform.FindChild ("ZombieHead").GetComponent<Collider2D> ().enabled = false;
		//transform.FindChild ("ZombieNeck").GetComponent<Collider2D> ().enabled = false;
		//transform.FindChild ("ZombieTorso").GetComponent<Collider2D> ().enabled = false;
		//transform.FindChild ("ZombieLegs").GetComponent<Collider2D> ().enabled = false;

		anim.SetTrigger ("Death");
		zombieMovementScript.OnDeath();
	}
}
