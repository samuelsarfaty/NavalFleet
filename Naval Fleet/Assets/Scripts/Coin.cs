using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	public float factor;

	private SelfDestruct sd;
	private float startTime;

	void Awake(){
		sd = GetComponent <SelfDestruct> ();
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		float remainingTime = sd.secondsToKill - (Time.time - startTime);
		transform.RotateAround (transform.position, Vector3.up, remainingTime * factor);
	}
}
