using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour {

	[HideInInspector]
	public bool isEngaged = false;
	[HideInInspector]
	public Enemy engagedEnemy;
	[HideInInspector]
	public ShipAttributes attributes;
	public bool selected;
	public bool gaveMoney = false;
	public AudioClip cannonSound;
	public AudioClip repairSound;
	public AudioClip attackHorn;
	public Coroutine lastRoutine;
	public AttackBar attackBar;
	public Reloader reloader;

	[HideInInspector]
	public  AudioSource source;

	public bool ? attackStance;

	private UnitSelector[] uSelector;
	private HealthBar healthBar;
	private GameObject propertiesCanvas;
	private GameObject shootingEffect;
	private Animator anim;
	private bool canShoot = true;



	void Awake(){
		propertiesCanvas = transform.GetChild (0).gameObject; 
		shootingEffect = transform.GetChild(1).gameObject;

		if (reloader) {
			reloader.gameObject.SetActive (false);
		}

		uSelector = GameObject.FindObjectsOfType<UnitSelector> ();

		attributes = GetComponent<ShipAttributes>();
		source = GetComponent<AudioSource>();
		anim = GetComponent<Animator> ();
		healthBar = GetComponentInChildren <HealthBar> ();
		propertiesCanvas.gameObject.SetActive (false);

		Ship[] ships = GameObject.FindObjectsOfType<Ship> ();
		for (int i = 0; i < ships.Length; i++) {
			if (ships [i] == this) {
				SelectShip ();

			} else {
				ships [i].DeSelectShip ();
			}
		}

	}
		
	void Start () {
		
		shootingEffect.gameObject.SetActive (false);
	}

	void Update () {
		if (isEngaged) {
			//StartCoroutine (FightSequence ());
			propertiesCanvas.SetActive(true);
		}

		if (attributes.health <= 0 && attributes.isDying == false) {
			StopCoroutine (Reload ());
			attackStance = null;
			attributes.Die ();
		}
			
	}

	void OnMouseDown(){
		// Find all the ships and iterate through them. If it finds this ship, select it and deselect all others.
		Ship[] ships = GameObject.FindObjectsOfType<Ship> ();

		/*if (isEngaged && canShoot && selected && attackStance == true) {//Checks: enemy on radius, not reloading, selected, and in attack mode
			attributes.damage = attackBar.bar.fillAmount * 10;
			Attack ();
			StartCoroutine (Reload ());

		}*/

		//Attack ();

		foreach (UnitSelector selectedShip in uSelector) {
			selectedShip.DeSelectButton ();
		}

		for (int i = 0; i < ships.Length; i++) {
			if (ships [i] == this) {
				SelectShip ();

			} else {
				ships [i].DeSelectShip ();
			}
		}
	}

	void OnMouseUp(){
		shootingEffect.gameObject.SetActive (false);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<RepairTool> ()) {
			source.PlayOneShot (repairSound);
		}
	}

	public void SelectShip(){ //This function includes all the actions to take when a ship is selected.
		selected = true;
		anim.SetBool ("Selected", true);
		if (propertiesCanvas.activeSelf == false) {
			propertiesCanvas.gameObject.SetActive (true);
		}

	}

	public void DeSelectShip(){ //This function does the opposite of SelectShip.
		selected = false;
		anim.SetBool ("Selected", false);
		if (!isEngaged) {
			propertiesCanvas.gameObject.SetActive (false);
		}
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
		if (!isEngaged) {																		//Player only allowed to move if he is not engaged
			Vector2 currentPos = new Vector2 (transform.position.x, transform.position.y);		//Get the player's current position as Vector 2
																						
			Vector2 diff = destination - currentPos;											//This bit was taken from the Unity Forum.					
			diff.Normalize ();																	// It shows how to rotate the 2D object to look at the destination
			float rotZ = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;							// Link here: https://answers.unity.com/questions/585035/lookat-2d-equivalent-.html
			transform.rotation = Quaternion.Euler (0, 0, rotZ + 90);						

			float step = (attributes.speed / (destination - currentPos).magnitude * Time.deltaTime);	//Calculate the value of each step by dividing speed by the difference 
																										//between the current position and the target position
			float t = 0;
			while (t <= 1.0f && !isEngaged) {
				t += step;																	//Increase the value of t by step on each pass. 
				transform.position = Vector2.Lerp (currentPos, destination, t);				//Interpolate the ship's position by t.
				yield return new WaitForEndOfFrame ();										//Wait for next frame and repeat until t = 1.
			
			}
		}
	}

	public void Attack(){

		if (isEngaged && canShoot && attackStance == true) {	//Checks: enemy on radius, not reloading, selected, and in attack mode

			attributes.damage = attackBar.bar.fillAmount * 10; 	//Multiply the fill of the attack bar by 10 and deal that amount of damage: the more the bar is filled, the higher the damage.

			shootingEffect.gameObject.SetActive(true);			//Show the particle effect of shooting
			source.PlayOneShot (cannonSound);					//Play the sound of shooting the cannon.

			float hitChance = Random.value;								//The original design included an random accuracy logic. The accuracy value goes from 0 to 1. 
			if (hitChance <= attributes.accuracy) {						//If the randomly generated value is lower than the ship's accuracy, the shot would be a hit.
				engagedEnemy.attributes.health -= attributes.damage;	//This feature was later removed, so all the ships' accuracy level was set at 1. Hence, shots will always hit.
			}

			StartCoroutine (Reload ());							//After shooting, reload.
		}


	}

	public void SetAttackStance(bool stance){
		attackStance = stance;

		if (attackStance == true) {
			healthBar.SetColor (Color.green);
		} else {
			healthBar.SetColor (Color.blue);

		}
	}

	IEnumerator Reload(){
		canShoot = false;
		reloader.gameObject.SetActive (true);
		yield return new WaitForSeconds (attributes.reloadTime);
		canShoot = true;
		shootingEffect.SetActive (false);
		reloader.gameObject.SetActive (false);
	}

	public void check(){
		print ("check");
	}
		
}
