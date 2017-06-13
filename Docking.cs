using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Docking : MonoBehaviour {

	//FIXME: does not take into consideration this method
	/*void onTriggerEnter(Collider other){
		Debug.Log ("ENTERED the trigger...");
	}*/

	void OnTriggerStay(Collider other){
		Debug.Log ("WITHIN trigger...");
	}

	/*void OnTriggerExit(Collider other){
		Debug.Log ("EXITED the trigger...");
	}*/

}
