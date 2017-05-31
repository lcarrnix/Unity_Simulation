using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginButton : MonoBehaviour {
	public Button loginBtn; 
	public Button showPassword;

	public Toggle passwordToggle;

	public InputField username;
	public InputField password;

	void Start()
	{
		loginBtn = loginBtn.GetComponent<Button> ();
		showPassword = showPassword.GetComponent<Button> ();
		username = username.GetComponent<InputField> ();
		password = password.GetComponent<InputField> ();

		loginBtn.enabled = false; //can't log in until both fields are filled
		showPassword.enabled = false; //will be enabled once password field is used
		password.enabled = true;
		username.enabled = true;

		passwordToggle.isOn = false;

		EnterUsername ();
	}

	public void StartSim() //when user clicks login
	{
		SceneManager.LoadScene ("MiniGame");
	}

	public void Update() //every frame
	{
		if(username.isFocused && (Input.GetKey(KeyCode.Tab) || Input.GetKey(KeyCode.Return)))
		{
			//jump to password field
			EnterPassword();
		}

		if (password.isFocused && username.text != "" && password.text != "" && Input.GetKey (KeyCode.Return)) 
		{
			PressEnter ();
		}

	}

	public void EnterUsername()
	{
		username.ActivateInputField (); //can click and type in this field, curser is flashing in field
		username.enabled = true;
		//password.ActivateInputField ();
	
		if(username.text != "") 
		{
			//password.ActivateInputField ();
			if (Input.GetKey (KeyCode.Tab) || Input.GetKey (KeyCode.Return)) 
			{
				Debug.Log ("pressed");
				username.enabled = false;
				EnterPassword ();
			}
		}

		if ((username.text != "") && (password.text != "")) 
		{
			PressLogin ();
		}
	}

	public void EnterPassword()
	{
		Debug.Log ("HELLO");
		//username.enabled = false;
		password.enabled = true;
		username.DeactivateInputField ();
		showPassword.enabled = true;

		password.ActivateInputField ();
		if (password.text != "")
		{
			Debug.Log ("in here");
			loginBtn.IsActive ();
			showPassword.enabled = true;
		}

		if ((username.text != "" && password.text != "") && Input.GetKey (KeyCode.Return)) 
		{
			//highlights login button 
			PressEnter();
		}

		PressLogin ();
	}

	public void togglePassword()
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

	public void ShowPasswordBtn()
	{
		if (password.contentType == InputField.ContentType.Password) 
		{
			ShowPassword ();
		} 
		else if (password.contentType == InputField.ContentType.Standard) 
		{
			HidePassword ();
		}
	}

	public void ShowPassword()
	{
		Debug.Log ("showing password");

		password.contentType = InputField.ContentType.Standard;


		password.ForceLabelUpdate ();
	}

	public void HidePassword()
	{
		Debug.Log ("hiding password");
		this.password.contentType = InputField.ContentType.Password;

		password.ForceLabelUpdate ();
	}

	public void PressEnter()
	{
		if (password.isFocused && username.text != "" && password.text != "" && Input.GetKey (KeyCode.Return)) 
		{
			StartSim ();
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
