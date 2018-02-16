using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float delayStart;
	public float spawnRate;
	public float range;

	void OnEnable(){
		InvokeRepeating ("SpawnEnemy", delayStart, spawnRate);
	}

	void SpawnEnemy(){ //This function spawns enemies around the player
		float angle = Random.Range (0.0f, Mathf.PI * 2);
		Vector2 v = new Vector2 (Mathf.Sin (angle), Mathf.Cos (angle)); 	//Got this from https://answers.unity.com/questions/157061/spawning-at-a-random-position-away-from-the-player.html
																			//My understanding is that creates a random vector 2 around the perimeter of a cricle.

		Instantiate (enemyPrefab, v * range, Quaternion.identity);
	}




}
