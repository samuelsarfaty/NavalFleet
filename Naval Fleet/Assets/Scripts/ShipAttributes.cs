using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAttributes : MonoBehaviour {

	//Separated these attributes from the main script of the ship so that I can access them independently of whether they belong to a friendly or enemy ship.

	public GameObject deathExplosion;
	public AudioClip explosionSound;
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
	private AudioSource source;

	void Awake(){
		myRenderer = GetComponent<SpriteRenderer> ();
		source = GetComponent<AudioSource> ();
	}

	IEnumerator DeathSequence(){
		isDying = true;											//Used to call this coroutine only once when dying
		myRenderer.enabled = false;	


		if (GetComponent<Ship> ()) {							//Disables script and collider so that the other ship doesn't continue to shoot while the death sequence is going
			GetComponent<Ship> ().enabled = false;
			GetComponent<BoxCollider2D> ().enabled = false;
		
		} else if (GetComponent<Enemy> ()) {
			GetComponent<Enemy> ().enabled = false;
			GetComponent<BoxCollider2D> ().enabled = false;
		}


		source.PlayOneShot (explosionSound);
		Instantiate (deathExplosion, transform.position, Quaternion.identity);
		yield return new WaitForSeconds (deathExplosionDuration);
		Destroy (this.gameObject);

	}

	public void Die(){
		StartCoroutine (DeathSequence ());
	}

}
