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
		//InvokeRepeating ("SpawnEnemy", delayStart, spawnRate);

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
		spawnRate -= numberOfPlayerShips * 2;

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











	/*void SpawnEnemy(){ //This function spawns enemies around the player
		float angle = Random.Range (0.0f, Mathf.PI * 2);
		Vector2 v = new Vector2 (Mathf.Sin (angle), Mathf.Cos (angle)); 	//Got this from https://answers.unity.com/questions/157061/spawning-at-a-random-position-away-from-the-player.html
																			//My understanding is that creates a random vector 2 around the perimeter of a cricle.

		Instantiate (enemyPrefab, v * range, Quaternion.identity);
	}*/

	/*IEnumerator SpawnEnemy(){
		numberOfPlayerShips = 0;

		Ship[] playerShips = GameObject.FindObjectsOfType<Ship> ();

		foreach (Ship ship in playerShips) {
			if (!ship.GetComponent<FishingBoat> ()) {
				numberOfPlayerShips++;
			}

		}

		float angle = Random.Range (0.0f, Mathf.PI * 2);
		Vector2 v = new Vector2 (Mathf.Sin (angle), Mathf.Cos (angle)); 

		Instantiate (enemyPrefab, v * range, Quaternion.identity);

		yield return new WaitForSeconds (spawnRate);
		StartCoroutine (SpawnEnemy ());
	}*/
	







}
