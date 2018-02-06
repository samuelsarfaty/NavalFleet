using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	private ShipAttributes myShip;
	private Image healthBar;
	private float startHealth;

	void Awake(){
		myShip = transform.GetComponentInParent<ShipAttributes> ();
		healthBar = GetComponent<Image> ();
		startHealth = myShip.health;
	}
		
	
	// Update is called once per frame
	void Update () {
		healthBar.fillAmount = myShip.health / startHealth;
	}
}
