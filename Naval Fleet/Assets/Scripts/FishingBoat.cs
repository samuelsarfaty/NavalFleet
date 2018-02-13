using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingBoat : MonoBehaviour {

	public int amountToGive;
	public float rate;
	public GameObject coinEffect;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("IncreaseMoney", rate, rate);
	}

	void IncreaseMoney(){
		Instantiate (coinEffect, transform.position, Quaternion.identity);
		GameManager.coins += amountToGive;
	}


}
