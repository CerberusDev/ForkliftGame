// Forklift Game
//
// Script author: Maciej Pryc
// Created: 2015/01/06

using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour {

	public float spawnInterval = 3.0f;
	public GameObject[] zombiePrefabs;

	Transform spawnPoint;
	float timer;

	void Start () {
		spawnPoint = transform;
		timer = 0.0f;
	}

	void Update () {
		timer -= Time.deltaTime;

		if (timer <= 0.0f) {
			SpawnZombie();
			timer += spawnInterval;
		}
	}

	void SpawnZombie() {
		int randomZombiePrefabIndex = Random.Range(0, zombiePrefabs.Length);
		GameObject randomZombiePrefab = zombiePrefabs [randomZombiePrefabIndex];

		Instantiate (randomZombiePrefab, spawnPoint.position, spawnPoint.rotation);
	}
}
