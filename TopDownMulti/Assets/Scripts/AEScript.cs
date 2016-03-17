using UnityEngine;
using System.Collections;

public class AEScript : MonoBehaviour {

	
	public float thrust;
	public Rigidbody2D RB;
	public BoxCollider2D BC2D;

	public float destroyDelay = 4.0f;
	
	// Use this for initialization
	void Start () 
	{
		RB = GetComponent<Rigidbody2D> ();
		//For Some Reason, Bullets Spawn At -90 Of The Player... This Seems To Be Easiest Fix Allowing RelativeForce
		//Dont Do This If Player Sprite Faces Right To begin With	transform.Rotate (0, 0, 90);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		//Move The Bullet Once It Has Spawned
		Vector2 Derp = new Vector2 (1, 0);
		RB.AddRelativeForce (Derp, ForceMode2D.Impulse);
		
		//Destroy Bullet After Time
		Destroy (gameObject, destroyDelay);
	}
	
	//Check For Shots Hitting
	void OnCollisionEnter2D(Collision2D coll )
	{
		//"Player" = Player 1 or Player 2
		//"Obstacles" = Walls / Traps / Doors On The Field
		//"Enemy" = Targets The Player Must Destroy
		//"Enemy_Ammo" = Enemy Bullets That Are Fired
		//"Deagle_Ammo" = Special Ammo That Pierces Walls
		//
		//Delete Player Bullet If It Hits, Obstacles, Enemies Or Enemy Shots. 
		if (coll.gameObject.tag == "Obstacles" || coll.gameObject.tag == "Enemy")
		{	
			//Only Destory Deagle Shot If It Hits Obstacles Or Enemies, Not Walls Or Projectiles
			Destroy (gameObject);
		}
	}
	
}



