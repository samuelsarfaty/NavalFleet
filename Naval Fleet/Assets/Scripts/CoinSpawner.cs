using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour {

	public GameObject coinPrefab;
	public float spawnRate;

	public float minX, maxX, minY, maxY;

	void Start(){
		InvokeRepeating ("SpawnCoin", spawnRate, spawnRate);
	}

	void SpawnCoin(){
		float xPos = Random.Range (minX, maxX);
		float yPos = Random.Range (minY, maxY);

		Instantiate (coinPrefab, new Vector2 (xPos, yPos), Quaternion.identity);
	}
}
