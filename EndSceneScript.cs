using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EndSceneScript : MonoBehaviour {
	//script for exit screen of simulation
	//sets exit button to closing the application window
	
	public Button exitBtn; 

	// Use this for initialization
	void Start () {
		exitBtn = exitBtn.GetComponent<Button> ();
		exitBtn.enabled = true; //user is able to exit immediately
	}
	
	// Update is called once per frame
	void Update () {
		//do nothing
	}

	public void ExitGame() //ending session- closing application
	{
		Application.Quit ();
	}

}
