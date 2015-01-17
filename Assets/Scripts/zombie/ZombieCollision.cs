// Forklift Game
//
// Script author: Maciej Pryc
// Created: 2015/01/04

using UnityEngine;
using System.Collections;

public class ZombieCollision : HasLife
{
	public GameObject zombieHeadPrefab;
	public PhysicsMaterial2D deadZombiePhysicalMaterial;

	enum BodyZone {HEAD, NECK, TORSO};

	Animator anim;
	ZombieMovement zombieMovementScript;
	Transform socketHeadSpawnPoint;
	Transform myTransform;
	Rigidbody2D	myRigidbody2D;
	GameObject fork;
	GameObject forklift;

	Vector2 jumpImpulse = new Vector2 (0.0f, 1500.0f);
	float destroyDelay = 10.0f;
	bool bAttack = false;
	bool bPierced = false;

	CanAttack Weapon;

	float lastDamageMomentum;

	void Start () 
	{
		anim = GetComponent<Animator> ();
		zombieMovementScript = GetComponent<ZombieMovement> ();
		socketHeadSpawnPoint = transform.FindChild ("SocketHeadSpawnPoint");
		myTransform = transform;
		myRigidbody2D = rigidbody2D;

		Weapon = GetComponent<CanAttack>();
	}

	private void NotifyAttack()
	{
		Weapon.GiveDamageTo(forklift, GameTypes.AttackModes.Primary, forklift.collider2D, 1.0f);
	}

	public void OnHeadCollision(Collision2D coll) 
	{
		//float hitStrength = coll.relativeVelocity.magnitude;

		//if (Life.IsAlive() && coll.collider.gameObject.tag == "Fork" && IsAMortalWound(hitStrength)) 
		//{
			//KillZombie(BodyZone.HEAD, hitStrength);
		//} 
		//else
		if (coll.collider.gameObject.tag == "Player") 
		{
			bAttack = true;
			anim.SetBool("Attack", true);
			forklift = coll.gameObject;
		}
	}

	public void OnHeadCollisionOff(Collision2D coll) 
	{
		if (bAttack) {
			bAttack = false;
			anim.SetBool("Attack", false);
			forklift = null;
		}
	}

	public void OnHeadTopCollision(Collision2D coll) 
	{
		//if (Life.IsAlive() && coll.collider.gameObject.tag == "Fork") 
		//{
		//	KillZombie(BodyZone.HEAD, 0.0f);
		//}
	}

	public void OnNeckCollision(Collision2D coll) 
	{
		//float hitStrength = coll.relativeVelocity.magnitude;
		//if (Life.IsAlive() && coll.collider.gameObject.tag == "Fork" && IsAMortalWound(hitStrength)) 
		//{
		//	KillZombie(BodyZone.NECK, hitStrength);
		//}
	}

	public void OnTorsoCollision(Collision2D coll) 
	{
		//float hitStrength = coll.relativeVelocity.magnitude;
		//if (Life.IsAlive() && coll.collider.gameObject.tag == "Fork" && IsAMortalWound(hitStrength)) 
		//{
		if( coll.collider.tag == "Fork" )
			fork = coll.collider.gameObject;

		//	KillZombie (BodyZone.TORSO, hitStrength);
		//	Destroy (rigidbody2D);
		//	myTransform.Translate(new Vector2(0.0f, 0.02f));
		//	Invoke ("AttachToFork", Random.Range(0.1f, 0.2f));
		//}
	}

	public void OnLegsCollision(Collision2D coll) 
	{
		if (bPierced) 
		{
			anim.SetTrigger ("PiercedAndSmashed");
			Destroy (gameObject, destroyDelay);
			Deattach();
		}
	}

	public void OnLegsTriggerEnter(Collider2D other)
	{
		Jump ();
	}

	void Jump() 
	{
		myTransform.Translate(new Vector3(0.0f, 0.1f, 0.0f));
		myRigidbody2D.AddForce (jumpImpulse);
	}

	void AttachToFork() 
	{
		myTransform.parent = fork.transform;
	}

