using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairTool : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<Ship> () && other.GetComponent<ShipAttributes> ()) {
			float currentHealth = other.GetComponent<ShipAttributes> ().health;
			currentHealth += currentHealth * 0.2f;
			other.GetComponent<ShipAttributes> ().health = currentHealth;

		}
	}
}
