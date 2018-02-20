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
		if (parentship.attackStance == true) {
			fightStance.gameObject.SetActive (true);
			defendStance.gameObject.SetActive (false);
		} else if (parentship.attackStance == false) {
			fightStance.gameObject.SetActive (false);
			defendStance.gameObject.SetActive (true);
		} else if (parentship.attackStance == null) {
			fightStance.gameObject.SetActive (false);
			defendStance.gameObject.SetActive (false);
		}
	}
}
