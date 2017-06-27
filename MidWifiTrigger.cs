using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MidWifiTrigger : MonoBehaviour {

	public Image LowWifiImage;
	public Image MidWifiImage;
	public Image HighWifiImage;

	void Start(){
		//get images
		//	LowWifiImage = LowWifiImage.GetComponent<Image> ();
		MidWifiImage = MidWifiImage.GetComponent<Image> ();
		//	HighWifiImage = HighWifiImage.GetComponent<Image> ();
	}

	void OnTriggerEnter (Collider other){
		Debug.Log("Object entered trigger");
		//activate Mid Wifi image and deactivate low and high images
		LowWifiImage.enabled = false;
		MidWifiImage.enabled = true;
		HighWifiImage.enabled = false;
	}

	void OnTriggerStay (Collider other){
		Debug.Log ("Object in trigger");
		//nothing
	}

	void OnTriggerExit (Collider other){
		Debug.Log ("object exited trigger");
		//deactivate Mid Wifi image
		MidWifiImage.enabled = false;
	}
}
