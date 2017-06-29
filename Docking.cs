using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Docking : MonoBehaviour {

	public GameObject player;
	public Rigidbody rb;
	public GameObject dockCube;
	public Slider speedSlider;
	private bool clicked; // If parkBtn has been clicked
	private bool stay; // If player is in trigger area


	public Image LowWifiImage;
	public Image MidWifiImage;
	public Image HighWifiImage;

	// This will only be displayed when the player is in trigger (near the dock)
	public Button parkBtn;

	void Start(){
		player = GameObject.Find ("Player");
		rb = rb.GetComponent<Rigidbody> ();
		dockCube = GameObject.Find ("Dock Cube");
		speedSlider = speedSlider.GetComponent<Slider> ();
		clicked = false;
		stay = false;

		// Autonomous driving stuff
		parkBtn = parkBtn.GetComponent<Button> ();
		parkBtn.enabled = false; //button is not interactable (for now)
		parkBtn.gameObject.SetActive(false); //makes button disappear

		//wifi images enabled
		/**
		HighWifiImage = HighWifiImage.GetComponent<Image> ();
		HighWifiImage.enabled = true;
		LowWifiImage = LowWifiImage.GetComponent<Image> ();
		MidWifiImage = MidWifiImage.GetComponent<Image> ();
		*/
	}

	// "It's the recommended place to apply forces and change Rigidbody settings"
	// Figure out working code for autonomous feature, fam
	void FixedUpdate(){
		// Player is in trigger area and the key 'P' has been pressed
		if (stay && Input.GetKey (KeyCode.P)) {
			//brute force works:
			player.transform.position = dockCube.transform.position;
		}
		// This does not work...
		// It basically just teleports player to dock (a lot like transform.position), but also breaks rigidbody code/obstacle detection code...
		/*if(stay && clicked){
			// Ray dir = new Ray (player.transform.position, dockCube.transform.position);
			// rb.AddForce (dir.direction * speedSlider.value);
		}*/
	}

	// Displays parkBtn
	void OnTriggerStay(Collider other){
		//Debug.Log ("WITHIN trigger...");
		parkBtn.enabled = true; // Button is interactable (for now)
		parkBtn.gameObject.SetActive(true); // Makes button appear
		stay = true;

		//wifi image enabled
		HighWifiImage.enabled = true;
	}

	// Hides parkBtn
	void OnTriggerExit(Collider other){
		//Debug.Log ("EXITED the trigger...");
		parkBtn.enabled = false;
		parkBtn.gameObject.SetActive(false);

		clicked = false;
		stay = false;
	}

	public void onBtnClick(){
		//set up auto-docking code here
		Debug.Log ("PARKING BTN CLICKED");
		clicked = true;
		//Debug.Log ("Dock positon: " + dockCube.transform.position);
		//Debug.Log ("Player positon: " + player.transform.position);

		// This should probably be in FixedUpdate()
		// Brute force works:
		player.transform.position = dockCube.transform.position;
	}
}