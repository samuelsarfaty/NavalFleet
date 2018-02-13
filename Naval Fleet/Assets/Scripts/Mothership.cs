using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mothership : MonoBehaviour {

	public float delayBetweenExplosions;

	public GameObject deathExplosion;
	public GameObject explosionLocation_1, explosionLocation_2, explosionLocation_3; 

	private LevelManager lm;

	void Awake(){
		lm = GameObject.FindObjectOfType<LevelManager> ();
	}

	IEnumerator MothershipDeath(){
		Instantiate (deathExplosion, explosionLocation_1.transform.position, Quaternion.identity);
		yield return new WaitForSeconds (delayBetweenExplosions);
		Instantiate (deathExplosion, explosionLocation_2.transform.position, Quaternion.identity);
		yield return new WaitForSeconds (delayBetweenExplosions);
		Instantiate (deathExplosion, explosionLocation_3.transform.position, Quaternion.identity);
		yield return new WaitForSeconds (1);
		lm.LoadLevel ("Lose");
	}

	public void Die(){
		StartCoroutine (MothershipDeath());
	}

}
