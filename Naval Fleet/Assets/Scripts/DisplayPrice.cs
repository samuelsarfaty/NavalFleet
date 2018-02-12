using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPrice : MonoBehaviour {

	private float price;
	private Text myText;

	void Awake(){
		price = GetComponentInParent<ShipAttributes> ().price;
		myText = GetComponent<Text> ();
	}

	// Use this for initialization
	void Start () {
		myText.text = price.ToString ();
	}
}
