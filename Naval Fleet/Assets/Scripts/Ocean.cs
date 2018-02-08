using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocean : MonoBehaviour {

	public Coroutine lastRoutine = null;

	void OnMouseDown(){
		//Get the position of the click and change it to world coordinates.
		Vector2 mousePos = Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint (mousePos);
		//Iterate through all the ships, find the selected one and move it to position.
		Ship[] ships = GameObject.FindObjectsOfType<Ship> ();
		for (int i = 0; i < ships.Length; i++) {
			if (ships [i].selected == true ) {
				if (lastRoutine != null) {
					ships [i].StopMove (lastRoutine);
				}
				ships [i].Move (mousePos);
			}
		}

	}
}
