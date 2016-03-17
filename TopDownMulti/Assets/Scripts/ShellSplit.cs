using UnityEngine;
using System.Collections;

public class ShellSplit : MonoBehaviour {

		public float thrust;
		public Rigidbody2D RB;
		
		//Allows Shell To Split Into Balls
		public GameObject BallPrefab;
		
		//Delete Object After This Time
		public float destroyDelay = 0.5f;
		
		// Use this for initialization
		void Start () {

			TrailRenderer tr = GetComponent<TrailRenderer> ();
			tr.sortingLayerName = "Players";
			
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
			
		//if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Obstacles") {	Destroy (gameObject);		}

		//Delete Player Bullet If It Hits, Obstacles, Enemies Or Enemy Shots. 
		if (coll.gameObject.tag == "Obstacles" || coll.gameObject.tag == "Walls"  || coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Enemy_Ammo") {	Destroy (gameObject);	}
		

			
		}
	}