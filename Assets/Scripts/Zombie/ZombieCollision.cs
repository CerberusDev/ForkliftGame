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
	public AudioClip zombieAttackSound;
	public AudioClip[] zombieRoarSounds;
	public AudioClip[] zombieDeathSounds;
	
	float roarSoundInterval = 5.0f;

	enum BodyZone 
	{
		headTop,
		head,
		neck,
		torso
	};

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

	public ParticleSystem PSBloodTemplate;
	ParticleSystem PSBlood;

	void Start () 
	{
		anim = GetComponent<Animator> ();
		zombieMovementScript = GetComponent<ZombieMovement> ();
		socketHeadSpawnPoint = transform.FindChild ("SocketHeadSpawnPoint");
		myTransform = transform;
		myRigidbody2D = rigidbody2D;

		Weapon = GetComponent<CanAttack>();

		Invoke ("PlayRoarSound", roarSoundInterval);
	}
	
	void PlayRoarSound()
	{
		AudioSource.PlayClipAtPoint (zombieRoarSounds[Random.Range(0, zombieRoarSounds.Length)], myTransform.position);
		Invoke ("PlayRoarSound", roarSoundInterval);
	}

	private void NotifyAttack()
	{
		if (IsInvoking("PlayRoarSound"))
		{
			CancelInvoke("PlayRoarSound");
			Invoke ("PlayRoarSound", roarSoundInterval);
		}

		Weapon.GiveDamageTo(forklift, GameTypes.AttackModes.Primary, forklift.collider2D, transform.position, 1.0f);
		AudioSource.PlayClipAtPoint (zombieAttackSound, myTransform.position);
	}

	public void OnHeadCollision(Collision2D coll) 
	{
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
	}

	public void OnNeckCollision(Collision2D coll) 
	{
	}

	public void OnTorsoCollision(Collision2D coll) 
	{
		if( coll.collider.tag == "Fork" )
		{
			fork = coll.collider.gameObject;
		}
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

		CancelInvoke ("PlayRoarSound");
		AudioSource.PlayClipAtPoint (zombieDeathSounds[Random.Range(0, zombieDeathSounds.Length)], myTransform.position);

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
			case BodyZone.headTop:
				anim.SetTrigger ("Death");
				
				Destroy (gameObject, destroyDelay);
				break;

			case BodyZone.head:
				anim.SetTrigger ("Death");

				Destroy (gameObject, destroyDelay);
				break;

			case BodyZone.neck:
				anim.SetTrigger ("HeadlessDeath");
				
				GameObject head = (GameObject)Instantiate(zombieHeadPrefab, 
				                                          socketHeadSpawnPoint.position, 
				                                          socketHeadSpawnPoint.rotation);
				
				Rigidbody2D headRigidbody2D = head.GetComponent<Rigidbody2D>();

				//attach blood particle
				PSBlood.transform.parent = head.transform;

				headRigidbody2D.AddForce(new Vector2(Random.Range(15.0f, 35.0f) * hitStrength, 
				                                     Random.Range(40.0f, 60.0f) * hitStrength));
				headRigidbody2D.AddTorque(Random.Range(-5.0f, hitStrength * hitStrength)); // x2

				Destroy (gameObject, destroyDelay);
				break;

			case BodyZone.torso:
				anim.SetTrigger ("Pierced");
				bPierced = true;
				break;
			}
		}
		// death from box
		else if( dmgType == GameTypes.DamageType.World_Box )
		{
			anim.SetTrigger ("Death");
			Destroy (gameObject, destroyDelay);
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
	public override int ReduceDamage( int originalDamage, float momentum, GameTypes.DamageType dmgType, Collider2D damagedPart )
	{
		// saved for Death // soft hax :D
		lastDamageMomentum = momentum;

		// zombie jumped on Fork
		if( damagedPart.name == "ZombieLegs")
		{
			originalDamage = 0;
		}

		// box can damage only HeadTop of zombie
		if( dmgType == GameTypes.DamageType.World_Box && damagedPart.name != "ZombieHeadTop" )
		{
			originalDamage = 0;
		}

		// check for deadly wound
		return IsAMortalWound(momentum) ? originalDamage : originalDamage / 10 ;
	}

	/// <summary>
	/// If called, someone just died
	/// </summary>
	/// <param name="finalPunchPart">Where was final punch applied</param>
	public override void Died( Collider2D finalPunchPart, Vector2 worldLocation, GameTypes.DamageType dmgType )
	{
		//Debug.Log (gameObject.name +  ": i died. part: " + finalPunchPart);

		PSBlood = Instantiate(PSBloodTemplate, worldLocation, Quaternion.identity ) as ParticleSystem;

		switch (finalPunchPart.name) 
		{
		case "ZombieHeadTop":
			PSBlood.transform.parent = socketHeadSpawnPoint.transform;
			//zombieMovementScript.RotateZombieOnDeath();
			KillZombie(BodyZone.headTop, lastDamageMomentum, dmgType);
			break;
		case "ZombieHead":
			PSBlood.transform.parent = transform;
			//zombieMovementScript.RotateZombieOnDeath();
			KillZombie(BodyZone.head, 0.0f, dmgType);
			break;
		case "ZombieNeck":
			// blood attached in KillZombie function
			KillZombie(BodyZone.neck, lastDamageMomentum, dmgType);
			break;
		case "ZombieTorso":
			KillZombie (BodyZone.torso, lastDamageMomentum, dmgType);
			if( dmgType == GameTypes.DamageType.Player_Fork)
			{
				Destroy (rigidbody2D);
				myTransform.Translate(new Vector2(0.0f, 0.02f));

				// attach and slow down blood flow
				PSBlood.transform.parent = transform;
				PSBlood.playbackSpeed = 0.5f;
				PSBlood.gravityModifier = 3.0f;

				Invoke ("AttachToFork", Random.Range(0.1f, 0.2f));
			}
			break;
		case "ZombieLegs":
			//
			break;
		}
	}
}
