using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GameManagerBis : MonoBehaviour
{
	public Text CounterTimeText;
	public Button PauseButton;

	private float CounterTime;
	private bool isLauchned = true;
	private bool isPaused = false;
	//private Text PauseButtonText = PauseButton.ToString();

	void Start () {
		CounterTime = 0.0f;
	}

	void Update () {
		if(!isPaused)
			CounterTime += Time.unscaledDeltaTime;
		TimeSpan timeSpan = TimeSpan.FromSeconds (CounterTime);
		CounterTimeText.text = string.Format ("{0} m {1}",
		                                      timeSpan.Minutes,
		                                      timeSpan.Seconds);
	}

	void StartChrono () 
	{
		if(isLauchned)
			CounterTime = 0.0f;
	}

	public void PauseChrono() 
	{
		/*if (PauseButtonText == "pause")
			Debug.Log ("true !");
		if (isPaused) {
			isPaused = false;
		} else {
			isPaused = true;
		}*/
	}

	void StopChrono()
	{
		isPaused = true;
	}
}