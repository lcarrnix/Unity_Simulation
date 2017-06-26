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
		SceneManager.LoadScene ("MiniGame");
	}

}
