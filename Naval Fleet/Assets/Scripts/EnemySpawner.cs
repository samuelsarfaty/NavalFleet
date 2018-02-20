using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float delayStart;

	public float spawnRate;
	private float startRate;

	public SpawnRange[] spawnPositions;

	private int numberOfPlayerShips;

	void OnEnable(){

		startRate = spawnRate;

		StartCoroutine (SpawnEnemy ());
	}

	IEnumerator SpawnEnemy(){

		Ship[] playerShips = GameObject.FindObjectsOfType<Ship> ();
		numberOfPlayerShips = 0;

		foreach (Ship ship in playerShips) {
			if (!ship.GetComponent<FishingBoat> ()) {
				numberOfPlayerShips++;
			}
		}

		spawnRate = startRate;
		spawnRate -= numberOfPlayerShips * 1.5f;

		SpawnRange selectedPosition = spawnPositions [Random.Range (0, spawnPositions.Length)];

		if (selectedPosition.GetComponent<SpawnRange> ().isHorizontal) {
			Vector2 v = new Vector2 (Random.Range (selectedPosition.min, selectedPosition.max), selectedPosition.transform.position.y);
			Instantiate (enemyPrefab, v, Quaternion.identity);

		} else if (selectedPosition.GetComponent<SpawnRange> ().isHorizontal == false) {
			Vector2 v = new Vector2 (selectedPosition.transform.position.x, Random.Range (selectedPosition.min, selectedPosition.max));
			Instantiate (enemyPrefab, v, Quaternion.identity);
		}

		yield return new WaitForSeconds (spawnRate);
		StartCoroutine (SpawnEnemy ());

	}


}
