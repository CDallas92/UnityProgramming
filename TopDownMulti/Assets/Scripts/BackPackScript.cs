using UnityEngine;
using System.Collections;

public class BackPackScript : MonoBehaviour {

	public Vector3 BackPackPosition = new Vector3 (0, 0, 0);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Update Backpack Info
		UpdateBackPackArea ();

	}


	void UpdateBackPackArea()
	{
		Vector3 backpackPos = new Vector3 (GameObject.Find ("BackPack").transform.position.x, GameObject.Find ("BackPack").transform.position.y, GameObject.Find ("BackPack").transform.position.z);
		
		//Set Vector To Public Variable
		BackPackPosition = backpackPos;

	}

}
