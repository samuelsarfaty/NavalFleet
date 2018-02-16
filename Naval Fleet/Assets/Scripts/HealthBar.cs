using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	[HideInInspector]
	public float startHealth;

	private ShipAttributes myShip;
	private Image healthBar;


	//This script gets the parent's health and displays it on the bar filling.

	void Awake(){
		myShip = transform.GetComponentInParent<ShipAttributes> ();
		healthBar = GetComponent<Image> ();
		startHealth = myShip.health;
	}

	void Start(){
	}
		
	
	// Update is called once per frame
	void Update () {
		healthBar.fillAmount = myShip.health / startHealth;
	}

	public void SetColor(Color color){
			healthBar.color = color;
	}
}
