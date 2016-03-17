using UnityEngine;
using System.Collections;

public class TrailScript : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		TrailRenderer tr = GetComponent<TrailRenderer> ();
		tr.sortingLayerName = "Players";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
