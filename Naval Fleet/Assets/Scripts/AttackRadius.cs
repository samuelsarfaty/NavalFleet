using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRadius : MonoBehaviour {

	private Ship parentShip;

	void Awake(){
		parentShip = transform.GetComponentInParent<Ship> ();
	}

	void OnTriggerEnter2D(Collider2D other){ //On trigger, set an engaged enemy for the player and turn on isAttacking to call the fight sequence.
		if (other.GetComponent<Enemy> ()) {
			parentShip.engagedEnemy = other.GetComponent<Enemy>();
			parentShip.RotateShot ();
			parentShip.isAttacking = true;
		}
			
	}

	void OnTriggerExit2D(Collider2D other){ //When enemy is destroyed, is attacking returns to false so that the player regains control of the ship.
		if (other.GetComponent<Enemy> ()) {
			parentShip.isAttacking = false;

		}
	}

		
}
