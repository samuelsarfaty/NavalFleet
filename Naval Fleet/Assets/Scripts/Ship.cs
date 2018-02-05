﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {


	[HideInInspector]
	public bool canMove = true;
	public bool selected;
	public float speed;

	void Awake(){

	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		// Find all the ships and iterate through them. If it finds this ship, select it and deselect all others.
		Ship[] ships = GameObject.FindObjectsOfType<Ship> ();
		for (int i = 0; i < ships.Length; i++) {
			if (ships [i] == this && ships[i].selected == false) {
				SelectShip ();
			} else {
				ships [i].DeSelectShip ();
			}
		}
	}

	public void SelectShip(){
		selected = true;
	}

	public void DeSelectShip(){
		selected = false;
	}

	IEnumerator MoveToPoint(Vector2 destination){
		canMove = false;																	//Disable player's ability to keep moving until coroutine finishes
		Vector2 currentPos = new Vector2 (transform.position.x, transform.position.y);		//Get the player's current position as Vector 2

		Vector2 diff = destination - currentPos;											//This bit was taken from the Unity Forum.					
		diff.Normalize ();																	// It shows how to rotate the 2D object to look at the destination
		float rotZ = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;							// Link here: https://answers.unity.com/questions/585035/lookat-2d-equivalent-.html
		transform.rotation = Quaternion.Euler (0, 0, rotZ + 90);						

		float step = (speed / (destination - currentPos).magnitude * Time.deltaTime);		//Calculate the value of each step by dividing speed by the difference between the current position and the target position
		float t = 0;
		while (t <= 1.0f) {
			t += step;																		//Increase the value of t by step on each pass. 
			transform.position = Vector2.Lerp (currentPos, destination, t);					//Interpolate the ship's position by t.
			yield return new WaitForEndOfFrame ();											//Wait for next frame and repeat until t = 1.

		}
		canMove = true;																		//Once the moving sequence is finished, the player can move again.

	}

	public void Move(Vector2 destination){ //Use this helper function as the MoveToPoint coroutine cannot be called from outside of this script.
		StartCoroutine (MoveToPoint (destination));
	}

}
