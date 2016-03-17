using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	//2D Movement
	public float horizontalSpeed = 0.1F;
	public float verticalSpeed = 0.1F;
	//2D Rotation
	public float rotationSpeed = 100.0f;




	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Check If Objects Are Within Correct Regions
		DefineCombatSpace ();

		float h = horizontalSpeed * Input.GetAxis("P2_LeftHorizontal");
		float v = verticalSpeed * Input.GetAxis("P2_LeftVertical");
		//Commit Transform
		transform.Translate(v, h, 0);

		//Turn Character Facing
		float r = rotationSpeed * Input.GetAxis ("P2_RightHorizontal");
		//Commit Rotation
		transform.Rotate (0, 0, -r);

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

}
