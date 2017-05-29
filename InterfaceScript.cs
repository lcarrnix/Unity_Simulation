using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class InterfaceScript : MonoBehaviour {
	public Canvas quitMenu; 
	public Button exitBtn;

	// Use this for initialization
	void Start () {
		quitMenu = quitMenu.GetComponent<Canvas> ();
		exitBtn = exitBtn.GetComponent<Button> ();
		quitMenu.enabled = false; 
	}

	public void ExitPress()
	{
		quitMenu.enabled = true;
		exitBtn.enabled = false;
		//need to disable other buttons on interface

	}

	public void NoPress() //pressing no to continue
	{
		quitMenu.enabled = false;
		exitBtn.enabled = true;
		//enable all other buttons on interface
	}

	public void ExitGame() //ending session- jumps to exit screen
	{
		SceneManager.LoadScene ("EndSession");
	}

	// Update is called once per frame
	void Update () {
		
	}
}
