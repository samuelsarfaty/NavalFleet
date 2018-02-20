using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;

	//public float spawnRate;
	//private float startRate;

	public int enemyCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public SpawnRange[] spawnPositions;

	private int numberOfPlayerShips;

	void OnEnable(){

		StartCoroutine(SpawnWaves());
	}

	IEnumerator SpawnWaves(){
		yield return new WaitForSeconds (startWait);

		while (true){

			enemyCount = 0;
			Ship[] playerShips = GameObject.FindObjectsOfType<Ship> ();

			foreach (Ship ship in playerShips){
				if (!ship.GetComponent<FishingBoat>()){
					enemyCount++;	
				}
			}

			/*if (enemyCount <= 2) {
				enemyCount = 2;
			}*/

			for (int i = 0; i < enemyCount + 1; i++) {
				SpawnRange selectedPosition = spawnPositions [Random.Range (0, spawnPositions.Length)];

				if (selectedPosition.GetComponent<SpawnRange> ().isHorizontal) {
					Vector2 v = new Vector2 (Random.Range (selectedPosition.min, selectedPosition.max), selectedPosition.transform.position.y);
					Instantiate (enemyPrefab, v, Quaternion.identity);

				} else if (selectedPosition.GetComponent<SpawnRange> ().isHorizontal == false) {
					Vector2 v = new Vector2 (selectedPosition.transform.position.x, Random.Range (selectedPosition.min, selectedPosition.max));
					Instantiate (enemyPrefab, v, Quaternion.identity);
				}

				yield return new WaitForSeconds (spawnWait);
			}

			yield return new WaitForSeconds (waveWait);
		}
	}


}
