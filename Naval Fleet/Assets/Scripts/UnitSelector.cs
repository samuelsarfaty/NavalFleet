using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelector : MonoBehaviour {

	public GameObject shipPrefab;
	public static GameObject selectedShip;

	private UnitSelector[] buttons;
	private Color startColor;

	void Awake(){
		buttons = GameObject.FindObjectsOfType<UnitSelector> ();
		startColor = GetComponent<SpriteRenderer> ().color;
	}

	void Start(){
		DeSelectButton ();
	}

	void OnMouseDown(){
		Ship[] ships = GameObject.FindObjectsOfType<Ship> (); //Deselect whatever ship is selected when clicking on the buttons.
		foreach (Ship ship in ships) {
			ship.DeSelectShip ();
		}
		foreach (UnitSelector selectedShip in buttons) { //'Turn off' all buttons and then enable the one clicked.
			selectedShip.DeSelectButton ();
		}
		SelectButton ();
	}

	 void SelectButton(){
		GetComponent<SpriteRenderer> ().color = startColor;
		GetComponentInChildren<Canvas> ().enabled = true;
		selectedShip = shipPrefab;
	}	

	public void DeSelectButton(){
		GetComponent<SpriteRenderer> ().color = Color.black;
		GetComponentInChildren<Canvas> ().enabled = false;
	}
		
}
