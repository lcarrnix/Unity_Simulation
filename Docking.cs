using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Docking : MonoBehaviour {

	public GameObject player;
	public GameObject dockCube;
	public Slider speedSlider;

	// This will only be displayed when the player is in trigger (near the dock)
	public Button parkBtn;

	void Start(){
		player = GameObject.Find ("Player");
		dockCube = GameObject.Find ("Dock Cube");
		speedSlider = speedSlider.GetComponent<Slider> ();

		// Autonomous driving stuff
		parkBtn = parkBtn.GetComponent<Button> ();
		parkBtn.enabled = false; //button is not interactable (for now)
		parkBtn.gameObject.SetActive(false); //makes button disappear
	}

	// Displays parkBtn
	void OnTriggerStay(Collider other){
		//Debug.Log ("WITHIN trigger...");
		parkBtn.enabled = true; // Button is interactable (for now)
		parkBtn.gameObject.SetActive(true); // Makes button appear
	}

	// Hides parkBtn
	void OnTriggerExit(Collider other){
		//Debug.Log ("EXITED the trigger...");
		parkBtn.enabled = false;
		parkBtn.gameObject.SetActive(false);
	}

	public void onBtnClick(){
		//set up auto-docking code here
		Debug.Log ("PARKING BTN CLICKED");
		//Debug.Log ("Dock positon: " + dockCube.transform.position);
		//Debug.Log ("Player positon: " + player.transform.position);

		//brute force works:
		player.transform.position = dockCube.transform.position;
	}
}
