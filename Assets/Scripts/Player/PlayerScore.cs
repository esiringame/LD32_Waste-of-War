using UnityEngine;
using System.Collections;
using System;

public class PlayerScore : DesignPattern.Singleton<PlayerScore> {
	
	public TimeSpan score { get; private set; }
	
	// Use this for initialization
	void Start () {
				
		if (gameObject)
			DontDestroyOnLoad (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public TimeSpan getChrono()
	{
		return this.score;
	}
	void setScore (TimeSpan score){
		this.score = score;
	}
}
