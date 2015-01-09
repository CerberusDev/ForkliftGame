// Forklift Game
//
// Script author: Alan Kwiatkowski
// Created: 2015/01/09

using UnityEngine;
using System.Collections;

public class CanAttack : MonoBehaviour 
{
	[System.Serializable]
	public class damageInfo
	{
		public GameTypes.DamageType type;
		public int damage;
	}
	public damageInfo[] attacks;

	void Start(){}
	public void Init(){}
	void Update() {}

	public void GiveDamageTo(GameTypes.AttackModes attaackMode, GameObject victim)
	{
		if ((int)attaackMode >= 0)
		{
			// cos tu nie tak z tym. zerkne jak wroce
			victim.GetComponent<HasLife>().TakeDamage( attacks[(int)attaackMode].damage, attacks[(int)attaackMode].type, gameObject);
		}
	}
}
