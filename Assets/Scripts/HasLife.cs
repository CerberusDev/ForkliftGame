// Forklift Game
//
// Script author: Alan Kwiatkowski
// Created: 2015/01/09

using UnityEngine;
using System.Collections;

public class HasLife : MonoBehaviour 
{
	public int Health;

	// Use this for initialization
	void Start (){}
	
	// Update is called once per frame
	void Update (){}
	
	public bool IsAlive()
	{
		return Health > 0;
	}
	
	public void TakeDamage(int damage, GameTypes.DamageType dmgType, GameObject instigator)
	{
		Debug.Log ("Damage: " + damage + " type: " +dmgType + " from: " +instigator);
		
		if (IsAlive ()) 
		{
			Health -= damage;
		}
		
		if( Health <= 0)
		{
			Died();
		}
	}
	
	/// <summary>
	/// If called, player forklift died
	/// </summary>
	void Died()
	{
		Destroy (gameObject);
	}
}
