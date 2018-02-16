﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropertiesDisplay : MonoBehaviour {

	private Ship myShip;
	private AttackBar attackBar;

	/*public Text damage;
	public Text accuracy;
	public Text reload;*/

	void Awake(){
		if (GetComponentInParent<Ship> ()) {
			myShip = GetComponentInParent<Ship> ();
		}
		if (GetComponentInChildren<AttackBar> ()) {
			attackBar = GetComponentInChildren<AttackBar> ();
		}

	}

	void Update(){
		if (myShip && myShip.isEngaged && myShip.attackStance == true) {
			attackBar.GetComponent<Image> ().enabled = true;
			attackBar.transform.GetChild (0).gameObject.SetActive (true);
		} else if (attackBar && myShip && myShip.isEngaged == false) {
			attackBar.GetComponent<Image> ().enabled = false;
			attackBar.transform.GetChild (0).gameObject.SetActive (false);
		} else if (myShip && myShip.attackStance == false) {
			attackBar.GetComponent<Image> ().enabled = false;
			attackBar.transform.GetChild (0).gameObject.SetActive (false);
		}
	}

}
