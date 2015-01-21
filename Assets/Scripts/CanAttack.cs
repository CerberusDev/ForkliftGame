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

	/// <summary>
	/// Handles giving damage to some gameObject.
	/// </summary>
	/// <param name="victim">Who will pain.</param>
	/// <param name="attaackMode">Attack mode</param>
	/// <param name="damagedPart">Which part of victim was damaged</param>
	/// <param name="momentum"> Relative velocity between colliders </param>
	public void GiveDamageTo(GameObject victim, GameTypes.AttackModes attaackMode, Collider2D damagedPart, Vector2 worldLocation, float momentum )
	{
		if ((int)attaackMode >= 0)
		{
			victim.GetComponent<LifeManager>().TakeDamage( attacks[(int)attaackMode].damage, attacks[(int)attaackMode].type, gameObject, damagedPart, worldLocation, momentum );
		}
	}
}
