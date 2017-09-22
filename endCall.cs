using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class endCall : MonoBehaviour {

	public Canvas mainInterface;
	public Canvas quitMenu; 
	public Button exitBtn;
	public Button settings;
	public Button redCallButton;
	public Button greenCallButton;
	/**
	public Button parkBtn;
	// origianl for when changing height slider
	public GameObject player;
	// new idea for height slider
	public GameObject screen;
	public GameObject stand;

	//canvases/menus
	public Canvas mainInterface;
	public Canvas quitMenu; 
	public Canvas settingsMenu;
	public Canvas helpMenu;
	public Canvas wifiMenu;
	public Canvas warningMenu;
	public Canvas AuthMenu; 

	//toggles
	public Toggle wifiToggle;
	public Toggle warningToggle;
	public Toggle AuthToggle; 

	//things on main display
	public Slider zoomSlider;
	public Slider volumeSlider;
	public Slider heightSlider;
	public Slider speedSlider;
	public Button exitBtn;
	public Button settings;
	public Button callButton;


	//things on settings menu
	//public Scrollbar scroll; //not sure if this is right?
	//public Toggle micToggle; 
	//public Dropdown wifiOptions;
	//public Button helpBtn;
	public Button closeSettings;

	//things on help menu
	public Button closeHelp;

	//testing hover over displays
	public Text zoomInfo;
	//public Text theText;
	public string hoverDisplay;
	public float fadeTime;
	public bool displayInfo;
	public Canvas Information;
	//end testing for hover over displays- not currently working

	**/
	// Use this for initialization
	void Start () {
		greenCallButton.enabled = true;
		redCallButton.enabled = false; 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void ExitPress()
	{
		quitMenu.enabled = true;
		exitBtn.enabled = false;
		//zoomSlider.enabled = false;
		//volumeSlider.enabled = false;
		//heightSlider.enabled = false; //added 7/5
		//speedSlider.enabled = false; //added 7/5
		//settings.enabled = false;
		greenCallButton.enabled = false; 
		redCallButton.enabled = true;

		//deactive height slider and speed slider

		//need to disable other buttons on interface
		//pressing yes will send to ExitGame()
		//pressing no will send to NoPress()
	}
}
