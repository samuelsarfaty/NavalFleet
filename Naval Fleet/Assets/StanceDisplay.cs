using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StanceDisplay : MonoBehaviour {

	public Image fightStance;
	public Image defendStance;

	private Ship parentship;

	void Awake(){
		parentship = GetComponentInParent<Ship> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (parentship.attackStance) {
			fightStance.gameObject.SetActive (true);
			defendStance.gameObject.SetActive (false);
		} else {
			fightStance.gameObject.SetActive (false);
			defendStance.gameObject.SetActive (true);
		}
	}
}
