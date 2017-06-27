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
		LowWifiImage = LowWifiImage.GetComponent<Image> ();
		MidWifiImage = MidWifiImage.GetComponent<Image> ();
		HighWifiImage = HighWifiImage.GetComponent<Image> ();
	}

	void OnTriggerEnter (Collider other){
		Debug.Log("Object entered trigger");
		//activate high Wifi image and deactivate low and mid images
		HighWifiImage.enabled = true;
		LowWifiImage.enabled = false;
		MidWifiImage.enabled = false;
	}

	void OnTriggerStay (Collider other){
		Debug.Log ("Object in trigger");
		//nothing
	}

	void OnTriggerExit (Collider other){
		Debug.Log ("object exited trigger");
		//deactivate high Wifi image
		HighWifiImage.enabled = false;
	}
}
