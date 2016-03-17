using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class P1_Health : MonoBehaviour {

	//Player Stats
	public float maxHealth = 100.0f;
	public float CurrentHealth;
	//Slider For HP
	public Slider healthSlider;

	// Use this for initialization
	void Start () 
	{
		//Initialise Slider & Player Health
		CurrentHealth = maxHealth;
		healthSlider.maxValue = maxHealth;

	}
	
	// Update is called once per frame
	void Update () 
	{

		//if (Input.GetAxis ("P1_RightTrigger") > 0) {CurrentHealth--;	}
		if (Input.GetAxis ("P1_LeftTrigger") > 0) {CurrentHealth++;		}

		//Set The Slider Hp Bar To Current Player Health
		healthSlider.value = CurrentHealth;

	}



	void OnCollisionEnter2D(Collision2D coll )
	{
		//If Player Is Hit By Obstacles Or Enemy_Ammo, Take Damage
		if (coll.gameObject.tag == "Obstacles" || coll.gameObject.tag == "Enemy_Ammo") {	CurrentHealth-= 10.0f;		}	
	
	}


}
