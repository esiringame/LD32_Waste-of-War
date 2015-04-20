using UnityEngine;
using System.Collections;
using System;

public class RankingManager : MonoBehaviour {

	public GameObject[] scorePanels;
	// Use this for initialization
	void Start () {
		ScoreManager.Instance.saveScore ();
		//ScoreManager.Instance.Add ("truc", TimeSpan.FromSeconds (2));
		ScoreManager.Instance.Add ("Toto", GameManager.Instance.Chronometer);
		ScoreManager.Instance.loadScore ();

		for (int i = 0; i < ScoreManager.Instance.Table.Count; i++) {
			scorePanels[i].GetComponent<ScoreEntry>().EditScore(ScoreManager.Instance.Table[i].name, ScoreManager.Instance.Table[i].score.ToString(), i);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
