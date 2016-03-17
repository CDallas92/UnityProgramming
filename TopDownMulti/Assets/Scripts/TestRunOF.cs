using UnityEngine;
using System.Collections;

public class TestRunOF : MonoBehaviour {

	public BoxCollider2D OuterCheckBox;
	public BoxCollider2D MainCollider;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter2D(Collision2D coll )
	{
	
		//if Deag Shot hits this box, turn Off parents collider

		if (coll.gameObject.tag == "P1_DeagleAmmo" && OuterCheckBox)
		{
			//BCBags.enabled = false;
			MainCollider.enabled = false;
		} 

	}

	void OnCollisionExit2D(Collision2D collo)
	{
		if (collo.gameObject.tag == "P1_DeagleAmmo" && OuterCheckBox) 
		{
			MainCollider.enabled = true;
		}

	}






}
