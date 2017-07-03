using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoWifiTrigger : MonoBehaviour {

	public Image NoWifiImage;
	public Image LowWifiImage;
	public Image MidWifiImage;
	public Image HighWifiImage;

	public Canvas wifiMenu;
	public Toggle wifiToggle;
	private bool wifiDisplayed;

	void Start () {
		// Get images
		NoWifiImage = NoWifiImage.GetComponent<Image> ();
		NoWifiImage.enabled = false;

		LowWifiImage = LowWifiImage.GetComponent<Image> ();
		MidWifiImage = MidWifiImage.GetComponent<Image> ();
		HighWifiImage = HighWifiImage.GetComponent<Image> ();

		wifiMenu = wifiMenu.GetComponent<Canvas> ();
		wifiMenu.enabled = false;
		wifiToggle = wifiToggle.GetComponent<Toggle> ();
		wifiToggle.enabled = false;
		wifiDisplayed = false;
	}

	// Pop up wifi warning is displayed only if toggle is on AND it hasn't already been displayed
	void displayWifiWarning(){
		if (wifiToggle.isOn && wifiDisplayed == true) {
			wifiMenu.enabled = true;
			wifiToggle.enabled = true;
			wifiDisplayed = false;
		}

	}

	void OnTriggerEnter(Collider other){
		wifiDisplayed = true;
	}
	
	void OnTriggerStay (Collider other){
		// Activate No Wifi image and deactivate others
		NoWifiImage.enabled = true;

		LowWifiImage.enabled = false;
		MidWifiImage.enabled = false;
		HighWifiImage.enabled = false;

		displayWifiWarning ();
	}

	void OnTriggerExit (Collider other){
		//deactivate No Wifi image
		NoWifiImage.enabled = false;
		wifiMenu.enabled = false;
	}
}
