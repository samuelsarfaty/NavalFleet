using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRadius : MonoBehaviour {

	public float damage;
	public float reloadTime;
	public float accuracy;

	private Ship parentShip;
	private Enemy engagedEnemy;

	void Awake(){
		parentShip = transform.GetComponentInParent<Ship> ();
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.GetComponent<Enemy> ()) {
			engagedEnemy = other.GetComponent<Enemy>();
			parentShip.isAttacking = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.GetComponent<Enemy> ()) {
			parentShip.isAttacking = false;
		}
	}
		
}
