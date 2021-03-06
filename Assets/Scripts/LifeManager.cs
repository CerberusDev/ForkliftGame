﻿// Forklift Game
//
// Script author: Alan Kwiatkowski
// Created: 2015/01/09

using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour 
{
	HasLife life;
	public int Health;

	Animator forkliftScratchesAnim;

	// hud object
	public GameObject HUDObject;
	// stat script in hud object
	private HUDStats HUD;

	public bool UseFullscreenDamageEffect;

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
	public void TakeDamage(int damage, GameTypes.DamageType dmgType, GameObject instigator, Collider2D damagedPart, Vector2 worldLocation, float momentum )
	{
		life = gameObject.GetComponent<HasLife>();

		if (IsAlive()) 
		{
			//special case
			if (damagedPart.name == "ZombieHeadTop")
				momentum += 2.0f;

			//special case end

			damage = life.ReduceDamage( damage, momentum, dmgType, damagedPart );
			Health -= damage;

			// Notify HUD about damage
			if( gameObject.tag == "Player" && damage > 0)
			{
				if (forkliftScratchesAnim == null)
				{
					forkliftScratchesAnim = gameObject.transform.FindChild("Scratches").GetComponent<Animator>();
				}

				int i;

				if (IsAlive())
				{
					i = (int)((100.0f - Health) / 33.0f);
				}
				else
				{
					i = 3;
				}

				forkliftScratchesAnim.SetInteger("ScratchesIndex", i);

				//init for first time. function Start and Awake did it to early. dunno why
				if( HUDObject != null )
				{
					if( HUD == null )
					{
						HUD = HUDObject.GetComponent<HUDStats> ();
					}
					
					if( HUD != null )
					{
						HUD.playerDamaged(damage);
						
						if( UseFullscreenDamageEffect )
						{
							HUD.PlayDamageEffect();
						}
						else
						{
							// TODO: dodac ekeft obrazen na wozku
							//HUD.damage
						}
					}
				}
			}

			if( !IsAlive() ) 
			{
				life.Died( damagedPart, worldLocation, dmgType );
				//print (damagedPart + " " + dmgType);
			}
		}

		//Debug.Log ("Dmg: " + damage + " type: " + dmgType + " from: " + instigator + "Part: " + damagedPart.name + "Moment: " + momentum );
	}
}
