using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighWifiTrigger : MonoBehaviour {

	public Image LowWifiImage;
	public Image MidWifiImage;
	public Image HighWifiImage;

	void Start(){
		//get images
		HighWifiImage = HighWifiImage.GetComponent<Image> ();
		HighWifiImage.enabled = false;
		LowWifiImage = LowWifiImage.GetComponent<Image> ();
		MidWifiImage = MidWifiImage.GetComponent<Image> ();

	}

	void OnTriggerStay (Collider other){
		//activate high Wifi image and deactivate low and mid images
		HighWifiImage.enabled = true;
		LowWifiImage.enabled = false;
		MidWifiImage.enabled = false;
	}

	void OnTriggerExit (Collider other){
		//deactivate high Wifi image
		HighWifiImage.enabled = false;
	}
}
