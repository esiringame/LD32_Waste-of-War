using UnityEngine;
using System.Collections;

public class DontDestroyOnLoad : MonoBehaviour {

	void Awake () {
		if (gameObject)
		//if (Application.loadedLevel == 0) 
			DontDestroyOnLoad (gameObject);
	}
}
