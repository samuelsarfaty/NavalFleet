﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingBoat : MonoBehaviour {

	public int amountToGive;
	public float rate;
	public GameObject coinEffect;
	public GameObject coinText;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("IncreaseMoney", rate, rate);
	}

	void IncreaseMoney(){
		Instantiate (coinEffect, transform.position, Quaternion.identity);
		GameManager.coins += amountToGive;
	}

	void OnTriggerEnter2D(Collider2D other){
		print ("found something");
		if (other.GetComponent<Coin> ()) {
				print("found coin");
			IncreaseMoney ();
			Destroy (other.gameObject);
		}
	}


}
