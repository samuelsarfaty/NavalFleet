using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public Text moneyText;
	public static int coins;
	public int startCoins;

	private LevelManager levelManager;

	void Awake(){
		levelManager = GameObject.FindObjectOfType<LevelManager> ();
	}

	// Use this for initialization
	void Start () {
		coins = startCoins;
	}
	
	// Update is called once per frame
	void Update () {
		moneyText.text = coins.ToString();
	}
}
