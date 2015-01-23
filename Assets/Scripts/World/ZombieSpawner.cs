// Forklift Game
//
// Script author: Maciej Pryc
// Created: 2015/01/06

using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour {

	public float meanSpawnInterval = 3.0f;
	public float spawnIntervalDeviation = 1.0f;
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
			
			GameObject zombie = (GameObject) Instantiate (randomZombiePrefab, spawnPoint.position, spawnPoint.rotation);
			float scale = Random.Range(0.85f, 1.15f);
			zombie.transform.localScale = new Vector3(scale, scale);

			Invoke ("SpawnZombie", Random.Range(meanSpawnInterval - spawnIntervalDeviation, 
			                                    meanSpawnInterval + spawnIntervalDeviation));
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
