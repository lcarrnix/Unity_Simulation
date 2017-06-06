using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginButton : MonoBehaviour {
	public Button loginBtn; 
	//public Button showPassword;

	public Toggle passwordToggle;

	public InputField username;
	public InputField password;

	void Start()
	{
		loginBtn = loginBtn.GetComponent<Button> ();
		//showPassword = showPassword.GetComponent<Button> ();
		username = username.GetComponent<InputField> ();
		password = password.GetComponent<InputField> ();

		loginBtn.enabled = false; //can't log in until both fields are filled
		password.enabled = true;
		username.enabled = true;

		passwordToggle.isOn = false;

		EnterUsername ();
	}

	public void StartSim() //when user clicks login- jumps to main scene
	{
		SceneManager.LoadScene ("MiniGame");
	}

	public void Update() //every frame
	{
		if(username.isFocused && (Input.GetKey(KeyCode.Tab) || Input.GetKey(KeyCode.Return)))
		{
			//jump to password field- shortcut 
			EnterPassword();
		}

		if (password.isFocused && username.text != "" && password.text != "" && Input.GetKey (KeyCode.Return)) 
		{
			PressEnter (); //not working- have to press enter twice for simulation to begin
		}

	}

	public void EnterUsername()
	{
		username.ActivateInputField (); //can click and type in this field, curser is flashing in field
		username.enabled = true;
	
		if(username.text != "") 
		{
			if (Input.GetKey (KeyCode.Tab) || Input.GetKey (KeyCode.Return)) 
			{
				username.enabled = false;
				EnterPassword (); //jumps to password input field
			}
		}

		if ((username.text != "") && (password.text != "")) 
		{
			PressLogin (); //if both fields are filled in, login button can be pressed
		}
		//***should have else statement to remind user to enter both username and password
	}

	public void EnterPassword()
	{
		password.enabled = true;
		username.DeactivateInputField (); //keeps cursor in the password field

		password.ActivateInputField ();
		if (password.text != "")
		{
			loginBtn.IsActive ();
			//showPassword.enabled = true;
		}

		if ((username.text != "" && password.text != "") && Input.GetKey (KeyCode.Return)) 
		{
			//highlights login button 
			PressEnter();
		}

		PressLogin (); //will check if both fields are filled in
	}

	public void togglePassword() //overview method for showPassword toggle
	{
		if (passwordToggle.isOn) 
		{
			ShowPassword (); 
		} 
		else 
		{
			HidePassword ();
		}
	}

	public void ShowPassword()
	{
		this.password.contentType = InputField.ContentType.Standard; //shows characters

		password.ForceLabelUpdate (); //from unity forums- makes this change shown at next frame
	}

	public void HidePassword()
	{
		this.password.contentType = InputField.ContentType.Password; //shows asterisks 

		password.ForceLabelUpdate (); //from unity forums- makes this change shown at next frame
	}

	public void PressEnter()
	{
		if (password.isFocused && username.text != "" && password.text != "" && Input.GetKey (KeyCode.Return)) 
		{
			StartSim (); //if both fields are filled in and user presses enter
		}
	}
	public void PressLogin()
	{
		if((username.text != "") && (password.text != ""))
		{
			loginBtn.enabled = true; //user able to log in now
		}
	}

}
