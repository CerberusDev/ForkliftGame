// Forklift Game
//
// Script author: Alan Kwiatkowski
// Created: 2015/01/09

using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour 
{
	HasLife life;
	public int Health;

	// Use this for initialization
	void Start (){}
	
	// Update is called once per frame
	void Update (){}
	
	public bool IsAlive()
	{
		return Health > 0;
	}

	/// <summary>
	/// Handles given damage
	/// </summary>
	/// <param name="damage">Amount of damage</param>
	/// <param name="dmgType">Type of damage</param>
	/// <param name="instigator">Who was responsible for this madness</param>
	/// <param name="damagedPart">Which part was damaged</param>
	/// <param name="momentum">Relative velocity between colliders</param>
	public void TakeDamage(int damage, GameTypes.DamageType dmgType, GameObject instigator, Collider2D damagedPart, float momentum )
	{
		life = gameObject.GetComponent<HasLife>();

		if (IsAlive()) 
		{
			//special case
			if (damagedPart.name == "ZombieHeadTop")
				momentum += 2.0f;

			if( damagedPart.name == "ZombieLegs")
				damage = 0;
			//special case end

			damage = life.ReduceDamage( damage, momentum );
			Health -= damage;
		}

		Debug.Log ("Dmg: " + damage + " type: " + dmgType + " from: " + instigator + "Part: " + damagedPart.name + "Moment: " + momentum );
		
		if( !IsAlive() ) 
		{
			life.Died( damagedPart );
		}
	}
}
