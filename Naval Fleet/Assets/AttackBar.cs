using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackBar : MonoBehaviour {

	public float speed;
	public bool increasing = true;

	private Image bar;

	void Awake(){
		bar = GetComponent<Image> ();
	}

	// Update is called once per frame
	void Update () {
		if (bar.fillAmount <= 0) {
			increasing = true;
		} else if (bar.fillAmount >= 1) {
			print ("filled");
			increasing = false;
		}

		if (increasing) {
			bar.fillAmount += speed * Time.deltaTime;
		} else if (!increasing){
			bar.fillAmount -= speed * Time.deltaTime;
		}
			
		}
	}
