using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MothershipHealth : MonoBehaviour {

	private Mothership mothership;
	private Image healthBar;
	private float startHealth;

	void Awake(){
		mothership = transform.GetComponentInParent<Mothership> ();
		healthBar = GetComponent<Image> ();
		//startHealth = mothership.health;
	}
	
	// Update is called once per frame
	void Update () {
		//healthBar.fillAmount = mothership.health / startHealth;
	}
}
