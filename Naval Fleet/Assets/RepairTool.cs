using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairTool : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		print ("found something");
		if (other.GetComponent<Ship> () && other.GetComponent<ShipAttributes> ()) {
			print ("found ship");
			other.GetComponent<ShipAttributes> ().health += 5;
			Destroy (this.gameObject);
		}
	}
}
