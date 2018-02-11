using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

	public float secondsToKill;

	void Awake(){
		Invoke ("Kill", secondsToKill);
	}

	void Kill(){
		Destroy(this.gameObject);
	}
}
