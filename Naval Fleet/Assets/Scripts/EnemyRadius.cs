using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRadius : MonoBehaviour {

	private Enemy parentEnemy;

	void Awake(){
		parentEnemy = transform.GetComponentInParent<Enemy> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<Ship> ()) {
			parentEnemy.engagedShip = other.GetComponent<Ship>();
			parentEnemy.isAttacking = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.GetComponent<Ship> ()) {
			parentEnemy.isAttacking = false;
			parentEnemy.Move ();
		}
	}
		
}
