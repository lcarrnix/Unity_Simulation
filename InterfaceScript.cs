using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//the script for all of the interface/display elements in the main scene (actual simulation environment)
public class InterfaceScript : MonoBehaviour {

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
	public Canvas warningMenu;
	public Canvas wifiMenu;

	//toggles
	public Toggle mapToggle;
	public Toggle wifiToggle;
	public Toggle warningToggle;

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
	public Toggle micToggle; 
	public Dropdown wifiOptions;
	public Button helpBtn;
	public Button closeSettings;

	//things on help menu
	public Button closeHelp;

	//testing hover over displays
	public Text zoomInfo;
	public Text theText;
	public string hoverDisplay;
	public float fadeTime;
	public bool displayInfo;
	public Canvas Information;
	//end testing for hover over displays- not currently working

	public Camera mapCam;
	public Camera disMapCam;
	public Text mapToggleDisabled;

	// Use this for initialization
	void Start () {

		player = GameObject.Find ("Player");
		screen = GameObject.Find ("screen");
		stand = GameObject.Find ("stand");

		mapCam = mapCam.GetComponent<Camera> ();
		disMapCam = disMapCam.GetComponent<Camera> ();
		mapToggleDisabled = mapToggleDisabled.GetComponent<Text> ();

		parkBtn = parkBtn.GetComponent<Button> ();
		parkBtn.enabled = false; //button is not interactable (for now)
		parkBtn.gameObject.SetActive(false); //makes button disappear

		//canvases/menus
		mainInterface = mainInterface.GetComponent<Canvas>();
		quitMenu = quitMenu.GetComponent<Canvas> ();
		settingsMenu = settingsMenu.GetComponent<Canvas> ();
		helpMenu = helpMenu.GetComponent<Canvas> ();
		warningMenu = warningMenu.GetComponent<Canvas> ();
		wifiMenu = wifiMenu.GetComponent<Canvas> ();

		mapToggle = mapToggle.GetComponent<Toggle> ();
		wifiToggle = wifiToggle.GetComponent<Toggle> ();
		warningToggle = warningToggle.GetComponent<Toggle> ();

		//main display components
		exitBtn = exitBtn.GetComponent<Button> ();
		zoomSlider = zoomSlider.GetComponent<Slider> ();
		volumeSlider = volumeSlider.GetComponent<Slider> ();
		heightSlider = heightSlider.GetComponent<Slider> ();
		speedSlider = speedSlider.GetComponent<Slider> ();
		settings = settings.GetComponent<Button> ();
		callButton = callButton.GetComponent<Button> ();


		//settings menu components
		//scroll = scroll.GetComponent<Scrollbar>(); //not sure if this is right
		micToggle = micToggle.GetComponent<Toggle>();
		wifiOptions = wifiOptions.GetComponent<Dropdown> ();
		helpBtn = helpBtn.GetComponent<Button> ();
		closeSettings = closeSettings.GetComponent<Button> ();

		//help menu components
		closeHelp = closeHelp.GetComponent<Button> ();


		/**
		 * commented out by amanda on 7/6
		//testing hover over
		theText = zoomInfo.GetComponent<Text>();
		zoomInfo = zoomInfo.GetComponent<Text>();
		//Information = Information.GetComponent<Canvas> ();
		zoomInfo.enabled = false; //displayed when zoom is hovered over
		zoomInfo.color = Color.clear; //invisible
		theText.color = Color.clear;

		*/

		//main display interface is on at start of simulation, other menus are hidden
		mainInterface.enabled = true; //interface always on top of view
		quitMenu.enabled = false; 
		settingsMenu.enabled = false;
		helpMenu.enabled = false;
		warningMenu.enabled = false;
		wifiMenu.enabled = false;

		mapToggle.enabled = true;
		wifiToggle.enabled = false;
		warningToggle.enabled = false;

		exitBtn.enabled = true;
		zoomSlider.enabled = true;
		volumeSlider.enabled = true;
		heightSlider.enabled = true;
		speedSlider.enabled = true;
		settings.enabled = true;
		callButton.enabled = true; 

		//scroll.enabled = false;
		micToggle.enabled = false;
		wifiOptions.enabled = false;
		helpBtn.enabled = false;
		closeSettings.enabled = false;
		closeHelp.enabled = false;
	}

	// Update is called once per frame
	void Update()
	{
		//FadeText (); //for testing hover over displays

		//display map camera
		if (mapToggle.isOn) {
			//Debug.Log("Map toggle on");
			mapCam.enabled = true;
			disMapCam.enabled = false;
			mapToggleDisabled.enabled = false;
		}
		//do not display map camera
		else {
			//Debug.Log("Map toggle off");
			mapCam.enabled = false;
			disMapCam.enabled = true;
			mapToggleDisabled.enabled = true;
		}
	}

