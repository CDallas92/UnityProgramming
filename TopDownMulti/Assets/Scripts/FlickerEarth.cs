using UnityEngine;
using System.Collections;



public class FlickerEarth : MonoBehaviour {

	//Setup For The Renderer To Be Toggled
	public Renderer rend;

// Update is called once per frame
	void Update () 
	{
		FlickerTheHologram ();
	}

	void FlickerTheHologram()
	{
		for(int i = 0; i<NumberOfFlickers(); i++)
		{
		rend.enabled = false;
		StartCoroutine("EnabledHologram");
		}
	}

	IEnumerator EnabledHologram()
	{
		//Wait For 0.5 Seconds Before Enabling Again
		yield return new WaitForSeconds (0.5f);
		//Enable
		rend.enabled = true;
	}

	//Set A Random Amount Of Timer Before Next Flicker
	float RandomTimer()
	{
		float minimum = 1.0f;
		float maximum = 10.0f;
		float selectedTime;
		float nextFlickerTime;

		//calc selectedTime
		selectedTime = Random.Range(minimum, maximum);

		nextFlickerTime = selectedTime;

		return nextFlickerTime;
	}


	//Randomly Choose How Many Times To Flash
	int NumberOfFlickers()
	{
		int min = 1;
		int max = 5;
		int flickercount;

		//calc number of flickers
		flickercount = Random.Range (min, max);

		return flickercount;
	}
}
