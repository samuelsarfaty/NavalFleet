using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float spawnRate;
	public float xMinSpawnPos;
	public float xMaxSpawnPos;

	void Start(){
		InvokeRepeating ("SpawnEnemy", 5.0f, spawnRate);
	}


	void SpawnEnemy(){
		Instantiate (enemyPrefab, new Vector2(Random.Range(xMinSpawnPos,xMaxSpawnPos), transform.position.y), Quaternion.identity);
	}


}
