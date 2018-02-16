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

	public Text startText;


	private LevelManager levelManager;

	void Awake(){
		levelManager = GameObject.FindObjectOfType<LevelManager> ();
	}

	// Use this for initialization
	void Start () {
		coins = startCoins;
		targetText.text = targetCoins.ToString ();
		StartCoroutine (KillText (startText));

	}
	
	// Update is called once per frame
	void Update () {
		moneyText.text = coins.ToString();
		if (coins == targetCoins) {	
			levelManager.LoadLevel ("Win");
		}
	}

	IEnumerator KillText(Text text){
		yield return new WaitForSeconds (3);
		text.gameObject.SetActive (false);
	}

}

		
