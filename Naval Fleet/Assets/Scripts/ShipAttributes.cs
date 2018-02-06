using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAttributes : MonoBehaviour {

	//Separated these attributes from the main script of the ship so that I can access them independently of whether they belong to a friendly or enemy ship.

	public float health;
	public float speed;
	public float damage;
	public float accuracy;
	public float reloadTime;

}
