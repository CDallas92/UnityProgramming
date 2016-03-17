using UnityEngine;
using System.Collections;

public class GrenadeScript : MonoBehaviour {

	public float thrust;
	public Rigidbody2D RB;

	//Shrapnel Object To Spawn From Grenade
	public GameObject ShrapnelPrefab;

	//Time Til Explosion
	public float detonateDelay;


	//Only Add Force One Time
	public bool NadeThrown = false;
	//Only Explode Once
	public bool NadeExploded = false;


	// Use this for initialization
	void Start () {
		
		RB = GetComponent<Rigidbody2D> ();
		//For Some Reason, Bullets Spawn At -90 Of The Player... This Seems To Be Easiest Fix Allowing RelativeForce
		//Dont Do This If Player Sprite Faces Right To begin With	transform.Rotate (0, 0, 90);

		//Grenade Timer = 2 Seconds
		detonateDelay = 25.0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		//Move The Bullet Once It Has Spawned
		Vector2 Derp = new Vector2 (10, 0);

		//Throw Once
		//Add Force Only One Time
		if (NadeThrown == false && NadeExploded == false) 
		{
			RB.AddRelativeForce (Derp, ForceMode2D.Impulse);
			NadeThrown = true;
		}

		//Reduce Nade Timer
		GrenadeTimer ();
		//Destroy If No Collision But Timer Expires
		if (detonateDelay <= 0) {	DetonateOnTimer ();	}

	}


	void DetonateOnTimer()
	{
		//Remove Nade
		Destroy (gameObject);
		//Call Explosions
		ShrapnelExplosion ();

		//Set Nade To Exploded State
		NadeExploded = true;
	}


	void GrenadeTimer()
	{
		//-1 Second Every Second
		detonateDelay -= 1.0f;
	}

	//Check For Shots Hitting
	void OnCollisionEnter2D(Collision2D coll )
	{
		//"Player" = Player 1 or Player 2
		//"Obstacles" = Walls / Traps / Doors On The Field
		//"Enemy" = Targets The Player Must Destroy
		//"Enemy_Ammo" = Enemy Bullets That Are Fired
		//
		//Delete Player Bullet If It Hits, Obstacles, Enemies Or Enemy Shots. 
		if (coll.gameObject.tag == "Obstacles" || coll.gameObject.tag == "Walls" || coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Enemy_Ammo" || coll.gameObject.tag == "P1_Ammo" && NadeExploded == false) 
		{	

			Destroy(gameObject);
			//Call Explosion Effects
			ShrapnelExplosion();

			//Grenade Already Exploded
			NadeExploded = true;
		}
	}
	



	void ShrapnelExplosion()
	{
		//Clean This Up Later
		//Make 4 Shrapnel Per Side..
		//
		//			* * * *
		//			*     *
		//			*     *
		// 			* * * *
		//All With Similar Rotations And Movespeeds To Create Spiraling Explosion
		//Plasma Grenade? // Idk, Create Thematic Bullshit To Cover Up Aesthetic.

		//Cause Damage To Anyone Hit
		//Give KnockBack If Possible To Nearest Person / Enemy

		//Clone of the bullet
		GameObject Clone;
		
		//Spawning the bullet at position
		Debug.Log ("Grenade Shrapnel!!!");
		
		//Instantiate Shotgun Shell PreFab
		Vector3 Shrapnel1 = new Vector3 (transform.Find ("TopLeftCorner").position.x, transform.Find ("TopLeftCorner").position.y ,transform.Find ("TopLeftCorner").position.z);
		Vector3 Shrapnel2 = new Vector3 (transform.Find ("TopRightCorner").position.x, transform.Find ("TopRightCorner").position.y ,transform.Find ("TopRightCorner").position.z);
		Vector3 Shrapnel3 = new Vector3 (transform.Find ("BottomLeftCorner").position.x, transform.Find ("BottomLeftCorner").position.y ,transform.Find ("BottomLeftCorner").position.z);
		Vector3 Shrapnel4 = new Vector3 (transform.Find ("BottomRightCorner").position.x, transform.Find ("BottomRightCorner").position.y ,transform.Find ("BottomRightCorner").position.z);
		Vector3 Shrapnel5 = new Vector3 (transform.Find ("BottomMiddle").position.x, transform.Find ("BottomMiddle").position.y ,transform.Find ("BottomMiddle").position.z);
		Vector3 Shrapnel6 = new Vector3 (transform.Find ("TopMiddle").position.x, transform.Find ("TopMiddle").position.y ,transform.Find ("TopMiddle").position.z);
		Vector3 Shrapnel7 = new Vector3 (transform.Find ("LeftMiddle").position.x, transform.Find ("LeftMiddle").position.y ,transform.Find ("LeftMiddle").position.z);
		Vector3 Shrapnel8 = new Vector3 (transform.Find ("RightMiddle").position.x, transform.Find ("RightMiddle").position.y ,transform.Find ("RightMiddle").position.z);
		Vector3 Shrapnel9 = new Vector3 (transform.Find ("TopRightCorner2").position.x, transform.Find ("TopRightCorner2").position.y ,transform.Find ("TopRightCorner2").position.z);
		Vector3 Shrapnel10  = new Vector3 (transform.Find ("TopLeftCorner2").position.x, transform.Find ("TopLeftCorner2").position.y ,transform.Find ("TopLeftCorner2").position.z);
		Vector3 Shrapnel11 = new Vector3 (transform.Find ("BottomLeft2").position.x, transform.Find ("BottomLeft2").position.y ,transform.Find ("BottomLeft2").position.z);
		Vector3 Shrapnel12 = new Vector3 (transform.Find ("BottomRight2").position.x, transform.Find ("BottomRight2").position.y ,transform.Find ("BottomRight2").position.z);


		Clone = (Instantiate (ShrapnelPrefab, Shrapnel1, transform.Find ("TopLeftCorner").rotation)) as GameObject;
		Clone = (Instantiate (ShrapnelPrefab, Shrapnel2, transform.Find ("TopRightCorner").rotation)) as GameObject;
		Clone = (Instantiate (ShrapnelPrefab, Shrapnel3, transform.Find ("BottomLeftCorner").rotation)) as GameObject;
		Clone = (Instantiate (ShrapnelPrefab, Shrapnel4, transform.Find ("BottomRightCorner").rotation)) as GameObject;
		Clone = (Instantiate (ShrapnelPrefab, Shrapnel5, transform.Find ("BottomMiddle").rotation)) as GameObject;
		Clone = (Instantiate (ShrapnelPrefab, Shrapnel6, transform.Find ("TopMiddle").rotation)) as GameObject;
		Clone = (Instantiate (ShrapnelPrefab, Shrapnel7, transform.Find ("LeftMiddle").rotation)) as GameObject;
		Clone = (Instantiate (ShrapnelPrefab, Shrapnel8, transform.Find ("RightMiddle").rotation)) as GameObject;
		Clone = (Instantiate (ShrapnelPrefab, Shrapnel9, transform.Find ("TopRightCorner2").rotation)) as GameObject;
		Clone = (Instantiate (ShrapnelPrefab, Shrapnel10, transform.Find ("TopLeftCorner2").rotation)) as GameObject;
		Clone = (Instantiate (ShrapnelPrefab, Shrapnel11, transform.Find ("BottomLeft2").rotation)) as GameObject;
		Clone = (Instantiate (ShrapnelPrefab, Shrapnel12, transform.Find ("BottomRight2").rotation)) as GameObject;



	}


}





