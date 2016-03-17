using UnityEngine;
using System.Collections;

public class Player2 : MonoBehaviour {


	//2D Movement
	public float P2_horizontalSpeedForce = 0.1F;
	public float P2_verticalSpeedForce = 0.1F;
	public float P2_currentSpeed;
	//2D Rotation
	public float P2_rotationSpeedForce = 100.0f;
	//Slow Rotation If Trigger Pressed
	public int P2_rotAmount = 2;
	//Stop Player
	public float P2_playerFocused = 0;
	//Shooting Mechanics
	public float P2_playerShooting = 0;
	public GameObject BulletPrefab;
	public GameObject ShellPrefab;
	public GameObject LazurPrefab;
	
	//Weapon Fire Rates
	public float P2_ShotgunFireRate = 1.0f;
	public float P2_RifleFireRate = 0.01f;
	public float P2_lastShot = 0.0f;
	
	//Decide What Weapon To Fire
	public enum P2_WeaponType{ P2_Rifle, P2_Shotgun, P2_Rocket}
	//Set Weapon As Rifle
	P2_WeaponType P2_currentWeapon;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Check If Objects Are Within Correct Regions
		//Don't Use Once Everything Has RigidBody
		DefineCombatSpace ();
		
		
		////////////////////////////////////////////////////////
		//Movement  :Read In Analogue Stick Movement
		//Rotations
		float h = P2_horizontalSpeedForce * Input.GetAxis("P2_LeftVertical");
		float v = P2_verticalSpeedForce * Input.GetAxis("P2_LeftHorizontal");
		
		//Trigger Stops Player Movement For Accuracy -- Left Trigger Use, 0.0 -> 1.0 
		P2_playerFocused = Input.GetAxis ("P2_LeftTrigger");
		//Character Rotations -- Turn Character Direction
		float r = (P2_rotationSpeedForce * Input.GetAxis ("P2_RightHorizontal")*P2_rotAmount);
		
		//Adjust Speed For Slow Walking Mode [ Full Press = Stop, Half Press = Slow ]
		//Slow Rotation Speed Down For Precise Aim
		if (P2_playerFocused > 0) {
			h = h * (1 - P2_playerFocused);
			v = v * (1 - P2_playerFocused);
			P2_rotAmount = 1;
		} else
			P2_rotAmount = 2;
		
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
		if (Input.GetAxis ("P2_RightTrigger") > 0) 
		{	
			if (P2_currentWeapon == P2_WeaponType.P2_Rifle) 
			{
				print ("Shots Fired");
				if (Time.time > P2_RifleFireRate + P2_lastShot) 
				{
					FireShots (); 
					P2_lastShot = Time.time;
				}
			}
		}
		
		
		////////////////////////////////////////////////////////
		//Fire Shotgun If Currently Equipped And Trigger Pressed 
		if (Input.GetAxis ("P2_RightTrigger") > 0) 
		{
			if (P2_currentWeapon == P2_WeaponType.P2_Shotgun) 
			{
				if (Time.time > P2_ShotgunFireRate + P2_lastShot) 
				{
					print ("Shotgun Fired");
					FireShotgun ();
					P2_lastShot = Time.time;
				}
			}
		}
		
		
		////////////////////////////////////////////////////////
		//Change Weapon Type
		if(Input.GetButtonDown("P2_Button_A"))
		{
			//If Currently Using Rifle, Change To Shotgun
			if(P2_currentWeapon == P2_WeaponType.P2_Rifle)
			{
				P2_currentWeapon = P2_WeaponType.P2_Shotgun;
				print ("P2_Changed To Shotty");
			}
			//If Current Using Shotgun, Change To Rocket
			else if(P2_currentWeapon == P2_WeaponType.P2_Shotgun)
			{
				P2_currentWeapon = P2_WeaponType.P2_Rocket;
				print ("P2_Changed To Rocket");
				//Currently Using Shotgun Change To Rifle
			}else if(P2_currentWeapon == P2_WeaponType.P2_Rocket)
			{
				P2_currentWeapon = P2_WeaponType.P2_Rifle;
				print ("P2_Changed To Rifle");
				
			}
		}
		
		//////END OF UPDATE///////
		
		////DEBUGGING////////////////
		//Debugging Display Actual Speed
		P2_currentSpeed = -(P2_horizontalSpeedForce * P2_playerFocused)+240;
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
	
	
	void FireShots()
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
		
	}
	
	
	
	
}
