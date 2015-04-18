using UnityEngine;
using System.Collections;

public class InfoCharacter : MonoBehaviour {

	private short nbSomaliens;
	private short rocks;
	private bool so_saw_saut_sow_sceau_seau_sot;

	public CaseBehaviour currentCase{ get; private set; }
	public AudioClip dead, trash_dead, water;

	// Use this for initialization
	void Start () {
		nbSomaliens = 5;
		rocks = 0;
		so_saw_saut_sow_sceau_seau_sot = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	bool isOver(){
		return nbSomaliens==0;
	}

	short nbVies(){
		return nbSomaliens;
	}
	

	bool isInventoryFull(){
		return rocks == 3;
	}

	bool isInventoryEmpty(){
		return rocks == 0;
	}

	void fillBucket(){
		so_saw_saut_sow_sceau_seau_sot = true;
		GetComponent<AudioSource>().PlayOneShot(water, 1.0F);
	}

	void emptyBucket(){
		so_saw_saut_sow_sceau_seau_sot = false;
	}

	void die(){
		so_saw_saut_sow_sceau_seau_sot = false;
		--nbSomaliens;
		rocks = 0;
		if(Random.value> 0.5)
			GetComponent<AudioSource>().PlayOneShot(dead, 1.0F);
		else
			GetComponent<AudioSource>().PlayOneShot(trash_dead, 1.0F);
	}

	void addRockToInventory(){
		if(!isInventoryFull())
			++rocks;
	}

	void removeRockFromInventory(){
		if(!isInventoryEmpty())
			--rocks;
	}
}
