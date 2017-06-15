using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Docking : MonoBehaviour {

	//FIXME: This method does not work. Dunno why. Probs don't need it in the future anyway.
	/*void onTriggerEnter(Collider other){
		Debug.Log ("ENTERED the trigger...");
	}*/

	// This will only be displayed when the player is in trigger (near the dock)
	public Button parkBtn;

	void Start(){
		// Autonomous driving stuff
		parkBtn = GameObject.FindObjectOfType<Button>(); //GetComponent<Button> ();
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
		parkBtn.enabled = false; // Button is not interactable (for now)
		parkBtn.gameObject.SetActive(false); // Makes button disappear
	}

	public void onBtnClick(){
		//set up auto-docking code here
		Debug.Log ("PARKING BTN CLICKED");
	}

}
