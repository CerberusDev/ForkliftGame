// Forklift Game
//
// Script author: Alan Kwiatkowski
// Created: 2015/01/09

using UnityEngine;
using System.Collections;

public class CanAttack : MonoBehaviour {

	private HasLife victimLife;

	public struct damageInfo
	{
		public GameTypes.DamageType type;
		public int damage;
	}

	private damageInfo[] attacks;

	// Use this for initialization
	void Start () 
	{
		attacks = new damageInfo[2];

		attacks [0].type = GameTypes.DamageType.Player_Fork;
		attacks [0].damage = 100;
	}

	public void Init()
	{
		Start ();
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void GiveDamageTo(int attaackMode, GameObject victim)
	{
		if (attaackMode >= 0)
		{
			//victimLife = victim.GetComponent("HasLife");


			//victimLife.TakeDamage(attacks[attaackMode].damage, attacks[attaackMode].type, gameObject);
		}
	}
}
