using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoWifiTrigger : MonoBehaviour {

	//public Image NoWifiImage;
	public Image LowWifiImage;
	public Image MidWifiImage;
	public Image HighWifiImage;

	// Use this for initialization
	void Start () {
		//NoWifiImage = NoWifiImage.GetComponent<Image> ();
		//NoWifiImage.enabled = false;
	}
	
	void onTriggerStay(Collider other){
		//activate No Wifi image and deactivate others
		//NoWifiImage.enabled = true;
		LowWifiImage.enabled = false;
		MidWifiImage.enabled = false;
		HighWifiImage.enabled = false;
	}

	void OnTriggerExit (Collider other){
		Debug.Log ("Exited no wifi trigger");
		//deactivate No Wifi image
		//NoWifiImage.enabled = false;
	}
}
