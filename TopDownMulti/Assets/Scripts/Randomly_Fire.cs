using UnityEngine;
using System.Collections;

public class Randomly_Fire : MonoBehaviour {

	public float numberOfBullets;

	public GameObject RandomShots;

	public float TerroristFireRate = 1.0f;
	public float TerroristLastShot = 0.0f;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Rotate on Z, Left-Right
		transform.Rotate (0, 0, 1);

		GameObject Clone;
		
		//Spawning the bullet at position
		//Debug.Log ("Terrorist Bullet is found");
		
		
		//Ensure Bullets Are Spawning infront of player
		Vector3 FireFromHere = new Vector3 (transform.Find ("TerrorFire").position.x, transform.Find ("TerrorFire").position.y ,transform.Find ("TerrorFire").position.z);
		
		//(transform.position.x, transform.position.y, transform.position.z);




		//Ensure Terrorist Shoots At Reasonable Speed
		if (Time.time > TerroristFireRate + TerroristLastShot) 
		{
		
			//Instantiate Clone Bullet Prefab
			Clone = (Instantiate (RandomShots, FireFromHere, transform.rotation)) as GameObject;

			TerroristLastShot = Time.time;
		}
	

	}
}
