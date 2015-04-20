using UnityEngine;
using System.Collections.Generic;
using DesignPattern;

public class ThrowUI : DesignPattern.Singleton<Grid> {

	// Attributes

	// CONSTANT CHARACTER POSITION
	private const int userPosX = 12;
	private const int userPosY = 5;
	// CONSTANT RANGE
	private const int range = 3;

	private List<CaseUIBehaviour> gridUI = new List<CaseUIBehaviour>();
	private Vector3 lastMousePosition;
	private float timerNoMove;


	// Use this for initialization
	void Start () {
		// Conditions aux bords
		for (int i = userPosY - range; i <= userPosY + range; i++) 
		{
			int offsetY = userPosY - i;
			if(i >= 0 && i < Grid.Instance.Height)
			{
				for(int j = (userPosX - (range - Mathf.	Abs(offsetY))); j <= (userPosX + (range - Mathf.Abs(offsetY))); j++)
				{
					if(j >= 0 && j < Grid.Instance.Width)
					{
						CaseUIBehaviour currentCase;
						if(Grid.Instance.grid[i][j].IsObstacle)
						{
							currentCase = Factory<CaseUIBehaviour>.New("Case/CaseSelectedNotValid");
						}
						else
						{
							currentCase = Factory<CaseUIBehaviour>.New("Case/CaseSelectedValid");
						} 
						currentCase.posX = j;
						currentCase.posY = i;
						currentCase.transform.position = new Vector3(j, i, 0);
						gridUI.Add(currentCase);
					}
				}
			}
		}
		setVisible(false);
	}

	void setVisible(bool visible)
	{
		for(int i = 0; i < gridUI.Count; i++)
		{
			gridUI[i].GetComponent<SpriteRenderer>().enabled = visible;
		}
	}

	// Update is called once per frame
	void Update()
	{
		Debug.Log (Input.mousePosition);
		if (Input.mousePosition != lastMousePosition)
		{
			setVisible(true);
			timerNoMove = 0;
		}
		else
		{
			timerNoMove += Time.deltaTime;
			if(timerNoMove > 1)
			{
				setVisible(false);
			}
		}
		lastMousePosition = Input.mousePosition;
	 // 1 seconde de délai
	}
}
