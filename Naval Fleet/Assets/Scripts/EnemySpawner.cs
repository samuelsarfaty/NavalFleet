using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float delayStart;
	public float spawnRate;
	public float range;



	void Start(){
		InvokeRepeating ("SpawnEnemy", delayStart, spawnRate);
	}


	void SpawnEnemy(){
		float angle = Random.Range (0.0f, Mathf.PI * 2);
		Vector2 v = new Vector2 (Mathf.Sin (angle), Mathf.Cos (angle));

		Instantiate (enemyPrefab, v * range, Quaternion.identity);
		//Instantiate (enemyPrefab, new Vector2(Random.Range(xMinSpawnPos,xMaxSpawnPos), transform.position.y), Quaternion.identity);
	}


}
