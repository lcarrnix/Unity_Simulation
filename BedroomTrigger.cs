using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BedroomTrigger : MonoBehaviour {

	public Canvas AuthMenu;
	public Toggle AuthToggle;
	private bool AuthDisplayed;

	void Start () {
		/**
		AuthMenu = AuthMenu.GetComponent<Canvas> ();
		AuthMenu.enabled = false;
		AuthToggle = AuthToggle.GetComponent<Toggle> ();
		AuthToggle.enabled = false; 
		*/
		AuthMenu = AuthMenu.GetComponent<Canvas> ();
		AuthMenu.enabled = false; 
		AuthToggle = AuthToggle.GetComponent<Toggle> ();
		AuthToggle.enabled = false;
		AuthDisplayed = false; 
	}

	void OnTriggerEnter (Collider other){
		Debug.Log ("enter bedroom");
		displayAuth(); 
	}

	/**
	void OnTriggerStay (Collider other){
		// Activate warning when entering and staying in trigger zone
		//AuthMenu.enabled = true;
		Debug.Log ("in bedroom");
	}
	**/

	void OnTriggerExit (Collider other){
		//deactivate warning when leaving trigger zone
		AuthMenu.enabled = false;
		AuthToggle.enabled = false;
		Debug.Log ("leave bedroom");
	}


	void displayAuth(){
		if (AuthToggle.isOn == true) {
			AuthMenu.enabled = true;
			AuthToggle.enabled = true;
			AuthDisplayed = true;
			Debug.Log ("in displayAuth");
		}
	}


}