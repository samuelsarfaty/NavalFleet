using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAttributes : MonoBehaviour {

	//Here are the variables and functions that work exactly the same way for both friendly and enemy ships.
	//I thought it would have been a good idea to make the friendly and enemy ships inherit from this class but I chose this way since I'm not too acquainted with how inheritance works.

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
