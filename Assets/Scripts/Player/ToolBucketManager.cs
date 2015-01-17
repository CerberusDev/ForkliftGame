// Forklift Game
//
// Script author: Alan Kwiatkowski
// Created: 2015/01/16

using UnityEngine;
using System.Collections;

public class ToolBucketManager : MonoBehaviour {

	int ToolCount;
	public int ToolMax;
	public float ToolRespawnTime;
	public GameObject ToolImage;

	// throw mechanic
	public float ThrowVectorX;
	public GameObject Prefab;
	public Transform SpawnPoint;

	float ThrowAngleTopValue;
	float ThrowAngleBottomValue;


	PlayerMovement Forklift;
	GameObject Projectile;

	// Use this for initialization
	void Start () 
	{
		ToolCount = ToolMax;
		ThrowAngleTopValue = 8.0f;
		ThrowAngleBottomValue = -4.0f;
		Forklift = gameObject.GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	/// <summary>
	/// Determines whether this instance can throw tool.
	/// </summary>
	/// <returns><c>true</c> if this instance can throw tool; otherwise, <c>false</c>.</returns>
	public bool CanThrowTool()
	{
		return ToolCount > 0;
	}

	/// <summary>
	/// Gets the current tool count.
	/// </summary>
	/// <returns>The tool count.</returns>
	public int GetToolCount()
	{
		return ToolCount;
	}

	void RespawnTool()
	{
		++ToolCount;

		if( Forklift != null )
		{
			Forklift.UpdateToolCount();
		}

		// invoke again to replenish all tools in same interval.
		if( ToolCount < ToolMax )
		{
			Invoke("RespawnTool", ToolRespawnTime);
		}

		if(ToolCount > ToolMax )
		{
			Debug.LogError("Too much tools. More function calls then invokes");
		}
	}

	/// <summary>
	/// Throws the tool immediately.
	/// </summary>
	public void ThrowTool()
	{
		if( Forklift != null )
		{
			Projectile = Instantiate(Prefab,SpawnPoint.position, SpawnPoint.rotation) as GameObject;
			Projectile.rigidbody2D.centerOfMass = new Vector2(0.0f, 0.11f);
			Projectile.rigidbody2D.velocity = new Vector2(ThrowVectorX, Forklift.GetThrowAngle(ThrowAngleTopValue, ThrowAngleBottomValue));
			Projectile.rigidbody2D.AddTorque(-Random.Range(8,12));

			Projectile.GetComponent<ProjectileCollision>().SetOwner(gameObject);

			// start regeneration only when throwing first tool. prevents having all tools after one respawn time.
			if( ToolCount-- == ToolMax )
			{
				Invoke("RespawnTool", ToolRespawnTime);
			}
		}
	}
}
