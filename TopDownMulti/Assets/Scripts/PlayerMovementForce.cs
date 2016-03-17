using UnityEngine;
using System.Collections;

public class PlayerMovementForce : MonoBehaviour {
	
	//2D Movement
	public float horizontalSpeedForce = 0.1F;
	public float verticalSpeedForce = 0.1F;
	public float currentSpeed;
	//2D Rotation
	public float rotationSpeedForce = 100.0f;
	//Slow Rotation If Trigger Pressed
	public int rotAmount = 2;
	//Stop Player
	public float playerFocused = 0;
	//Shooting Mechanics
	public float playerShooting = 0;
	public GameObject BulletPrefab;
	public GameObject ShellPrefab;
	public GameObject DeaglePrefab;
	public GameObject GrenadePrefab;

	//Weapon Fire Rates
	public float ShotgunFireRate = 1.0f;
	public float RifleFireRate = 1.0f;
	public float DeagleFireRate = 1.0f;
	public float GrenadeFireRate = 1.0f;
	public float lastShot = 0.0f;

	//Check For Weapon Firing
	public bool RifleFiring = false;
	public bool ShotgunFiring = false;
	public bool DeagleFiring = false;
	//Stops Player Constantly Reloading And Fucking Shit Up
	public bool reloadingRifle = false;
	public bool reloadingShotgun = false;
	public bool reloadingDeagle = false;

	//Decide What Weapon To Fire
	enum WeaponType{ Rifle, Shotgun, Deagle}
	//Set Weapon As Rifle
	WeaponType currentWeapon;

	public P1_AmmoScript P1_Ammo;
	public FoxScript Foxy;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Check If Objects Are Within Correct Regions
		//Don't Use Once Everything Has RigidBody
	//	DefineCombatSpace ();


		////////////////////////////////////////////////////////
		//Movement  :Read In Analogue Stick Movement
		//Rotations
		float h = horizontalSpeedForce * Input.GetAxis("P1_LeftVertical");
		float v = verticalSpeedForce * Input.GetAxis("P1_LeftHorizontal");

		//Trigger Stops Player Movement For Accuracy -- Left Trigger Use, 0.0 -> 1.0 
		playerFocused = Input.GetAxis ("P1_LeftTrigger");
		//Character Rotations -- Turn Character Direction
		float r = (rotationSpeedForce * Input.GetAxis ("P1_RightHorizontal")*rotAmount);

		//Adjust Speed For Slow Walking Mode [ Full Press = Stop, Half Press = Slow ]
		//Slow Rotation Speed Down For Precise Aim
		if (playerFocused > 0) {
			h = h * (1 - playerFocused);
			v = v * (1 - playerFocused);
			rotAmount = 1;
		} else
			rotAmount = 2;

		//Declare RigidBody -- Apply Force Values To The Vector
		Rigidbody2D BP = GetComponent<Rigidbody2D> ();
		Vector2 ForceToBeApplied = new Vector2 (v, -h);
		////////////////////////////////////////////////////////
		//Commit Force
		BP.AddForce (ForceToBeApplied);
		//Commit Rotations
		transform.Rotate (0, 0, -r);

	

		////////////////////////////////////////////////////////
		//Fire Rifle If Currently Equipped And Trigger Pressed
		if (Input.GetAxis ("P1_RightTrigger") > 0) 
		{	
			if (currentWeapon == WeaponType.Rifle && P1_Ammo.currentRifleAmmo > 0) 
			{
				//Enable Detection Of Firing To Stop Reloading While Shooting.
				RifleFiring = true;
				//Reset Reloading System To Allow For Reloads Only After A Shot Has Been Fired. 
				reloadingRifle = false;
				print ("Shots Fired");
				if (Time.time > RifleFireRate + lastShot) 
				{
					FireShots (); 
					lastShot = Time.time;
				}
			}		
		}
		else
		RifleFiring = false;

