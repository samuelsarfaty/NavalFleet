using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour {

	[HideInInspector]
	public bool isAttacking = false;
	[HideInInspector]
	public Enemy engagedEnemy;
	[HideInInspector]
	public ShipAttributes attributes;
	public bool selected;
	public AudioClip cannonSound;
	public Coroutine lastRoutine;
	public AttackBar attackBar;
	public Reloader reloader;

	private GameObject propertiesCanvas;
	private GameObject shootingEffect;
	private AudioSource source;
	private Animator anim;
	private bool canShoot = true;


	void Awake(){
		propertiesCanvas = transform.GetChild (0).gameObject; 
		shootingEffect = transform.GetChild(1).gameObject;

		reloader.gameObject.SetActive (false);

		attributes = GetComponent<ShipAttributes>();
		source = GetComponent<AudioSource>();
		anim = GetComponent<Animator> ();
	}
		
	void Start () {
		propertiesCanvas.gameObject.SetActive (false);
		shootingEffect.gameObject.SetActive (false);
	}

	void Update () {
		if (isAttacking && canShoot) {
			//StartCoroutine (FightSequence ());
		}

		if (attributes.health <= 0 && attributes.isDying == false) {
			attributes.Die ();
		}
			
	}

	void OnMouseDown(){
		// Find all the ships and iterate through them. If it finds this ship, select it and deselect all others.
		Ship[] ships = GameObject.FindObjectsOfType<Ship> ();
		for (int i = 0; i < ships.Length; i++) {
			if (ships [i] == this) {
				SelectShip ();

				if (isAttacking && canShoot) {
					attributes.damage = attackBar.bar.fillAmount * 10;
					Attack ();
					StartCoroutine (Reload ());

				}

			} else {
				ships [i].DeSelectShip ();
			}
		}
	}

	void OnMouseUp(){
		shootingEffect.gameObject.SetActive (false);
	}

	public void SelectShip(){ //This function includes all the actions to take when a ship is selected.
		selected = true;
		anim.SetBool ("Selected", true);
		propertiesCanvas.gameObject.SetActive (true);

	}

	public void DeSelectShip(){ //This function does the opposite of SelectShip.
		selected = false;
		anim.SetBool ("Selected", false);
		propertiesCanvas.gameObject.SetActive (false);
	}

	public void Move(Vector2 destination){ //Use this helper function as the MoveToPoint coroutine cannot be called from outside of this script. Set lastRoutine and start moving towards position.
		lastRoutine = StartCoroutine(MoveToPoint(destination));
	}

	public void StopMove(Coroutine coroutineToStop){ //When moving multiple times, this function stops the previous MoveToPoint coroutine.
		StopCoroutine (coroutineToStop);
	}
		
	public void RotateShot(){ //Rotates the cannon particle effect to look at the enemy ship. Called from the AttackRadius script.
		shootingEffect.transform.LookAt (engagedEnemy.transform);
	}

	IEnumerator MoveToPoint(Vector2 destination){
		if (!isAttacking) {																		//Player only allowed to move if he is not engaged
			Vector2 currentPos = new Vector2 (transform.position.x, transform.position.y);		//Get the player's current position as Vector 2
																						
			Vector2 diff = destination - currentPos;											//This bit was taken from the Unity Forum.					
			diff.Normalize ();																	// It shows how to rotate the 2D object to look at the destination
			float rotZ = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;							// Link here: https://answers.unity.com/questions/585035/lookat-2d-equivalent-.html
			transform.rotation = Quaternion.Euler (0, 0, rotZ + 90);						

			float step = (attributes.speed / (destination - currentPos).magnitude * Time.deltaTime);		//Calculate the value of each step by dividing speed by the difference between the current position and the target position
			float t = 0;
			while (t <= 1.0f && !isAttacking) {
				t += step;																	//Increase the value of t by step on each pass. 
				transform.position = Vector2.Lerp (currentPos, destination, t);				//Interpolate the ship's position by t.
				yield return new WaitForEndOfFrame ();										//Wait for next frame and repeat until t = 1.
			
			}
		}
	}

	/*IEnumerator FightSequence(){ // Calls the attack function once, then disables the ability to attack again until reload time is complete.
		Attack ();
		canShoot = false;
		yield return new WaitForSeconds (attributes.reloadTime);
		shootingEffect.gameObject.SetActive (false);
		canShoot = true;
	}*/

	void Attack(){ //Generate a random value from 0-1. If the number is lower than accuracy, ship hits enemy. Otherwise wait for reload and try agian.
		shootingEffect.gameObject.SetActive(true);
		float hitChance = Random.value;
		source.PlayOneShot (cannonSound);

		if (hitChance <= attributes.accuracy) {
			engagedEnemy.attributes.health -= attributes.damage;
		}
	}

	IEnumerator Reload(){
		canShoot = false;
		reloader.gameObject.SetActive (true);
		yield return new WaitForSeconds (attributes.reloadTime);
		canShoot = true;
		reloader.gameObject.SetActive (false);
	}
		
}
