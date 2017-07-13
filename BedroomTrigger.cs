using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BedroomTrigger : MonoBehaviour {

	public Canvas AuthMenu;
	public Toggle AuthToggle;
	//public GameObject AuthTrigger;  
	// Use this for initialization
	void Start () {

		AuthMenu = AuthMenu.GetComponent<Canvas> ();
		AuthMenu.enabled = false;

		AuthToggle = AuthToggle.GetComponent<Toggle> ();
		AuthToggle.enabled = false; 

		//AuthTrigger = GameObject.Find ("AuthTrigger"); 
	}
		
	void OnTriggerEnter (Collider other){
		AuthMenu.enabled = true;
		AuthToggle.enabled = true;
		Debug.Log ("enter bedroom");
	}

	void OnTriggerStay (Collider other){
		// Activate warning when entering and staying in trigger zone
		//AuthMenu.enabled = true;
		Debug.Log ("in bedroom");
	}

	void OnTriggerExit (Collider other){
		//deactivate warning when leaving trigger zone
		AuthMenu.enabled = false;
		AuthToggle.enabled = false;
		Debug.Log ("leave bedroom");
	}

}