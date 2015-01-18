// Forklift Game
//
// Script author: Maciej Pryc
// Created: 2015/01/18

using UnityEngine;
using System.Collections;

public class ToggleZombieSpawnPoint : MonoBehaviour 
{
	public GameObject zombieSpawner;
	public bool bEnableSpawnerOnCollision;

	ZombieSpawner zombieSpawnerScript;

	void Start()
	{
		zombieSpawnerScript = zombieSpawner.GetComponent<ZombieSpawner> ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			zombieSpawnerScript.Toggle(bEnableSpawnerOnCollision);
		}
	}
}
