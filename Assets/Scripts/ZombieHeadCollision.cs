using UnityEngine;
using System.Collections;

public class ZombieHeadCollision : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log (coll.gameObject);
	}
}
