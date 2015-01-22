// Forklift Game
//
// Script author: Maciej Pryc, Alan Kwiatkowski
// Created: 2014/12/20

using UnityEngine;
using System.Collections;

public class PlayerMovement : HasLife
{
	Rigidbody2D playerRigidBody;
	CanAttack Weapon;

	// hud object
	public GameObject HUDObject;
	// stat script in hud object
	private HUDStats HUD;

	private ToolBucketManager Bucket;
	Animator anim;
	bool bThrowingAnimInProgress = false;
	AudioSource engineAudioSource;
	AudioSource forkAudioSource;

	//////////////// 
	// MOVEMENT PLAYER
	////////////////

	public float movementSpeed = 4300;
	Vector2 movementForce;
	bool bWasLastDirectionRight;
	public float changeDirectionTreshold = 2.0f;

	public GameObject LevelStart, LevelEnd;

	public ParticleSystem PSEngineSmoke;
	bool bEngineSmokeEnabled;
	//////////////// 
	// MOVEMENT FORK
	////////////////

	public float forkSpeed = 2.0f;
	Vector2 forkMovement;
	Transform forkTransform;
	Transform socketForkPosition;
	Transform socketForkTop;
	Transform socketForkBottom;
	Transform socketForkSpecial;
	Transform currentForkBottomSocket;
	
	//////////////// 
	// METHODS
	////////////////
	/// 
	void Awake () 
	{
		playerRigidBody = GetComponent<Rigidbody2D> ();
		forkTransform = transform.FindChild ("ForkliftFork");
		socketForkPosition = forkTransform.FindChild("SocketForkPosition");
		socketForkTop = transform.FindChild ("SocketForkTop");
		socketForkBottom = transform.FindChild ("SocketForkBottom");
		socketForkSpecial = transform.FindChild ("SocketForkSpecial");
		currentForkBottomSocket = socketForkBottom;

		forkMovement = new Vector2 (0.0f, 0.0f);
		movementForce = new Vector2 (0.0f, 0.0f);

		// make mass center low
		playerRigidBody.centerOfMass.Set(playerRigidBody.centerOfMass.x, 0);
		playerRigidBody.drag = 5;
		playerRigidBody.fixedAngle = true;

		Weapon = GetComponent<CanAttack> ();
		Bucket = GetComponent<ToolBucketManager>();
		anim = GetComponent<Animator>();
		engineAudioSource = GetComponent<AudioSource> ();
		forkAudioSource = forkTransform.GetComponent<AudioSource> ();

		SetEngineSmokePS(false);
		Invoke("DelayedEngineSmoke", 0.5f);
	}

	void FixedUpdate () 
	{
		forkMovement.x = 0.0f;
		forkMovement.y = 0.0f;
		movementForce.x = 0.0f;

		float currForkHeight = socketForkPosition.position.y;

		///////////////////////
		////     INPUT     ////
		///////////////////////

		if (Input.GetKey(KeyCode.RightArrow)) 
		{
			if(CanChangeDirection(true))
			{
				movementForce.x = movementSpeed;
				bWasLastDirectionRight = true;
			}
		}

		if (Input.GetKey (KeyCode.LeftArrow)) 
		{
			if(CanChangeDirection(false))
			{
				movementForce.x = -movementSpeed;
				bWasLastDirectionRight = false;
			}
		}

		if (Input.GetKey(KeyCode.UpArrow) && currForkHeight < socketForkTop.position.y) 
		{
			forkMovement.y = forkSpeed * Time.deltaTime;
		}

		if (Input.GetKey (KeyCode.DownArrow) && currForkHeight > currentForkBottomSocket.position.y) 
		{
			forkMovement.y -= forkSpeed * Time.deltaTime;
		}

		if (Input.GetKeyUp(KeyCode.Z)) 
		{
			TryToThrowTool();
		}

		//////////////////////
		////  MAIN CALLS  ////
		//////////////////////

		//move fork
		forkTransform.Translate(forkMovement);
		// move player
		playerRigidBody.AddForce(movementForce);
		

		forkAudioSource.mute = (forkMovement.y == 0.0f);
		engineAudioSource.pitch = 1.5f + Mathf.Abs (playerRigidBody.velocity.x) / 6.0f;
	}

