using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRotation : MonoBehaviour {

	Quaternion rotation;

	void Awake (){
		rotation = transform.rotation;
	}


	void LateUpdate(){
		transform.rotation = rotation; //Freezes the rotation of the ship's canvas so that it is always facing the right direction regardless of the ship's rotation.
	}
}
