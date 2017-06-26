using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CancelBtn : MonoBehaviour {
	//LoadingScene will be displayed for 3 seconds before switching to MiniGame

	//START of delay code...
	private bool connectClicked;

	void Start(){
		connectClicked = true;
	}

	void Update(){
		if (connectClicked) {
			ModeSelect ();
			connectClicked = false;
		}
	}

	void ModeSelect(){
		StartCoroutine ("Wait");
	}

	IEnumerator Wait(){
		yield return new WaitForSeconds (3);
		SceneManager.LoadScene ("MiniGame");
	}
	//END of delay code...

	// When user clicks cancel - jumps back to call scene
	public void Cancel()
	{
		SceneManager.LoadScene ("CallScene");
	}

}
