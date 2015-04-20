using System;
using System.Collections.Generic;
using DesignPattern;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager> {

	public List<Score> Table{ get; set; }

	protected ScoreManager(){
		Table = new List<Score> ();
	}

	void Start () {
	
	}

	public void Add(string n, TimeSpan s){
		Table.Add (new Score { name = n, score = s });
		Table.Sort ();
	}

	public void saveScore(){
		PlayerPrefs.Save ();
		int nbScore = Table.Count;
		print (nbScore);
		for (int i = 0; i < nbScore; i++) {
			PlayerPrefs.SetString("Name"+i, Table[i].name.ToString());
			PlayerPrefs.SetInt("Score"+i, (int)Table[i].score.Ticks); 
		}
	}

	public void loadScore(){
		int nbScore = Table.Count;
		print (nbScore);
		for (int i = 0; i < nbScore; i++) {
			Table[i].name = PlayerPrefs.GetString("Name"+i);
			Table[i].score = TimeSpan.FromTicks(PlayerPrefs.GetInt("Score"+i)); 
		}
	}
}

public class Score : IEquatable<Score> , IComparable<Score>{
	public string name{ get; set; }
	public TimeSpan score{ get; set; }

	public int CompareTo(Score compareScore)
	{
		return score.CompareTo (compareScore.score);
	}
	
	public bool Equals(Score other)
	{
		if (other == null) return false;
		return ( this.name.Equals(other.name) && this.score.Equals(other.score) );
	}
	
	public override bool Equals(object obj)
	{
		if (obj == null) return false;
		Score objAsPart = obj as Score;
		if (objAsPart == null) return false;
		else return Equals(objAsPart);
	}
	
	public static bool operator ==(Score x, Score y){
		return x.Equals(y);
	}
	
	public static bool operator !=(Score x, Score y){
		return !x.Equals(y);
	}


}