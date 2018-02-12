using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropertiesDisplay : MonoBehaviour {

	private ShipAttributes myShip;

	public Text damage;
	public Text accuracy;
	public Text reload;

	void Awake(){
		myShip = GetComponentInParent<ShipAttributes> ();
		UpdateText ();

	}

	void UpdateText(){ //Turn the values of damage, accuracy, and reload speed to text and display them below the selected ship.
		damage.text = myShip.damage.ToString ();
		accuracy.text = myShip.accuracy.ToString ();
		reload.text = myShip.speed.ToString ();
	}

}
