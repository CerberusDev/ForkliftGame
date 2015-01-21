// Forklift Game
//
// Script author: Alan Kwiatkowski
// Created: 2015/01/18

using UnityEngine;
using System.Collections;

public class BoxCollision : MonoBehaviour 
{
	CanAttack Weapon;

	void Start () 
	{
		Weapon = gameObject.GetComponent<CanAttack>();
	}

	void OnCollisionEnter2D(Collision2D coll) 
	{
		// is Zombie
		if( coll.collider.gameObject.tag == "ZombieMelee" || coll.gameObject.tag == "ZombieMelee")
		{
			if( Weapon != null )
			{
				Weapon.GiveDamageTo(coll.gameObject, GameTypes.AttackModes.Primary, coll.collider, coll.contacts[0].point, coll.relativeVelocity.magnitude );
			}
		}
	}
}
