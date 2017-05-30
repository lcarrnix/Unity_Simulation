using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EndSceneScript : MonoBehaviour {
	public Button exitBtn; 

	// Use this for initialization
	void Start () {
		exitBtn = exitBtn.GetComponent<Button> ();
		exitBtn.enabled = true; //user is able to exit immediately
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ExitGame() //ending session- jumps to exit screen
	{
		Application.Quit ();
	}

}
