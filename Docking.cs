using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Docking : MonoBehaviour {

	/*
	THIS CODE ISN'T REALLY WORKING... When auto park presed or clicked, player can no longer move plus there are some weird bugs with it.
	*/

	public GameObject player;
	public Rigidbody rb;
	public GameObject dockCube;
	public Slider speedSlider;
	private bool inTriggerArea; // If player is in trigger area
	private bool selectedAuto; // If player clicked parkBtn or pressed 'p'
	private Vector3 destination;

	// This will only be displayed when the player is in trigger (near the dock)
	public Button parkBtn;

	void autoPark(){
		if(selectedAuto == true && inTriggerArea == true){
			player.transform.position = destination;
			selectedAuto = false;
			Debug.Log ("in autopark function");
		}
	}

	void Start(){
		player = GameObject.Find ("Player");
		rb = rb.GetComponent<Rigidbody> ();
		dockCube = GameObject.Find ("Dock Cube");
		speedSlider = speedSlider.GetComponent<Slider> ();
		inTriggerArea = false;
		selectedAuto = false;
		//destination = new Vector3 (dockCube.transform.position.x, player.transform.position.y, dockCube.transform.position.z);
		destination = new Vector3 (dockCube.transform.position.x, dockCube.transform.position.y, dockCube.transform.position.z);
		Debug.Log ("Destination: " + destination);
		Debug.Log ("Dock: " + dockCube.transform.position.x + " " + dockCube.transform.position.y + " " + dockCube.transform.position.z);

		// Autonomous driving stuff
		parkBtn = parkBtn.GetComponent<Button> ();
		parkBtn.enabled = false; //button is not interactable (for now)
		parkBtn.gameObject.SetActive(false); //makes button disappear
	}

	void Update(){
		if(inTriggerArea == true && Input.GetKey(KeyCode.P)){
			selectedAuto = true;
		}
		autoPark ();
	}

	// "It's the recommended place to apply forces and change Rigidbody settings"
	// Figure out working code for autonomous feature, fam
	//void FixedUpdate()

	// Displays parkBtn
	void OnTriggerStay(Collider other){
		//Debug.Log ("WITHIN trigger...");
		parkBtn.enabled = true; // Button is interactable (for now)
		parkBtn.gameObject.SetActive(true); // Makes button appear

		inTriggerArea = true;
	}

	// Hides parkBtn
	void OnTriggerExit(Collider other){
		//Debug.Log ("EXITED the trigger...");
		parkBtn.enabled = false;
		parkBtn.gameObject.SetActive(false);

		inTriggerArea = false;
		selectedAuto = false;
	}

	public void onBtnClick(){
		//set up auto-docking code here
		Debug.Log ("PARKING BTN CLICKED");
		selectedAuto = true;
	}
}