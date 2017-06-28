using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LowWifiTrigger : MonoBehaviour {

	public Image LowWifiImage;
	public Image MidWifiImage;
	public Image HighWifiImage;

	void Start(){
		//get images
		LowWifiImage = LowWifiImage.GetComponent<Image> ();
		LowWifiImage.enabled = false;
		MidWifiImage = MidWifiImage.GetComponent<Image> ();
		HighWifiImage = HighWifiImage.GetComponent<Image> ();
	}

	void OnTriggerStay (Collider other){
		LowWifiImage.enabled = true;
		MidWifiImage.enabled = false;
		HighWifiImage.enabled = false;
	}

	void OnTriggerExit (Collider other){
		//deactivate LowWifi image
		LowWifiImage.enabled = false;
	}

}
