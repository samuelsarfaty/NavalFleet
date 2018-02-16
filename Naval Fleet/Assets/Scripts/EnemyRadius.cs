using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRadius : MonoBehaviour {

	private Enemy parentEnemy;
	private ShipAttributes attributes;

	public GameObject negCoinEffect;

	void Awake(){
		parentEnemy = transform.GetComponentInParent<Enemy> ();
		attributes = GetComponentInParent<ShipAttributes> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<Ship> ()) {
			parentEnemy.engagedShip = other.GetComponent<Ship>();
			parentEnemy.RotateShot ();
			parentEnemy.isAttacking = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){

		if (other.GetComponent<Ship> ()) {
			//Instantiate (negCoinEffect, transform.position, Quaternion.identity);
			parentEnemy.isAttacking = false;
			parentEnemy.Move ();

			if (attributes.isDying == false && other.GetComponent<Ship>().gaveMoney == false && other.GetComponent<ShipAttributes>().isDying == true) {
				other.GetComponent<Ship> ().gaveMoney = true;
				GameManager.coins -= 50;
				Instantiate (negCoinEffect, transform.position, Quaternion.identity);
			}
		}
	}
		
}
