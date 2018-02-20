using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocean : MonoBehaviour {

	public GameObject x;

	void OnMouseDown(){
		//Get the position of the click and change it to world coordinates.
		Vector2 mousePos = Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint (mousePos);
		Ship[] ships = GameObject.FindObjectsOfType<Ship> ();

		for (int i = 0; i < ships.Length; i++) { 					//Iterate through all the ships
			if (ships [i].selected == true) {						//If one of the ships is selected and moving, stop the last movement and move towards new position. Otherwise just move to the clicked position.
				if (ships [i].lastRoutine != null) {
					ships [i].StopMove (ships [i].lastRoutine);
				}
				ships [i].Move (mousePos);
				Instantiate (x, mousePos, Quaternion.identity);
				return;												//The execution of the function stops if a ship was moved, otherwise, it continues.
			}
		}
		if (GameManager.coins >= UnitSelector.selectedShip.GetComponent<ShipAttributes> ().price) { //If player has enough money to purchase the selected ship, spawn it.
			SpawnShip (mousePos);
		}
	}

	void SpawnShip(Vector2 spawnLocation){
		GameManager.coins -= UnitSelector.selectedShip.GetComponent<ShipAttributes> ().price;
		Instantiate (UnitSelector.selectedShip, spawnLocation, Quaternion.identity);

	}
}
