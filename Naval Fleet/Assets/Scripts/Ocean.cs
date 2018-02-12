using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocean : MonoBehaviour {

	void OnMouseDown(){
		//Get the position of the click and change it to world coordinates.

		Vector2 mousePos = Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint (mousePos);
		Ship[] ships = GameObject.FindObjectsOfType<Ship> ();

		/*Loop through all the ships. If a ship is selected, stop its current move routine and move it to the clicked position.
		 * If no ships are selected, spawn a ship and reduce money
		 */
		for (int i = 0; i < ships.Length; i++) { 					
			if (ships [i].selected == true) {						 
				if (ships [i].lastRoutine != null) {
					ships [i].StopMove (ships [i].lastRoutine);
				}
				ships [i].Move (mousePos);
				return;
			}
		}
		if (UnitSelector.selectedShip.GetComponent<ShipAttributes> ().price <= GameManager.coins) {
			SpawnShip (mousePos);
		}
	}

	void SpawnShip(Vector2 spawnLocation){
		GameManager.coins -= UnitSelector.selectedShip.GetComponent<ShipAttributes> ().price;
		Instantiate (UnitSelector.selectedShip, spawnLocation, Quaternion.identity);

	}
}
