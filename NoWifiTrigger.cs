using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoWifiTrigger : MonoBehaviour {

	public Image NoWifiImage;
	public Image LowWifiImage;
	public Image MidWifiImage;
	public Image HighWifiImage;

	void Start () {
		// Get images
		NoWifiImage = NoWifiImage.GetComponent<Image> ();
		NoWifiImage.enabled = false;

		LowWifiImage = LowWifiImage.GetComponent<Image> ();
		MidWifiImage = MidWifiImage.GetComponent<Image> ();
		HighWifiImage = HighWifiImage.GetComponent<Image> ();
	}
	
	void OnTriggerStay (Collider other){
		// Activate No Wifi image and deactivate others
		NoWifiImage.enabled = true;

		LowWifiImage.enabled = false;
		MidWifiImage.enabled = false;
		HighWifiImage.enabled = false;
	}

	void OnTriggerExit (Collider other){
		//deactivate No Wifi image
		NoWifiImage.enabled = false;
	}
}
