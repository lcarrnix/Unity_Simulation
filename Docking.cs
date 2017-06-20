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


	public ObstacleDetection od;

	// This will only be displayed when the player is in trigger (near the dock)
	public Button parkBtn;

	void Start(){
		player = GameObject.Find ("Player");
		rb = rb.GetComponent<Rigidbody> ();
		dockCube = GameObject.Find ("Dock Cube");
		speedSlider = speedSlider.GetComponent<Slider> ();
		clicked = false;
		stay = false;

		od = new ObstacleDetection ();

		// Autonomous driving stuff
		parkBtn = parkBtn.GetComponent<Button> ();
		parkBtn.enabled = false; //button is not interactable (for now)
		parkBtn.gameObject.SetActive(false); //makes button disappear
	}

	void Update(){
		// Player is in trigger area and the key 'P' has been pressed
		if (stay && Input.GetKey (KeyCode.P)) {
			//brute force works:
			player.transform.position = dockCube.transform.position;
		}
	}

	// Displays parkBtn
	void OnTriggerStay(Collider other){
		//Debug.Log ("WITHIN trigger...");
		parkBtn.enabled = true; // Button is interactable (for now)
		parkBtn.gameObject.SetActive(true); // Makes button appear
		stay = true;
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

		//brute force works:
		player.transform.position = dockCube.transform.position;
	}
}