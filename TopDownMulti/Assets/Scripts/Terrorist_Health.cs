using UnityEngine;
using System.Collections;

public class Terrorist_Health : MonoBehaviour {

	
	//Player Stats
	public float Terrorist_maxHealth = 100.0f;
	public float Terrorist_CurrentHealth;

	public Renderer rend;

	// Use this for initialization
	void Start () 
	{
		//Initialise Slider & Player Health
		Terrorist_CurrentHealth = Terrorist_maxHealth;

		//Draw The Sprite
		rend.GetComponent<Renderer>();
		rend.enabled = true;

	}
	
	// Update is called once per frame
	void Update () 
	{
		//If Terrorist Below 1 Health, Kill Him
		if (Terrorist_CurrentHealth < 1) 
		{
			//Destroy Terrorism
			Destroy (gameObject, 0);

		}


	}
	
	
	
	void OnCollisionEnter2D(Collision2D coll )
	{
		//If Player Is Hit By Obstacles Or Enemy_Ammo, Take Damage
		Debug.Log ("Terrorist Hit");


		if (coll.gameObject.tag == "P1_Ammo") 
		{
			Terrorist_CurrentHealth-= 10.0f;	
			rend.enabled = false; 
			StartCoroutine("FlashTerrorism");
		}	

		if (coll.gameObject.tag == "P1_DeagleAmmo") 
		{
			Terrorist_CurrentHealth -= 20.0f;
			rend.enabled = false;
			StartCoroutine ("FlashTerrorism");
		}
	}
	
	//Allow Sprite Renderer To Turn Back On After Being Hit
	 IEnumerator FlashTerrorism()
	{
		yield return new WaitForSeconds (0.1f);
		rend.enabled = true;
	}

}
