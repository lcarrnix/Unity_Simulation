using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConnectBtn : MonoBehaviour {
	// When user clicks connect - jumps to main scene
	public void StartSim()
	{
		//connectClicked = true;
		SceneManager.LoadScene ("LoadingScene");
	}

	// When user clicks exit - jumps to end scene
	public void ExitSimulation(){
		SceneManager.LoadScene ("EndSession");
	}
}
