﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRadius : MonoBehaviour {

	private Ship parentShip;
	private ShipAttributes attributes;

	public GameObject coinEffect;

	void Awake(){
		parentShip = transform.GetComponentInParent<Ship> ();
		attributes = GetComponentInParent<ShipAttributes> ();
	}

	void OnTriggerEnter2D(Collider2D other){ //On trigger, set an engaged enemy for the player and turn on isAttacking to call the fight sequence.
		if (other.GetComponent<Enemy> ()) {
			parentShip.engagedEnemy = other.GetComponent<Enemy>();
			parentShip.RotateShot ();
			parentShip.isEngaged = true;
		}
			
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.GetComponent<Enemy> () && !parentShip.isEngaged) {
			parentShip.engagedEnemy = other.GetComponent<Enemy>();
			parentShip.RotateShot ();
			parentShip.isEngaged = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){ //When enemy is destroyed, is attacking returns to false so that the player regains control of the ship.

		if (other.GetComponent<Enemy> ()) {
			parentShip.isEngaged = false;
			parentShip.transform.Rotate (Vector3.forward * 1); 	//Call rotation on ship so that OnTriggerEnter is called again.
																//This works in case there is more than 1 enemy on the player radius.
			if (attributes.isDying == false && other.GetComponent<Enemy> ().gaveMoney == false && other.GetComponent<ShipAttributes>().isDying == true) {
				other.GetComponent<Enemy> ().gaveMoney = true;
				GameManager.coins += 50;
				Instantiate (coinEffect, transform.position, Quaternion.identity);
				if (parentShip.selected == false) {
					parentShip.DeSelectShip ();
				}
			}

		}
	}

		
}
