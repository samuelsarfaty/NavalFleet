using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAttributes : MonoBehaviour {

	//Here are the variables and functions that work exactly the same way for both friendly and enemy ships.
	//I thought it would have been a good idea to make the friendly and enemy ships inherit from this class but I chose this way since I'm not too acquainted with how inheritance works.

	public GameObject deathExplosion;
	public AudioClip deathExplosionSound;
	public float deathExplosionDuration;

	[HideInInspector]
	public bool isDying = false;
	public int price;
	public float health;
	public float speed;
	public float damage;
	public float accuracy;
	public float reloadTime;
	public SpriteRenderer myRenderer;
	public GameObject healthPickup;

	private GameObject propertiesCanvas;
	private AudioSource source;
	private BoxCollider2D col;
	private CircleCollider2D rad;

	void Awake(){
		/*if (GetComponent<SpriteRenderer> ()) {
			myRenderer = GetComponent<SpriteRenderer> ();
		} else {
			myRenderer = GetComponentInChildren<SpriteRenderer> ();
		}*/

		propertiesCanvas = transform.GetChild(0).gameObject;

		source = GetComponent<AudioSource> ();
		col = GetComponent<BoxCollider2D> ();
		rad = GetComponentInChildren<CircleCollider2D> ();
	}

	IEnumerator DeathSequence(){
		isDying = true;
		myRenderer.enabled = false;
		col.enabled = false;
		propertiesCanvas.SetActive (false);
		//Destroy(propertiesCanvas.gameObject);

		if (rad) { //Needed to add this separate condition because the fishing boat doesn't have an attack radius
			rad.enabled = false;
		}

		source.PlayOneShot (deathExplosionSound);
		Instantiate (deathExplosion, transform.position, Quaternion.identity);

		if (healthPickup) {
			Instantiate (healthPickup, transform.position, Quaternion.identity);
		}

		yield return new WaitForSeconds (deathExplosionDuration);
		Destroy (this.gameObject);

	}

	public void Die(){
		StartCoroutine (DeathSequence ());
	}

}
