﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Text moneyText;
	public int coins;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		moneyText.text = coins.ToString();
	}
}
