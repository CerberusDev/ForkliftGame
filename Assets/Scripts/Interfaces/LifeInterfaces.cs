// Forklift Game
//
// Script author: Alan Kwiatkowski
// Created: 2015/01/14

using UnityEngine;
using System.Collections;

/// <summary>
/// Has life, interface for multi prefab Death functionality
/// </summary>
public class HasLife : MonoBehaviour
{
	/// <summary>
	/// If called, someone just died
	/// </summary>
	/// <param name="finalPunchPart">Where was final punch applied</param>
	public virtual void Died( Collider2D finalPunchPart, GameTypes.DamageType dmgType ){}

	/// <summary>
	/// Handles all reducing damage mechanic
	/// </summary>
	/// <returns>The damage.</returns>
	public virtual int ReduceDamage ( int originalDamage, float momentum, GameTypes.DamageType dmgType, Collider2D damagedPart )
	{
		return originalDamage;
	}

	
}