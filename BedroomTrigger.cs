using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BedroomTrigger : MonoBehaviour {

	public Canvas authMenu;
	public GameObject authTrigger;  
	// Use this for initialization
	void Start () {

		authMenu = authMenu.GetComponent<Canvas> ();
		authMenu.enabled = false;
		authTrigger = GameObject.Find ("AuthTrigger"); 
	}
		
	void OnTriggerEnter (Collider other){
		authMenu.enabled = true;
		Debug.Log ("enter bedroom");
	}

	void OnTriggerStay (Collider other){
		// Activate warning when entering and staying in trigger zone
		authMenu.enabled = true;
		Debug.Log ("in bedroom");
	}

	void OnTriggerExit (Collider other){
		//deactivate warning when leaving trigger zone
		authMenu.enabled = false;
		Debug.Log ("leave bedroom");
	}

}