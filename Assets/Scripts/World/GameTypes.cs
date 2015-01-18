// Forklift Game
//
// Script author: Alan Kwiatkowski
// Created: 2015/01/09

using UnityEngine;
using System.Collections;

public class GameTypes : MonoBehaviour {

	public enum DamageType
	{
		Default,
		Zombie_Melee,
		Zombie_Pistol,
		Player_Tool,
		Player_Fork,
		World_Barrel,
		World_Box
	}

	public enum AttackModes
	{
		Primary,
		Secondary
	}

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
