// Forklift Game
//
// Script author: Alan Kwiatkowski, Maciej Pryc
// Created: 2015/01/17

using UnityEngine;
using System.Collections;

public class ProjectileCollision : MonoBehaviour 
{
	public AudioClip projectileHitSound;

	private GameObject Owner;
	private CanAttack ForkliftWeapon;

	public void SetOwner( GameObject inOwner )
	{
		Owner = inOwner;
		ForkliftWeapon = Owner.GetComponent<CanAttack>();
	}

	void OnCollisionEnter2D( Collision2D coll )
	{
		if( coll.gameObject.layer == LayerMask.NameToLayer("Enemy"))
		{
			ForkliftWeapon.GiveDamageTo(coll.gameObject,GameTypes.AttackModes.Secondary, coll.collider, coll.contacts[0].point, coll.relativeVelocity.magnitude);
			Destroy(gameObject);
		}
		else
		{
			if (coll.relativeVelocity.magnitude > 1.0f)
			{
				AudioSource.PlayClipAtPoint(projectileHitSound, transform.position);
			}
		}
	}
}
