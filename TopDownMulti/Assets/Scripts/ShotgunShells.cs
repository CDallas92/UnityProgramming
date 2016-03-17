using UnityEngine;
using System.Collections;



public class ShotgunShells : MonoBehaviour {
	
	public float thrust;
	public Rigidbody2D RB;

	//Allows Shell To Split Into Balls
	public GameObject BallPrefab;

	//Delete Object After This Time
	public float destroyDelay = 0.2f;
	public bool spawned = false;

	// Use this for initialization
	void Start () {
		
		RB = GetComponent<Rigidbody2D> ();
		//For Some Reason, Bullets Spawn At -90 Of The Player... This Seems To Be Easiest Fix Allowing RelativeForce
		//Dont Do This If Player Sprite Faces Right To begin With	transform.Rotate (0, 0, 90);
		
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		
		//Move The Bullet Once It Has Spawned
		Vector2 Derp = new Vector2 (1, 0);
		RB.AddRelativeForce (Derp, ForceMode2D.Impulse);



		//Destroy Bullet After Time
		Destroy (gameObject, destroyDelay);

		//Deploy Shotgun Balls, Once
		if (spawned == false) {
			SplitShells ();
			spawned = true;
		}

	}

	
	//Check For Shots Hitting
	void OnCollisionEnter2D(Collision2D coll )
	{
		//Send Message To Damage Player If Hit
		//if (coll.gameObject.tag == "Player")
		//	coll.gameObject.SendMessage ("ApplyDamage", 10);
		
	//	if (coll.gameObject.tag == "Obstacles" || coll.gameObject.tag == "Player") {	Destroy (gameObject);		}

		//Delete Player Shells If It Hits, Obstacles, Enemies Or Enemy Shots. 
		if (coll.gameObject.tag == "Obstacles" || coll.gameObject.tag == "Walls"  || coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Enemy_Ammo") {	Destroy (gameObject);	}
		

		
	}

	void SplitShells()
	{
		//Clone of the bullet
		GameObject Clone;
		
		//Spawning the bullet at position
		Debug.Log ("Ball is found");
		
		//Instantiate Shotgun Shell PreFab
		Vector3 Ball1 = new Vector3 (transform.Find ("BallFire1").position.x, transform.Find ("BallFire1").position.y ,transform.Find ("BallFire1").position.z);
		Vector3 Ball2 = new Vector3 (transform.Find ("BallFire2").position.x, transform.Find ("BallFire2").position.y ,transform.Find ("BallFire2").position.z);
		Vector3 Ball3 = new Vector3 (transform.Find ("BallFire3").position.x, transform.Find ("BallFire3").position.y ,transform.Find ("BallFire3").position.z);
		
		
		Clone = (Instantiate (BallPrefab, Ball1, transform.Find ("BallFire1").rotation)) as GameObject;
		Clone = (Instantiate (BallPrefab, Ball2, transform.Find ("BallFire1").rotation)) as GameObject;
		Clone = (Instantiate (BallPrefab, Ball3, transform.Find ("BallFire1").rotation)) as GameObject;
	}
	
}