	void Deattach() 
	{
		myTransform.parent = null;
	}

	bool IsAMortalWound(float hitStrength) 
	{
		return (hitStrength > 2.0f);	
	}

	void KillZombie(BodyZone woundZone, float hitStrength, GameTypes.DamageType dmgType ) 
	{
		int deadEnemyLayer = LayerMask.NameToLayer("DeadEnemy");

		gameObject.layer = deadEnemyLayer;
		myTransform.FindChild ("ZombieLegs").gameObject.layer = deadEnemyLayer;
		myTransform.FindChild ("ZombieHeadTop").gameObject.layer = deadEnemyLayer;
		myTransform.FindChild ("ZombieHead").gameObject.layer = deadEnemyLayer;
		myTransform.FindChild ("ZombieNeck").gameObject.layer = deadEnemyLayer;
		myTransform.FindChild ("ZombieTorso").gameObject.layer = deadEnemyLayer;

		//bAlive = false;
		zombieMovementScript.EnableMovement (false);

		myTransform.FindChild ("ZombieLegs").GetComponent<CircleCollider2D> ().sharedMaterial = deadZombiePhysicalMaterial;

		if( dmgType == GameTypes.DamageType.Player_Fork)
		{
			switch (woundZone) 
			{
			case BodyZone.HEAD:
				anim.SetTrigger ("Death");

				Destroy (gameObject, destroyDelay);
				break;

			case BodyZone.NECK:
				anim.SetTrigger ("HeadlessDeath");
				
				GameObject head = (GameObject)Instantiate(zombieHeadPrefab, 
				                                          socketHeadSpawnPoint.position, 
				                                          socketHeadSpawnPoint.rotation);
				
				Rigidbody2D headRigidbody2D = head.GetComponent<Rigidbody2D>();
				headRigidbody2D.AddForce(new Vector2(Random.Range(15.0f, 35.0f) * hitStrength, 
				                                     Random.Range(40.0f, 60.0f) * hitStrength));
				headRigidbody2D.AddTorque(Random.Range(-5.0f, 20.0f));

				Destroy (gameObject, destroyDelay);
				break;

			case BodyZone.TORSO:
				anim.SetTrigger ("Pierced");
				bPierced = true;
				break;
			}
		}
		// normal death
		else
		{
			anim.SetTrigger ("Death");
			Destroy (gameObject, destroyDelay);
		}
	}

	/// <summary>
	/// Handles all reducing damage mechanic
	/// </summary>
	/// <returns>The damage.</returns>
	/// <param name="originalDamage">Original damage.</param>
	/// <param name="momentum">Momentum.</param>
	public override int ReduceDamage( int originalDamage, float momentum )
	{
		// saved for Death // soft hax :D
		lastDamageMomentum = momentum;

		// check for deadly wound
		return IsAMortalWound(momentum) ? originalDamage : originalDamage / 10 ;
	}

	/// <summary>
	/// If called, someone just died
	/// </summary>
	/// <param name="finalPunchPart">Where was final punch applied</param>
	public override void Died( Collider2D finalPunchPart, GameTypes.DamageType dmgType )
	{
		//Debug.Log (gameObject.name +  ": i died. part: " + finalPunchPart);
	
		switch (finalPunchPart.name) 
		{
		case "ZombieHeadTop":
			KillZombie(BodyZone.HEAD, lastDamageMomentum, dmgType);
			break;
		case "ZombieHead":
			KillZombie(BodyZone.HEAD, 0.0f, dmgType);
			break;
		case "ZombieNeck":
			KillZombie(BodyZone.NECK, lastDamageMomentum, dmgType);
			break;
		case "ZombieTorso":
			KillZombie (BodyZone.TORSO, lastDamageMomentum, dmgType);
			if( dmgType == GameTypes.DamageType.Player_Fork)
			{
				Destroy (rigidbody2D);
				myTransform.Translate(new Vector2(0.0f, 0.02f));
				Invoke ("AttachToFork", Random.Range(0.1f, 0.2f));
			}
			break;
		case "ZombieLegs":
			//
			break;
		}
	}
}