		////////////////////////////////////////////////////////
		//Fire Shotgun If Currently Equipped And Trigger Pressed 
		if (Input.GetAxis ("P1_RightTrigger") > 0) {
			if (currentWeapon == WeaponType.Shotgun && P1_Ammo.currentShotgunAmmo > 0) {
				if (Time.time > ShotgunFireRate + lastShot) {
					//Enable Detection Of Firing To Stop Reloading While Shooting
					ShotgunFiring = true;
					//Reset Reloading Systm To Allow For Reloads Only After A Shot has Been Fired
					reloadingShotgun = false;
					print ("Shotgun Fired");
					FireShotgun ();
					lastShot = Time.time;
				}
			}
		} else
		ShotgunFiring = false;
		
		////////////////////////////////////////////////////////
		//Fire Deagle If Currently Equipped And Trigger Pressed 
		if (Input.GetAxis ("P1_RightTrigger") > 0) {
			if (currentWeapon == WeaponType.Deagle && P1_Ammo.currentDeagleAmmo > 0) {
				if (Time.time > DeagleFireRate + lastShot) {
					//Enable Detection Of Firing To Stop Reloading While Shooting
					DeagleFiring = true;
					//Reset Reloading Systm To Allow For Reloads Only After A Shot has Been Fired
					reloadingDeagle = false;
					print ("Deagle Fired");
					FireDeagle ();
					lastShot = Time.time;
				}
			}
		} else
			DeagleFiring = false;

		////////////////////////////////////////////////////////
		//Throw Grenades
		if (Input.GetAxis ("P1_Button_RightBumper") == 1) {
			//Throw Grenade From Method
			if(Time.time > GrenadeFireRate + lastShot)
			{
				ThrowGrenade();
				print ("Nade Thrown");
				lastShot = Time.time;
			}
		
		}

		////////////////////////////////////////////////////////
		//Change Weapon Type
		if(Input.GetButtonDown("P1_Button_A"))
		{
			//If Currently Using Rifle, Change To Shotgun
			if(currentWeapon == WeaponType.Rifle)
			{
				currentWeapon = WeaponType.Shotgun;
				print ("Changed To Shotty");
			}
			//If Current Using Shotgun, Change To Rocket
			else if(currentWeapon == WeaponType.Shotgun)
			{
				currentWeapon = WeaponType.Deagle;
				print ("Changed To Deagle");
			//Currently Using Shotgun Change To Rifle
			}else if(currentWeapon == WeaponType.Deagle)
			{
				currentWeapon = WeaponType.Rifle;
				print ("Changed To Rifle");

			}
		}



		///////////////////////////////////////////////////////
		//Reload Rifle When B Pressed, If Not Firing
		if (Input.GetButtonDown ("P1_Button_B") && RifleFiring == false && reloadingRifle == false && currentWeapon == WeaponType.Rifle) 
		{
			reloadingRifle = true;
			P1_Ammo.reloadRifle ();
		}

		if (Input.GetButtonDown ("P1_Button_B") && ShotgunFiring == false && reloadingShotgun == false && currentWeapon == WeaponType.Shotgun) 
		{
			reloadingShotgun = true;
			P1_Ammo.reloadShotgun();
		}

		///////////////////////////////////////////////////////
		//Throw The Dog When Equipped
		if (Input.GetButtonDown ("P1_Button_Y")) 
		{
			Debug.Log ("DJAJWADJWOPDOP");
			Foxy.DropFox();
		}

		


		//////END OF UPDATE///////

	////DEBUGGING////////////////
	//Debugging Display Actual Speed
		currentSpeed = -(horizontalSpeedForce * playerFocused)+240;
	/////////////////////////////
	}
	
	void DefineCombatSpace()
	{
		//Border Limits
		float LeftWall  = -7.0f;
		float RightWall =  7.0f;
		float UpperWall =  5.0f;
		float LowerWall = -5.0f;
		//Border Respawns
		float LeftWallRespawn  = -6.91f;
		float RightWallRespawn =  6.91f;
		float UpperWallRespawn =  4.91f;
		float LowerWallRespawn = -4.91f;
		
		//Stop Objects From Leaving Combat Zone
		//X Axis
		Vector3 LeftStopper = new Vector3(LeftWallRespawn, transform.position.y, transform.position.z);
		Vector3 RightStopper = new Vector3(RightWallRespawn, transform.position.y, transform.position.z);
		//Y Axis
		Vector3 UpperStopper = new Vector3(transform.position.x, UpperWallRespawn, transform.position.z);
		Vector3 BottomStopper = new Vector3(transform.position.x, LowerWallRespawn, transform.position.z);
		
		//Confine The Movement Space (Hardcoded For Now) -- Replace Values With GameObejcts
		//Do Not Pass Left Wall
		if (this.transform.position.x < LeftWall)  {transform.position = LeftStopper;	}
		//Do Not Pass Right Wall
		if (this.transform.position.x > RightWall) {transform.position = RightStopper;	}
		//Do Not Pass Upper Wall
		if (this.transform.position.y > UpperWall) {transform.position = UpperStopper;	}
		//Do Not Pass Lower Wall
		if (this.transform.position.y < LowerWall) {transform.position = BottomStopper;	}
		
	}
	

