using UnityEngine;
using System.Collections;

public class FoxScript : MonoBehaviour {

	
	//Player Stats
	public float maxHealth = 100.0f;
	public float CurrentHealth;

	//Get The Position Of The Holding backpack
	public BackPackScript BPS;
	public FirePositionScript FPSPos;

	//Declare The Components For The Object
	public Rigidbody2D foxRigidbody;
	public BoxCollider2D foxCollision;

	//Detect If The Fox Has Been picked up
	public bool FoxRescued = false;
	public bool FoxPickedUp = false;

	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{

		//If The Fox Has Been Picked Up
		if (FoxRescued == true) 
		{
			//Move Fox To Backpack
			GameObject.Find ("lilFox").transform.position = BPS.BackPackPosition;
			//Stop The Passenger From Interacting
			foxCollision.enabled = false;
			foxRigidbody.isKinematic = true;

			//Rotate The Fox // Store Temp Rotation For The Backpacks Current Rotation
			var tempRotation = BPS.transform.rotation;
			//Set The Foxes Rotation To The backpack Rotation Stored
			gameObject.transform.localRotation = tempRotation;
			gameObject.transform.Rotate (0,0,180);
		}
	}
	

	void OnCollisionEnter2D(Collision2D coll )
	{
		//If Player Hits Fox, Enter Recue Mode
		if (coll.gameObject.tag == "Player" && FoxPickedUp == false) 
		{
			FoxRescued = true;
			FoxPickedUp = true;
			foxRigidbody.isKinematic = true;
		}
	}


	public void DropFox()
	{
		FoxRescued = false;
		StartCoroutine ("EnableFoxPhsyics");
	}

	IEnumerator EnableFoxPhsyics()
	{
		yield return new WaitForSeconds (0.5f);

		//Reset Fox Entirely And Drop Him
		FoxPickedUp = false;
		foxRigidbody.isKinematic = false;
		foxCollision.enabled = true;

	}

}
