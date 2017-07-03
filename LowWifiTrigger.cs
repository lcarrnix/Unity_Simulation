using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LowWifiTrigger : MonoBehaviour {

	public Image NoWifiImage;
	public Image LowWifiImage;
	public Image MidWifiImage;
	public Image HighWifiImage;

	void Start(){
		// Get images
		LowWifiImage = LowWifiImage.GetComponent<Image> ();
		LowWifiImage.enabled = false;

		NoWifiImage = NoWifiImage.GetComponent<Image> ();
		MidWifiImage = MidWifiImage.GetComponent<Image> ();
		HighWifiImage = HighWifiImage.GetComponent<Image> ();
	}

	void OnTriggerStay (Collider other){
		// Activate Low Wifi image and deactivate others
		LowWifiImage.enabled = true;

		NoWifiImage.enabled = false;
		MidWifiImage.enabled = false;
		HighWifiImage.enabled = false;
	}

	void OnTriggerExit (Collider other){
		//deactivate LowWifi image
		LowWifiImage.enabled = false;
	}

}