public void FireShots()
	{
		//Clone of the bullet
		GameObject Clone;

		//Spawning the bullet at position
		Debug.Log ("Bullet is found");


		//Ensure Bullets Are Spawning infront of player
		Vector3 FireFromHere = new Vector3 (transform.Find ("FirePosition").position.x, transform.Find ("FirePosition").position.y ,transform.Find ("FirePosition").position.z);

			//(transform.position.x, transform.position.y, transform.position.z);

		//Instantiate Clone Bullet Prefab
		Clone = (Instantiate (BulletPrefab, FireFromHere, transform.rotation)) as GameObject;
		//Deduct 1 Rifle Ammo Per Shot
		P1_Ammo.currentRifleAmmo -= 1;
	}

	void FireShotgun()
	{

		//Clone of the bullet
		GameObject Clone;
		
		//Spawning the bullet at position
		Debug.Log ("Shell is found");
		
		//Instantiate Shotgun Shell PreFab
		Vector3 Shotgun1 = new Vector3 (transform.Find ("ShotgunFire1").position.x, transform.Find ("ShotgunFire1").position.y ,transform.Find ("ShotgunFire1").position.z);
		Vector3 Shotgun2 = new Vector3 (transform.Find ("ShotgunFire2").position.x, transform.Find ("ShotgunFire2").position.y ,transform.Find ("ShotgunFire2").position.z);
		Vector3 Shotgun3 = new Vector3 (transform.Find ("ShotgunFire3").position.x, transform.Find ("ShotgunFire3").position.y ,transform.Find ("ShotgunFire3").position.z);
		
		
		Clone = (Instantiate (ShellPrefab, Shotgun1, transform.Find ("ShotgunFire1").rotation)) as GameObject;
		Clone = (Instantiate (ShellPrefab, Shotgun2, transform.Find ("ShotgunFire2").rotation)) as GameObject;
		Clone = (Instantiate (ShellPrefab, Shotgun3, transform.Find ("ShotgunFire3").rotation)) as GameObject;
		//Deduct Shotgun Shells Per Shot
		P1_Ammo.currentShotgunAmmo -= 1;

	}


	void FireDeagle()
	{
		GameObject Clone;
		//Show The Log That Function Has Been Fired
		Debug.Log ("Deagle Round Found");
		//Set Up The Vector For The Shot To Travel
		Vector3 Deagle1 = new Vector3 (transform.Find ("FirePosition").position.x, transform.Find ("FirePosition").position.y, transform.Find ("FirePosition").position.z);
		//Initiate The Deagle Shot
		Clone = (Instantiate(DeaglePrefab, Deagle1, transform.Find ("FirePosition").rotation)) as GameObject;
		//Deduct Deagle Ammo Shot
		P1_Ammo.currentDeagleAmmo -= 1;
	}

	void ThrowGrenade()
	{
		GameObject Clone;
		//Show The Log That Function Has Been Fired
		Debug.Log ("Grenade Found");
		//Set Up The Vector For The Shot To Travel
		Vector3 Grenade1 = new Vector3 (transform.Find ("FirePosition").position.x, transform.Find ("FirePosition").position.y, transform.Find ("FirePosition").position.z);
		//Initiate The Deagle Shot
		Clone = (Instantiate(GrenadePrefab, Grenade1, transform.Find ("FirePosition").rotation)) as GameObject;
		//Deduct Deagle Ammo Shot
		P1_Ammo.currentGrenadeAmmo -= 1;
		
	}

}
