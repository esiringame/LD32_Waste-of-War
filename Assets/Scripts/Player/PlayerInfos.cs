using UnityEngine;
using System.Collections;
using System;

public class PlayerInfos : MonoBehaviour {

	public string PlayerName { get; private set; }

	// Use this for initialization
	void Start () {
		PlayerName = "";


		if (gameObject)
			DontDestroyOnLoad (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("name" + playerName);
	}

	void setInfos(string name, TimeSpan score){
		PlayerName = name;
	}

	void setName (string name){
		PlayerName = name;
		Debug.Log ("Into PI setname");
		Debug.Log (PlayerName);


	}


	void goToNextScene(){
		Application.LoadLevel ("HighScore");
	}

}

