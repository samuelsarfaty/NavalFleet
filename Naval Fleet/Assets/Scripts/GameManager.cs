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
	public GameObject enemyPrefab;
	public Transform firstEnemySpawnPos;

	public Text startText;
	public Text buyText;


	private LevelManager levelManager;
	private Animator anim;
	private bool firstCoinsTaken = false;

	void Awake(){
		levelManager = GameObject.FindObjectOfType<LevelManager> ();
		anim = GetComponent<Animator> ();
		buyText.gameObject.SetActive (false);
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
			SpawnEnemy (firstEnemySpawnPos);
			firstCoinsTaken = true;
			anim.SetTrigger ("ShowEnemy");

		}


	}

	IEnumerator KillText(Text text, float animSeconds){
		yield return new WaitForSeconds (animSeconds);
		text.gameObject.SetActive (false);
	}

	void SpawnEnemy(Transform posToSpawn){
		Instantiate (enemyPrefab, posToSpawn.transform.position, Quaternion.identity);
	}
}

		
