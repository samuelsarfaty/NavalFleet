﻿using System.Collections;
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
		foreach (UnitSelector selectedShip in buttons) {
			selectedShip.DeSelectButton ();
		}
		SelectButton ();
	}

	void SelectButton(){
		GetComponent<SpriteRenderer> ().color = startColor;
		GetComponentInChildren<Canvas> ().enabled = true;
		selectedShip = shipPrefab;
	}	

	void DeSelectButton(){
		GetComponent<SpriteRenderer> ().color = Color.black;
		GetComponentInChildren<Canvas> ().enabled = false;
	}
}
