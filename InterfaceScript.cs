using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//the script for all of the interface/display elements in the main scene (actual simulation environment)
public class InterfaceScript : MonoBehaviour {

	public Button parkBtn;
	public GameObject player;

	//canvases/menus
	public Canvas mainInterface;
	public Canvas quitMenu; 
	public Canvas settingsMenu;
	public Canvas helpMenu;
	public Canvas warningMenu; // FIXME: it looks like I have to do the same for the toggle. Look into it.

	public Toggle warningToggle;
	public Toggle mapToggle;

	//things on main display
	public Slider zoomSlider;
	public Slider volumeSlider;
	public Slider heightSlider;
	public Button exitBtn;
	public Button settings;
	public Toggle mapView;

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

	// Use this for initialization
	void Start () {

		player = GameObject.Find ("Player");

		parkBtn = parkBtn.GetComponent<Button> ();//GameObject.FindObjectOfType<Button>();
		parkBtn.enabled = false; //button is not interactable (for now)
		parkBtn.gameObject.SetActive(false); //makes button disappear

		//canvases/menus
		mainInterface = mainInterface.GetComponent<Canvas>();
		quitMenu = quitMenu.GetComponent<Canvas> ();
		settingsMenu = settingsMenu.GetComponent<Canvas> ();
		helpMenu = helpMenu.GetComponent<Canvas> ();
		warningMenu = warningMenu.GetComponent<Canvas> ();

		warningToggle = warningToggle.GetComponent<Toggle> ();
		mapToggle = mapToggle.GetComponent<Toggle> ();

		//main display components
		exitBtn = exitBtn.GetComponent<Button> ();
		zoomSlider = zoomSlider.GetComponent<Slider> ();
		volumeSlider = volumeSlider.GetComponent<Slider> ();
		heightSlider = heightSlider.GetComponent<Slider> ();
		settings = settings.GetComponent<Button> ();
		mapView = mapView.GetComponent<Toggle> ();

		//settings menu components
		//scroll = scroll.GetComponent<Scrollbar>(); //not sure if this is right
		micToggle = micToggle.GetComponent<Toggle>();
		wifiOptions = wifiOptions.GetComponent<Dropdown> ();
		helpBtn = helpBtn.GetComponent<Button> ();
		closeSettings = closeSettings.GetComponent<Button> ();

		//help menu components
		closeHelp = closeHelp.GetComponent<Button> ();

		//testing hover over
		theText = zoomInfo.GetComponent<Text>();
		zoomInfo = zoomInfo.GetComponent<Text>();
		//Information = Information.GetComponent<Canvas> ();
		zoomInfo.enabled = false; //displayed when zoom is hovered over
		zoomInfo.color = Color.clear; //invisible
		theText.color = Color.clear;

		//main display interface is on at start of simulation, other menus are hidden
		mainInterface.enabled = true; //interface always on top of view
		quitMenu.enabled = false; 
		settingsMenu.enabled = false;
		helpMenu.enabled = false;
		warningMenu.enabled = false;

		warningToggle.enabled = false;
		mapToggle.enabled = true;

		exitBtn.enabled = true;
		zoomSlider.enabled = true;
		volumeSlider.enabled = true;
		settings.enabled = true;
		mapView.enabled = true;

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
		if (mapToggle.isOn) {
			//display map camera
			Debug.Log("Map toggle on");
			mapCam.camera. == true;

		}
		else {
			//do not display map camera
			Debug.Log("Map toggle off");
		}
	}

	public void SettingsMenuDisplay()
	{
		//displaying the settings menu, hiding/disabling everything else
		mainInterface.enabled = false; //hiding other canvas
		settingsMenu.enabled = true;
		helpMenu.enabled = false;
		warningMenu.enabled = false;

		warningToggle.enabled = false;
		mapToggle.enabled = false;

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
		settings.enabled = false;
		mapView.enabled = false;

		//pressing close on settings menu will send to NoPress()
	}

	public void HelpMenuDisplay()
	{
		settingsMenu.enabled = false; //hiding settings canvas
		helpMenu.enabled = true; //displaying the help menu, hiding/dissabling everything else

		//help menu component
		closeHelp.enabled = true;

		warningToggle.enabled = false;
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
		settings.enabled = false;
		mapView.enabled = false;

		//need to disable other buttons on interface
		//pressing yes will send to ExitGame()
		//pressing no will send to NoPress()
	}

	public void NoPress() //pressing no to continue from exit menu/pressing close on settings menu
	{
		mainInterface.enabled = true;
		quitMenu.enabled = false;
		settingsMenu.enabled = false;
		exitBtn.enabled = true;
		zoomSlider.enabled = true;
		volumeSlider.enabled = true;
		settings.enabled = true;
		mapView.enabled = true;

		//enable all other buttons on interface
	}

	public void onCloseWarning(){
		warningMenu.enabled = false;
		warningToggle.enabled = false;
	}

	//FIXME: small bug here. Height is only changed briefly until player moves.
	public void changeHeightSlider(float value){
		player.transform.position = new Vector3 (player.transform.position.x, value, player.transform.position.z);
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

	public void ExitGame() //ending session- jumps to exit screen
	{
		SceneManager.LoadScene ("EndSession");
	}


}