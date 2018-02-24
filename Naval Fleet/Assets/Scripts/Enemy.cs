using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[HideInInspector]
	public bool isAttacking = false;
	[HideInInspector]
	public Ship engagedShip;
	[HideInInspector]
	public ShipAttributes attributes;
	public AudioClip cannonSound;
	public Vector2 target;
	public bool gaveMoney = false;

	private Mothership mothership;
	private GameObject shootingEffect;
	private AudioSource source;
	private bool canShoot = true;

	void Awake(){
		mothership = GameObject.FindObjectOfType<Mothership> ();
		attributes = GetComponent<ShipAttributes> ();
		shootingEffect = transform.GetChild (0).gameObject;
		source = GetComponent<AudioSource> ();
		target = new Vector2 (mothership.transform.position.x, mothership.transform.position.y);

	}
		
	void Start () {
		//Make enemies go straight to the mothership
		Move();
		shootingEffect.gameObject.SetActive (false);

	}

	void Update () {
		if (isAttacking && canShoot) {
			StartCoroutine (FightSequence ());
		}

		if (attributes.health <= 0 && attributes.isDying == false) {
			attributes.Die ();
		}
	}

	void OnTriggerEnter2D(Collider2D other){

		//If mothership enters trigger, enemy wins
		if (other.GetComponent<Mothership> ()) {
			other.GetComponent<Mothership> ().Die ();
			Destroy (this.gameObject);
		}
	}

	IEnumerator MoveToPoint(Vector2 destination){
		Vector2 currentPos = new Vector2 (transform.position.x, transform.position.y);		//Get the player's current position as Vector 2

		Vector2 diff = destination - currentPos;											//This bit was taken from the Unity Forum.					
		diff.Normalize ();																	// It shows how to rotate the 2D object to look at the destination
		float rotZ = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;							// Link here: https://answers.unity.com/questions/585035/lookat-2d-equivalent-.html
		transform.rotation = Quaternion.Euler (0, 0, rotZ + 90);						

		float step = (attributes.speed / (destination - currentPos).magnitude * Time.deltaTime);		//Calculate the value of each step by dividing speed by the difference between the current position and the target position
		float t = 0;
		while (t <= 1.0f && !isAttacking) {
			t += step;																		//Increase the value of t by step on each pass. 
			transform.position = Vector2.Lerp (currentPos, destination, t);					//Interpolate the ship's position by t.
			yield return new WaitForEndOfFrame ();											//Wait for next frame and repeat until t = 1.

		}

	}

	public void Move(){
		StartCoroutine (MoveToPoint (target));
	}

	IEnumerator FightSequence(){
		Attack ();														//Call the attack function
		canShoot = false;
		yield return new WaitForSeconds (attributes.reloadTime);		//Wait for reload time
		shootingEffect.gameObject.SetActive (false);
		canShoot = true;												//Allow to shoot again
	}

	void Attack(){
		shootingEffect.gameObject.SetActive(true);						//Show shooting particle effect
		source.PlayOneShot (cannonSound);								//Play the shooting sound

		float hitChance = Random.value;													//The original design included an random accuracy logic. The accuracy value goes from 0 to 1.

		if (Random.value <= attributes.accuracy) {										//If the randomly generated value is lower than the ship's accuracy, the shot would be a hit.
																						//This feature was later removed, so all the ships' accuracy level was set at 1. Hence, shots will always hit.

			if (engagedShip.attackStance == true || engagedShip.attackStance == null) { //If the ship is in attack mode or no mode, the enemy deals full damage.
				engagedShip.attributes.health -= attributes.damage;
			} else {
				engagedShip.attributes.health -= attributes.damage * 0.3f; //If the ship is in defense mode, the enemy deals 30% of the damage it normally would.
			}
		}
	}

	public void RotateShot(){
		shootingEffect.transform.LookAt (engagedShip.transform);
	}

		
}
