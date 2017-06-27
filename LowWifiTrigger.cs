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
		MidWifiImage = MidWifiImage.GetComponent<Image> ();
		HighWifiImage = HighWifiImage.GetComponent<Image> ();
	}

	void OnTriggerEnter (Collider other){
		Debug.Log("Object entered trigger");
		//activate LowWifi image
		LowWifiImage.enabled = true;
		MidWifiImage.enabled = false;
		HighWifiImage.enabled = false;
	}

	void OnTriggerStay (Collider other){
		Debug.Log ("Object in trigger");
		//nothing
	}

	void OnTriggerExit (Collider other){
		Debug.Log ("object exited trigger");
		//deactivate LowWifi image
		LowWifiImage.enabled = false;
	}

}
