// Forklift Game
//
// Script author: Maciej Pryc
// Created: 2015/01/06

using UnityEngine;
using System.Collections;

public class DelayedDestroy : MonoBehaviour {

	public float destroyDelay = 10.0f;

	void Start () {
		Destroy (gameObject, destroyDelay);
	}
}
