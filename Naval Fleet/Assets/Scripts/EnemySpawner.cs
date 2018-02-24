using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;

	//public float spawnRate;
	//private float startRate;

	public int enemyCount;
	public int maxEnemies;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public bool allEnemiesKilled = false;

	public SpawnRange[] spawnPositions;

	private int numberOfPlayerShips;

	void OnEnable(){

		StartCoroutine(SpawnWaves());
	}

	IEnumerator SpawnWaves(){
		yield return new WaitForSeconds (startWait);									//Wait for first spawn call

		while (true){																	//Keep running this loop throught the entire game

			yield return new WaitForSeconds (waveWait);									//Wait for the interval between waves
			enemyCount = 0;
			Ship[] playerShips = GameObject.FindObjectsOfType<Ship> ();

			foreach (Ship ship in playerShips){											//This loop iterates through all the player combat ships in the game and adds to 'enemyCount'
				if (!ship.GetComponent<FishingBoat>()){
					enemyCount++;	
				}
			}

			if (enemyCount >= maxEnemies) {												//There can't be more enemies than maxEnemies.
				enemyCount = maxEnemies;
			}

			for (int i = 0; i < enemyCount + 1; i++) {													//Every wave spawn one more enemy than the player has ships
				SpawnRange selectedPosition = spawnPositions [Random.Range (0, spawnPositions.Length)];

				if (selectedPosition.GetComponent<SpawnRange> ().isHorizontal) {						//Randomly choose whether to spawn enemies along the sides or top/bottom of the screen
					Vector2 v = new Vector2 (Random.Range (selectedPosition.min, selectedPosition.max), selectedPosition.transform.position.y);
					Instantiate (enemyPrefab, v, Quaternion.identity);

				} else if (selectedPosition.GetComponent<SpawnRange> ().isHorizontal == false) {
					Vector2 v = new Vector2 (selectedPosition.transform.position.x, Random.Range (selectedPosition.min, selectedPosition.max));
					Instantiate (enemyPrefab, v, Quaternion.identity);
				}

				yield return new WaitForSeconds (spawnWait); //Waits for next enemy to appear in a wave
			}
			yield return new WaitUntil(() => allEnemiesKilled); //Waits until all enemies are killed and then spawns the new wave
		}
	}


}
