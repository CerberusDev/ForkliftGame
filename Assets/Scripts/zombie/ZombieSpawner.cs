// Forklift Game
//
// Script author: Maciej Pryc
// Created: 2015/01/06

using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour {

	public float spawnInterval = 3.0f;
	public GameObject[] zombiePrefabs;
	public bool bInitialEnabled = false;

	Transform spawnPoint;
	bool bEnabled;

	void Start () 
	{
		spawnPoint = transform;
		bEnabled = bInitialEnabled;
		SpawnZombie ();
	}

	void SpawnZombie() 
	{
		if (bEnabled)
		{
			int randomZombiePrefabIndex = Random.Range(0, zombiePrefabs.Length);
			GameObject randomZombiePrefab = zombiePrefabs [randomZombiePrefabIndex];
			
			Instantiate (randomZombiePrefab, spawnPoint.position, spawnPoint.rotation);
			
			Invoke ("SpawnZombie", spawnInterval);
		}
	}

	public void Toggle(bool bNewEnabled)
	{
		if (bEnabled != bNewEnabled)
		{
			bEnabled = bNewEnabled;

			if (bEnabled)
				SpawnZombie();
			else
				CancelInvoke("SpawnZombie");
		}
	}
}
