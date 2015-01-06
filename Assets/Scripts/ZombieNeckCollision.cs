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
