using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreEntry : MonoBehaviour {

	public GameObject name;
	public GameObject score;
	public GameObject rank;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void EditScore(string name, string score, int rank)
	{
		this.name.GetComponent<Text>().text = name;
		this.score.GetComponent<Text>().text = score;
		this.rank.GetComponent<Text>().text = rank.ToString();
	}
}