	public void SettingsMenuDisplay()
	{
		//displaying the settings menu, hiding/disabling everything else
		mainInterface.enabled = false; //hiding other canvas
		settingsMenu.enabled = true;
		helpMenu.enabled = false;
		warningMenu.enabled = false;
		wifiMenu.enabled = false;

		//toggles
		mapToggle.enabled = false;
		wifiToggle.enabled = false;
		warningToggle.enabled = false;

		//setting menu components
		//scroll.enabled = true;
		micToggle.enabled = true;
		wifiOptions.enabled = true;
		helpBtn.enabled = true;
		closeSettings.enabled = true;

		//disabling buttons on main interface
		exitBtn.enabled = false;
		zoomSlider.enabled = false;
		volumeSlider.enabled = false;
		heightSlider.enabled = false; //added 7/5
		speedSlider.enabled = false; //added 7/5
		settings.enabled = false;
		callButton.enabled = false; 

		//pressing close on settings menu will send to NoPress()
	}

	public void HelpMenuDisplay()
	{
		settingsMenu.enabled = false; //hiding settings canvas
		helpMenu.enabled = true; //displaying the help menu, hiding/dissabling everything else

		//help menu component
		closeHelp.enabled = true;

		mapToggle.enabled = false;

		//disabling buttons on settings menu
		//scroll.enabled = false;
		micToggle.enabled = false;
		wifiOptions.enabled = false;
		helpBtn.enabled = false;
		closeSettings.enabled = false;

		//pressing close on help menu will send to SettingsMenuDisplay()
	}

	public void ExitPress()
	{
		quitMenu.enabled = true;
		exitBtn.enabled = false;
		zoomSlider.enabled = false;
		volumeSlider.enabled = false;
		heightSlider.enabled = false; //added 7/5
		speedSlider.enabled = false; //added 7/5
		settings.enabled = false;
		mapToggle.enabled = false;
		callButton.enabled = false; 

		//deactive height slider and speed slider

		//need to disable other buttons on interface
		//pressing yes will send to ExitGame()
		//pressing no will send to NoPress()
	}

	// Pressing no to continue from exit menu/pressing close on settings menu
	public void NoPress()
	{
		mainInterface.enabled = true;
		quitMenu.enabled = false;
		settingsMenu.enabled = false;
		exitBtn.enabled = true;
		zoomSlider.enabled = true;
		volumeSlider.enabled = true;
		heightSlider.enabled = true; //added 7/5
		speedSlider.enabled = true; //added 7/5
		settings.enabled = true;
		mapToggle.enabled = true;
		callButton.enabled = true; //added 6/29

		//enable all other buttons on interface
	}

	// When warning pop msg close btn is clicked
	public void onCloseWarning(){
		warningMenu.enabled = false;
		warningToggle.enabled = false;
	}

	// When wifi pop msg close btn is clicked
	public void onCloseWifi(){
		wifiMenu.enabled = false;
		wifiToggle.enabled = false;
	}

	public void changeHeightSlider(float value){
		// this works for ball, but not for telepresence system object
		//player.transform.position = new Vector3 (player.transform.position.x, value, player.transform.position.z);

		//player.transform.localScale = new Vector3 (player.transform.localScale.x, value, player.transform.localScale.z);
		stand.transform.localScale = new Vector3 (stand.transform.localScale.x, value, stand.transform.localScale.z);
		stand.transform.position = new Vector3 (stand.transform.position.x, value, stand.transform.position.z);

		screen.transform.position = new Vector3 (screen.transform.position.x, stand.transform.position.y + value , screen.transform.position.z);
	}

	//TESTING HOVER OVER
	/*void OnMouseOver()
	{
		displayInfo = true;	
	}

	void OnMouseExit()
	{
		displayInfo = false; 
	}

	void FadeText()
	{
		if (displayInfo == true) //hover over display will show
		{ 
			theText.text = hoverDisplay;
			theText.color = Color.Lerp (theText.color, Color.black, fadeTime * Time.deltaTime);
		} 
		else 
		{
			theText.color = Color.Lerp (theText.color, Color.clear, fadeTime * Time.deltaTime);
		}
	}*/
	//END TESTING HOVER OVER- currently not working*

	// Ending session- jumps to call screen
	public void ExitGame()
	{
		SceneManager.LoadScene ("CallScene");
	}

}