using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private AudioSource source;

	void Awake(){
		source = GetComponent<AudioSource> ();
	}

	// Use this for initialization
	void Start () {
		//source.Play ();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
