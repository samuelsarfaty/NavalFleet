using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public Text moneyText;
	public Text targetText;
	public static int coins;
	public int startCoins;
	public int targetCoins;
	public float delayEnemyStart;
	public GameObject enemyPrefab;

	public Transform EnemySpawnPos_0;

	public Text startText;
	public Text buyText;

	private EnemySpawner enemySpawner;
	private LevelManager levelManager;
	private Animator anim;
	private bool firstCoinsTaken = false;

	void Awake(){
		levelManager = GameObject.FindObjectOfType<LevelManager> ();
		anim = GetComponent<Animator> ();
		buyText.gameObject.SetActive (false);
		enemySpawner = GameObject.FindObjectOfType<EnemySpawner> ();

		enemySpawner.GetComponent<EnemySpawner> ().enabled = false;
	}

	// Use this for initialization
	void Start () {
		coins = startCoins;
		targetText.text = targetCoins.ToString ();
		StartCoroutine (KillText (startText, 3));

	}
	
	// Update is called once per frame
	void Update () {
		moneyText.text = coins.ToString();
		if (coins == targetCoins) {	
			levelManager.LoadLevel ("Win");
		}

		if (coins == 100 && !firstCoinsTaken) {
			buyText.gameObject.SetActive (true);
			StartCoroutine (KillText (buyText, 1.7f));
			print ("first enemy");
			SpawnEnemy (EnemySpawnPos_0);
			firstCoinsTaken = true;
			anim.SetTrigger ("ShowEnemy");
			Invoke ("EnableSpawner", delayEnemyStart); 

		}

		if (coins <= 0) {
			coins = 0;
		}


	}

	IEnumerator KillText(Text text, float animSeconds){
		yield return new WaitForSeconds (animSeconds);
		text.gameObject.SetActive (false);
	}

	void SpawnEnemy(Transform posToSpawn){
		Instantiate (enemyPrefab, posToSpawn.transform.position, Quaternion.identity);
	}

	void EnableSpawner(){
		enemyPrefab.GetComponent<EnemySpawner> ().enabled = true;
	}
}

		