	bool CanChangeDirection( bool desiredDirectionRight )
	{
		return Mathf.Abs(playerRigidBody.velocity.x) < changeDirectionTreshold || bWasLastDirectionRight == desiredDirectionRight;
	}

	public void ToggleSpecialForkSocket(bool bEnabled)
	{
		if (bEnabled)
			currentForkBottomSocket = socketForkSpecial;
		else
			currentForkBottomSocket = socketForkBottom;
	}

	public void OnForkCollision(Collision2D coll) 
	{
		// is Zombie
		if( coll.collider.gameObject.tag == "ZombieMelee" || coll.gameObject.tag == "ZombieMelee")
		{
			Weapon.GiveDamageTo(coll.gameObject, GameTypes.AttackModes.Primary, coll.collider, coll.contacts[0].point, coll.relativeVelocity.magnitude );
		}
	}

	/// <summary>
	/// If called, someone just died
	/// </summary>
	public override void Died( Collider2D finalPunchPart, Vector2 worldLocation, GameTypes.DamageType dmgType )
	{
		Debug.Log ("I, forklift... just died");
		gameObject.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
	}
	
	void Update()
	{
		//init for first time. function Start and Awake did it to early. dunno why
		if( HUDObject != null )
		{
			if( HUD == null )
			{
				HUD = HUDObject.GetComponent<HUDStats> ();

				UpdateToolCount();
			}
			
			// update level progress (player position between LevelStart and LevelEnd)
			if( HUD != null)
			{
				// what percent is player position between LevelStart and LevelEnd
				HUD.SetLevelProgress((transform.position.x - LevelStart.transform.position.x) / (LevelEnd.transform.position.x - LevelStart.transform.position.x));
			}
		}

		if( bEngineSmokeEnabled )
		{
			UpdatePSEngineSmoke();
		}
	}

	/// <summary>
	/// Tries to throw tool.
	/// </summary>
	void TryToThrowTool()
	{
		if( Bucket != null)
		{
			if( Bucket.CanThrowTool() && !bThrowingAnimInProgress)
			{
				if( anim != null )
				{
					anim.SetTrigger("Throw");
					bThrowingAnimInProgress = true;
				}
			}
		}
	}

	/// <summary>
	/// Notify from throw animation.
	/// </summary>
	void NotifyThrowTool()
	{
		if( Bucket != null)
		{
			Bucket.ThrowTool();

			UpdateToolCount();
		}
	}

	/// <summary>
	/// Notify to stop animation
	/// </summary>
	/// 
	void NotifyThrowEnd()
	{
		bThrowingAnimInProgress = false;
	}
	
	/// <summary>
	/// Calculate Throw vector Y form Fork position
	/// </summary>
	/// <returns>The throw angle.</returns>
	public float GetThrowAngle( float TopValue, float BottomValue )
	{
		float ForkPercentage = (socketForkPosition.position.y - socketForkBottom.position.y) / (socketForkTop.position.y - socketForkBottom.position.y);
		float Addition = ( TopValue - BottomValue ) * ForkPercentage;
		return BottomValue + Addition;
	}

	public void UpdateToolCount()
	{
		if( HUD == null )
		{
			HUD = HUDObject.GetComponent<HUDStats> ();
		}

		if( HUD != null)
		{
			HUD.SetToolCount( Bucket.GetToolCount() );
		}
	}
	
	void SetEngineSmokePS( bool inState )
	{
		bEngineSmokeEnabled = inState;
		if( inState )
		{
			PSEngineSmoke.Play();
		}
		else
		{
			PSEngineSmoke.Stop();
		}
	}
	
	void DelayedEngineSmoke()
	{
		SetEngineSmokePS(true);
	}
	
	void UpdatePSEngineSmoke()
	{
		if(	playerRigidBody.velocity.magnitude > 0.01f )
		{
			PSEngineSmoke.emissionRate = Mathf.Max( playerRigidBody.velocity.magnitude * 10, 3);
		}
	}
}
