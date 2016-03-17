using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class P1_AmmoScript : MonoBehaviour {
	
	//Player Stats
	public int maxRifleAmmo = 100;
	public int maxShotgunAmmo = 30;
	public int maxDeagleAmmo = 35;
	public int maxGrenades = 10;
	//Max Rifle Ammo
	public int remainingRifleAmmo;
	//Max Ammo per Reload
	public int rifleClip;
	//Max Shotgun Ammo
	public int remainingShotgunAmmo;
	//Max Ammo Per Reload
	public int shotgunClip;
	//Max Deagle Ammo
	public int remainingDeagleAmmo;
	//Max Ammo Per Deagle Reload
	public int deagleClip;
	//Currently Loaded Ammo
	public int currentRifleAmmo;
	public int currentShotgunAmmo;
	public int currentDeagleAmmo;
	public int currentGrenadeAmmo;

	public float reloadRifleTime = 1.0f;

	//Allows Writing To The Text Object In UI - That Is Assigned In Inspector
	public Text Rifle_Ammo;
	public Text Shotgun_Ammo;
	public Text Deagle_Ammo;
	public Text Grenade_Ammo;

	//Display Reload Text
	public bool rifleReloadingText = false;
	public bool shotgunReloadingText = false;
	public bool deagleReloadingText = false;
	public bool grenadeReloadingText = false;

	// Use this for initialization
	void Start () 
	{
		//Initialise Player Starting Ammo
		rifleClip = 30;
		shotgunClip = 6;
		deagleClip = 7;
		currentRifleAmmo = rifleClip;
		currentShotgunAmmo = shotgunClip;
		currentDeagleAmmo = deagleClip;
		currentGrenadeAmmo = maxGrenades;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		//if (Input.GetAxis ("P1_RightTrigger") > 0) {CurrentHealth--;}
	//	if (Input.GetAxis ("P1_LeftTrigger") > 0) {currentRifleAmmo++;}

		//If Firing Is Enabled Lose Ammo === Rifle / Shotgun /
		//if (PMF.RifleFiring == true	 ) {	currentRifleAmmo--;		}		
		//if (PMF.ShotgunFiring == true) {	currentShotgunAmmo--;	}		

		//Display Rifle Ammo Left
		if (rifleReloadingText == false) 	{		Rifle_Ammo.text = "Rifle  : " + currentRifleAmmo.ToString () + "/" + maxRifleAmmo.ToString ();		}
		//Display Shotgun Ammo Left
		if (shotgunReloadingText == false)  {	Shotgun_Ammo.text = "Shotgun: " + currentShotgunAmmo.ToString () + "/" + maxShotgunAmmo.ToString ();	}
		//Display Deagle Ammo Left
		if (deagleReloadingText == false)  {	Deagle_Ammo.text = "Deagle: " + currentDeagleAmmo.ToString () + "/" + maxDeagleAmmo.ToString ();	}
		//Display Grenade Ammo Left
		if (grenadeReloadingText == false)  {	Grenade_Ammo.text = "Grenades: " + currentGrenadeAmmo.ToString () + "/" + maxGrenades.ToString ();	}

		//Keep Checks On Ammo Count
		OutOfAmmo ();
	}

	void OutOfAmmo()
	{
		//Ensure Ammo Doesnt Go Into Negatives
		if (rifleClip < 1) 		{rifleClip = 0;}
		if (shotgunClip < 1) 	{shotgunClip = 0;}
		if (deagleClip < 1)		{deagleClip = 0;}
	}

	public void reloadRifle()
	{
		//Add Remaining Bullets In Clip To Total Ammo --- Dont Want To Lose Unfired Rounds
		remainingRifleAmmo = currentRifleAmmo;
		maxRifleAmmo += remainingRifleAmmo;
		currentRifleAmmo = 0;

		//Display Reload Text
		Rifle_Ammo.text = "RIFLE RELOADING ";


		//CoRoutine Allows Delay To Be Added During Gameplay
		//Only Reload If Ammo is Available
		if (maxRifleAmmo > 29) {
			StartCoroutine ("RifleReloading");
		} else
			StartCoroutine ("RemainingRifleAmmo");
	}

	//Delay For Reloading Functionality 
	IEnumerator RifleReloading()
	{
		rifleReloadingText = true;
		yield return new WaitForSeconds (1.0f);

		//After Delay Process Reload
		//Add Clip Size To Weapon, Remove Appropriate Number Of Rounds From Ammo Total
		currentRifleAmmo = rifleClip;
		maxRifleAmmo -= rifleClip;

		//Hide Reloading Message 
		rifleReloadingText = false;
	}

	IEnumerator RemainingRifleAmmo()
	{
		yield return new WaitForSeconds (1.0f);

		//Set The Rounds In The Clip To The Remaining Number Of Shots 
		currentRifleAmmo = maxRifleAmmo;
		//Set The Remaining Number Of Shots To 0
		maxRifleAmmo = 0;

	}

	public void reloadShotgun()
	{
		//Add Remaining Shells To Total Ammo --- Dont Lose Unfired Shells
		remainingShotgunAmmo = currentShotgunAmmo;
		maxShotgunAmmo += remainingShotgunAmmo;
		currentShotgunAmmo = 0;

		//Display Reload Text
		shotgunReloadingText = true;
		Shotgun_Ammo.text = "SHOTGUN RELOADING";

		//CoRoutine Allows Delay To be Added During Gameplay
		StartCoroutine ("ShotgunReloading");

	}

	IEnumerator ShotgunReloading()
	{
		yield return new WaitForSeconds (2.0f);

		//After Delay Process Reload
		//Add Clip Size To Weapon, Remove Appropritate Number Of Rounds From Ammo Total
		currentShotgunAmmo = shotgunClip;
		maxShotgunAmmo -= shotgunClip;

		//Hide Reloading Text Message
		shotgunReloadingText = false;
	}


}
	
