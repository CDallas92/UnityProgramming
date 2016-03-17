using UnityEngine;
using System.Collections;

public class Enemy_BulletScript : MonoBehaviour {
	
	public float thrust;
	public Rigidbody2D RB;
	
	
	public float destroyDelay = 2.0f;
	
	// Use this for initialization
	void Start () {

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
		//Send Message To Damage Player If Hit
		//if (coll.gameObject.tag == "Player")
		//	coll.gameObject.SendMessage ("ApplyDamage", 10);
		
		//	if (coll.gameObject.tag == "Obstacles" || coll.gameObject.tag == "Player" || coll.gameObject.tag == "P1_Ammo" || coll.gameObject.tag == "P2_Ammo" ) {	Destroy (gameObject);	}
		
		
		//Delete Rifle Shots If It Hits Anything That is "Tagged" Unless It Is Enemy Ammo
		//This Is Designed For Enemies To Use, So Their Own Bullets Dont Destroy Each Other 
		if (coll.gameObject.tag != "Enemy_Ammo" ) {	Destroy (gameObject);	}		
		
	}
	
	
}