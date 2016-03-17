using UnityEngine;
using System.Collections;

public class FirePositionScript : MonoBehaviour {

	public Vector3 FirePositionPos = new Vector3 (0, 0, 0);
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Update FirePosition Info
		UpdateFirePosPos ();
		
	}
	
	
	void UpdateFirePosPos()
	{
		Vector3 FirePosPos = new Vector3 (GameObject.Find ("FirePosition").transform.position.x, GameObject.Find ("FirePosition").transform.position.y, GameObject.Find ("FirePosition").transform.position.z);
		
		//Set Vector To Public Variable
		FirePositionPos = FirePosPos;
		
	}

	public Vector3 ReturnPositionVector()
	{

		return FirePositionPos;


	}

}
