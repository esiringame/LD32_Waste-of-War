using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class InputFieldSetup : MonoBehaviour {
	private InputField inputF;
	private GameObject playerInfos;
	// Use this for initialization
	void Start () {
		Debug.Log ("Into IFS start");
		inputF = gameObject.GetComponent<InputField>();
		inputF.characterLimit = 25;
		inputF.onEndEdit.AddListener(SendPlayerName);
		playerInfos = GameObject.Find ("PlayerInfos");
	}
	
	// Update is called once per frame
	void Update () {
		//inputF.onEndEdit.AddListener(SendPlayerName);
	}

	void SendPlayerName(string name)
	{
		Debug.Log ("Into IFS sendplayername");

		playerInfos.SendMessage ("setName", inputF.text);
		playerInfos.SendMessage ("goToNextScene");

	}
}
