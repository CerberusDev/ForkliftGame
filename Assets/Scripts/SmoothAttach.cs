// Forklift Game
//
// Script author: Maciej Pryc
// Created: 2015/01/09

using UnityEngine;
using System.Collections;

public class SmoothAttach : MonoBehaviour {

	public GameObject attachBase;

	Transform baseTransform;
	Transform myTransform;
	Vector3 offset;

	void Start() {
		myTransform = transform;
		baseTransform = attachBase.transform;
		offset = new Vector3 (4.0f, 0.0f, -10.0f);
	}

	void Update () {
		myTransform.Translate (-offset);
		Vector3 diff = baseTransform.position - myTransform.position;

		diff /= 8.0f;

		myTransform.position = myTransform.position + diff;
		myTransform.Translate (offset);
	}
}
