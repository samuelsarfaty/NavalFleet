using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingBoat : MonoBehaviour {

	public int amountToGive;
	public float rate;

	private GameManager gm;
	public GameObject coinEffect;

	void Awake(){
		gm = GameObject.FindObjectOfType<GameManager> ();
	}

	// Use this for initialization
	void Start () {
		InvokeRepeating ("IncreaseMoney", rate, rate);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void IncreaseMoney(){
		Instantiate (coinEffect, transform.position, Quaternion.identity);
		gm.coins += amountToGive;
	}


}
