using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mothership : MonoBehaviour {

	public float delayBetweenExplosions;

	public GameObject deathExplosion;
	public AudioClip deathExplosionSound;
	public GameObject explosionLocation_1, explosionLocation_2, explosionLocation_3; 

	private LevelManager lm;
	private AudioSource source;

	void Awake(){
		lm = GameObject.FindObjectOfType<LevelManager> ();
		source = GetComponent<AudioSource> ();
	}

	IEnumerator MothershipDeath(){ //Spawm 3 explosions and then load the "Lose" scene
		Instantiate (deathExplosion, explosionLocation_1.transform.position, Quaternion.identity);
		yield return new WaitForSeconds (delayBetweenExplosions);
		Instantiate (deathExplosion, explosionLocation_2.transform.position, Quaternion.identity);
		yield return new WaitForSeconds (delayBetweenExplosions);
		Instantiate (deathExplosion, explosionLocation_3.transform.position, Quaternion.identity);
		source.PlayOneShot (deathExplosionSound);
		yield return new WaitForSeconds (3);
		lm.LoadLevel ("Lose");
	}

	public void Die(){ //Helper function used to call the MothershipDeath coroutine from outside this script.
		StartCoroutine (MothershipDeath());
	}

}
