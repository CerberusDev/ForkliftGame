using UnityEngine;
using System.Collections;

public class ProjectileCollision : MonoBehaviour {

	private GameObject Owner;
	private CanAttack ForkliftWeapon;

	// Use this for initialization
	void Start(){}
	
	// Update is called once per frame
	void Update (){}

	public void SetOwner( GameObject inOwner )
	{
		Owner = inOwner;
		ForkliftWeapon = Owner.GetComponent<CanAttack>();
	}

	void OnCollisionEnter2D( Collision2D coll )
	{
		if( coll.gameObject.layer == LayerMask.NameToLayer("Enemy"))
		{
			ForkliftWeapon.GiveDamageTo(coll.gameObject,GameTypes.AttackModes.Secondary, coll.collider, coll.relativeVelocity.magnitude);
			Destroy(gameObject);
		}
	}
}
