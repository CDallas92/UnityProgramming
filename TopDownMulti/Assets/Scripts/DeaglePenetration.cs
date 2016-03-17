using UnityEngine;
using System.Collections;

public class DeaglePenetration : MonoBehaviour {
	
	public BoxCollider2D OuterCheckBox;
	public BoxCollider2D MainCollider;

	public bool hasBeenHit = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(hasBeenHit == false)
		{
			MainCollider.enabled = true;
			OuterCheckBox.enabled = true;
		}
	}
	
	
	void OnCollisionEnter2D(Collision2D coll )
	{
		
		//if Deag Shot hits this box, turn Off parents collider
		
		if (coll.gameObject.tag == "P1_DeagleAmmo" && hasBeenHit == false) {
			Debug.Log ("TURN THAT SHIT INTO A TRIGGER");
			//BCBags.enabled = false;
			MainCollider.enabled = false;
			hasBeenHit = true;
			StartCoroutine("ResetMode");
		} 

	}
	
	IEnumerator ResetMode()
	{
		yield return new WaitForSeconds (0.2f);
		hasBeenHit = false;
	}
}

	