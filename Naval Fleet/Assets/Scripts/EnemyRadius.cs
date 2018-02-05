using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRadius : MonoBehaviour {

	public float damage;
	public float reloadTime;
	public float accuracy;

	private Enemy parentEnemy;
	private Ship engagedShip;

	void Awake(){
		parentEnemy = transform.GetComponentInParent<Enemy> ();
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.GetComponent<Ship> ()) {
			engagedShip = other.GetComponent<Ship>();
			parentEnemy.isAttacking = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.GetComponent<Enemy> ()) {
			parentEnemy.isAttacking = false;
		}
	}
		
}
