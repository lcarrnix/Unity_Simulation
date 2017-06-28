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
		MidWifiImage = MidWifiImage.GetComponent<Image> ();
		MidWifiImage.enabled = false;
		/*LowWifiImage = LowWifiImage.GetComponent<Image> ();
		HighWifiImage = HighWifiImage.GetComponent<Image> ();*/
	}

	void OnTriggerStay (Collider other){
		//activate Mid Wifi image and deactivate low and high images
		MidWifiImage.enabled = true;
		LowWifiImage.enabled = false;
		HighWifiImage.enabled = false;
	}

	void OnTriggerExit (Collider other){
		Debug.Log ("object exited trigger");
		//deactivate Mid Wifi image
		MidWifiImage.enabled = false;
	}
}
