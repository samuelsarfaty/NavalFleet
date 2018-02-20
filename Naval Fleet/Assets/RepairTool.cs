using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairTool : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<Ship> () && other.GetComponent<ShipAttributes> ()) {
			other.GetComponent<ShipAttributes> ().health += 5;
			Destroy (this.gameObject);
		}
	}
}
