using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

	private GameManager gm;

	public bool selected;

	void Awake(){
		gm = GameObject.FindObjectOfType<GameManager> ();
	}

	// Use this for initialization
	void Start () {
		gm.ships.Add (this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		print ("clicked ship");
		selected = true;
	}


}
