using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[HideInInspector]
	public bool isAttacking = false;
	public float speed;
	public float damageToMotherhip;

	private Mothership mothership;
	private Vector2 target;

	void Awake(){
		mothership = GameObject.FindObjectOfType<Mothership> ();
		target = new Vector2 (mothership.transform.position.x, mothership.transform.position.y);
	}

	// Use this for initialization
	void Start () {
		//Make enemies go straight to the mothership
		StartCoroutine (MoveToPoint (target));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){

		//If mothership enters trigger, deal damage and destroy
		if (other.GetComponent<Mothership> ()) {
			mothership.health -= damageToMotherhip;
			Destroy (gameObject);
		}
	}

	IEnumerator MoveToPoint(Vector2 destination){
		Vector2 currentPos = new Vector2 (transform.position.x, transform.position.y);		//Get the player's current position as Vector 2

		Vector2 diff = destination - currentPos;											//This bit was taken from the Unity Forum.					
		diff.Normalize ();																	// It shows how to rotate the 2D object to look at the destination
		float rotZ = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;							// Link here: https://answers.unity.com/questions/585035/lookat-2d-equivalent-.html
		transform.rotation = Quaternion.Euler (0, 0, rotZ + 90);						

		float step = (speed / (destination - currentPos).magnitude * Time.deltaTime);		//Calculate the value of each step by dividing speed by the difference between the current position and the target position
		float t = 0;
		while (t <= 1.0f && !isAttacking) {
			t += step;																		//Increase the value of t by step on each pass. 
			transform.position = Vector2.Lerp (currentPos, destination, t);					//Interpolate the ship's position by t.
			yield return new WaitForEndOfFrame ();											//Wait for next frame and repeat until t = 1.

		}

	}
}
