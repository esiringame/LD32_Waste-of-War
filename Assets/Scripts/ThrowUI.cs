using UnityEngine;
using System.Collections.Generic;
using DesignPattern;

public class ThrowUI : DesignPattern.Singleton<ThrowUI>
{

	// Attributes
	public GameObject player;
	// CONSTANT THROW RANGE
	private const int range = 3;

	private List<CaseUIBehaviour> gridUI = new List<CaseUIBehaviour>();
	private Vector3 lastMousePosition;
	private bool start = true; // Start conditions
	private float timerNoMove;


	// Use this for initialization
	void Update () {

		int userPosY = (int)player.GetComponent<PlayerController>().PositionCase.y;
		int userPosX = (int)player.GetComponent<PlayerController>().PositionCase.x; 
		int lastUserPosY = -1;
		int lastUserPosX = -1;

		if (player.GetComponent<PlayerController> ().IsMoving)
		{
			cleanGridUI ();
		}
		if (Input.mousePosition != lastMousePosition && !start)
		{
			cleanGridUI();
			fillGridUI(userPosX, userPosY);
			setVisible(true);
			timerNoMove = 0;
			lastUserPosX = userPosX;
			lastUserPosY = userPosY;
		}
		else
		{
			timerNoMove += Time.deltaTime;
			if(timerNoMove > 1 && !isMouseInGrid())
			{
				setVisible(false);
			}
			// Disabling start conditions
			start = false;
		}
		checkClickInGridUI(userPosX, userPosY);
		lastMousePosition = Input.mousePosition;
	}

	/*
	 * 	FUNCTION fillGridUI
	 */
	void fillCase(int i, int j)
	{
		CaseUIBehaviour currentCase;
		if(Grid.Instance.grid[i][j] == null || Grid.Instance.grid[i][j].IsObstacle)
		{
			currentCase = Factory<CaseUIBehaviour>.New("Case/CaseHighlightedNotValid");
		}
		else
		{
			if(j == (int) Camera.main.ScreenToWorldPoint(Input.mousePosition).x && i == (int) Camera.main.ScreenToWorldPoint(Input.mousePosition).y)
			{
				currentCase = Factory<CaseUIBehaviour>.New("Case/CaseSelected");
			}
			else
			{
				currentCase = Factory<CaseUIBehaviour>.New("Case/CaseHighlightedValid");
			}
		} 
		currentCase.posX = j;
		currentCase.posY = i;
		currentCase.transform.position = new Vector3((float)(j + 0.5), (float)(i + 0.5), 0);
		gridUI.Add(currentCase);
	}

	/*
	 * 	FUNCTION cleanGridUI
	 */	
	void cleanGridUI()
	{
		foreach (CaseUIBehaviour caseUI in gridUI)
		{
			DestroyImmediate(caseUI.gameObject);
		}
		gridUI.Clear();
	}

	/*
	 * 	FUNCTION setVisible
	 */
	void setVisible(bool visible)
	{
		for(int i = 0; i < gridUI.Count; i++)
		{
			gridUI[i].GetComponent<SpriteRenderer>().enabled = visible;
		}
	}

	/*
	 * 	FUNCTION isMouseInGrid
	 */
	bool isMouseInGrid()
	{
		bool state = false;
		for (int i = 0; i < gridUI.Count; i++)
		{
			if(gridUI[i].posX == (int) Camera.main.ScreenToWorldPoint(Input.mousePosition).x && gridUI[i].posY == (int) Camera.main.ScreenToWorldPoint(Input.mousePosition).y)
			{
				state = true;
			}
		}
		return state;
	}

	void fillGridUI(int userPosX, int userPosY)
	{
		for (int i = userPosY - range; i <= userPosY + range; i++) 
		{
			int offsetY = userPosY - i;
			if(i >= 0 && i < Grid.Instance.Height)
			{
				for(int j = (userPosX - (range - Mathf.	Abs(offsetY))); j <= (userPosX + (range - Mathf.Abs(offsetY))); j++)
				{
					if(j >= 0 && j < Grid.Instance.Width)
					{
						fillCase(i, j);
					}
				}
			}
		}
	}

	void checkClickInGridUI(int userPosX, int userPosY)
	{
		for (int i = userPosY - range; i <= userPosY + range; i++) 
		{
			int offsetY = userPosY - i;
			if(i >= 0 && i < Grid.Instance.Height)
			{
				for(int j = (userPosX - (range - Mathf.	Abs(offsetY))); j <= (userPosX + (range - Mathf.Abs(offsetY))); j++)
				{
					if(j >= 0 && j < Grid.Instance.Width)
					{
						if(Input.GetMouseButtonDown(0) && ((int) Camera.main.ScreenToWorldPoint(Input.mousePosition).x == j && (int) Camera.main.ScreenToWorldPoint(Input.mousePosition).y == i))
						selected (i, j);
					}
				}
			}
		}
	}
	
	void selected(int i, int j)
	{
		int userPosY = (int) player.GetComponent<PlayerController>().PositionCase.y;
		int userPosX = (int) player.GetComponent<PlayerController>().PositionCase.x;
		if (userPosY == i && userPosX == j)
		{
			player.GetComponent<PlayerController>().PutStone();
		}
		else
		{
			player.GetComponent<PlayerController>().ThrowStone(j, i);
		}
		cleanGridUI();
	}
}
