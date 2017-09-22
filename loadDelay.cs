using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadDelay : MonoBehaviour {

	private bool delayLoading; //for delay to show loading

	void Start () {
		delayLoading = true;
	}
		
	void Update () {
		if (delayLoading) {
			ModeSelect ();
			delayLoading = false;
		}
	}

	void ModeSelect(){
		StartCoroutine ("Wait");
	}

	IEnumerator Wait(){
		yield return new WaitForSeconds (3);
		SceneManager.LoadScene ("UserView");
	}
	//END of delay code...
}
