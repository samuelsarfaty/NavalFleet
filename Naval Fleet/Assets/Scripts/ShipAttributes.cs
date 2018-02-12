using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAttributes : MonoBehaviour {

	//Separated these attributes from the main script of the ship so that I can access them independently of whether they belong to a friendly or enemy ship.

	public GameObject deathExplosion;
	public float deathExplosionDuration;

	[HideInInspector]
	public bool isDying = false;
	public int price;
	public float health;
	public float speed;
	public float damage;
	public float accuracy;
	public float reloadTime;

	private SpriteRenderer myRenderer;

	void Awake(){
		myRenderer = GetComponent<SpriteRenderer> ();
	}

	IEnumerator DeathSequence(){
		isDying = true;
		myRenderer.enabled = false;
		Instantiate (deathExplosion, transform.position, Quaternion.identity);
		yield return new WaitForSeconds (deathExplosionDuration);
		Destroy (this.gameObject);

	}

	public void Die(){
		StartCoroutine (DeathSequence ());
	}

}
