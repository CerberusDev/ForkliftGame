// Forklift Game
//
// Script author: Maciej Pryc
// Created: 2015/01/09

using UnityEngine;
using System.Collections;

public class DestroyOnCollision : MonoBehaviour {
	
	void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log (coll.gameObject);
		Destroy (coll.gameObject);
	}
}
