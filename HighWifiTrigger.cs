using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighWifiTrigger : MonoBehaviour {

	public Image NoWifiImage;
	public Image LowWifiImage;
	public Image MidWifiImage;
	public Image HighWifiImage;

	void Start(){
		//get images
		HighWifiImage = HighWifiImage.GetComponent<Image> ();
		HighWifiImage.enabled = false;

		NoWifiImage = NoWifiImage.GetComponent<Image> ();
		LowWifiImage = LowWifiImage.GetComponent<Image> ();
		MidWifiImage = MidWifiImage.GetComponent<Image> ();

	}

	void OnTriggerStay (Collider other){
		// Activate high Wifi image and deactivate low and mid images
		HighWifiImage.enabled = true;

		NoWifiImage.enabled = false;
		LowWifiImage.enabled = false;
		MidWifiImage.enabled = false;
	}

	void OnTriggerExit (Collider other){
		//deactivate high Wifi image
		HighWifiImage.enabled = false;
	}
}
