using UnityEngine;
using System.Collections;

public class ChangeMusic : MonoBehaviour {

	//public AudioClip level2music;

	private AudioSource source;

	void Awake () 
	{
		source = GetComponent<AudioSource> ();
	}

	void OnLevelWasLoaded (int level)
	{
		/*if (level == 2) 
		{
			source.clip = level2music;
			source.Play();
		}*/

		if (level == 0)
		{
			GameObject BackgroundMusic = GameObject.FindWithTag("BackgroundMusic");
			Destroy(BackgroundMusic);
		}
	}
}
