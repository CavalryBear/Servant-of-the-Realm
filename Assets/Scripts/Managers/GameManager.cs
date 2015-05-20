using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public float OutlineTileSize;
	public RoomOutline OutlinePrefab;
	public RoomOutline Outline;
	public bool CreatingOutline;

	//public GameStates GameState;
	public GameObject GameState;

	public GameObject OutlineHolder;

	private Vector3 _oldMousePosition;
	
	void Start ()
	{
		//GameState = GameStates.DefaultlState;
		//EnterBuildState();
	}

	void Update ()
	{
		processInput();
	}

	private void processInput()
	{
//		switch (GameState)
//		{
//		case GameStates.DefaultlState:
//			break;
//		case GameStates.BuildState:
//			processBuildInput();
//			break;
//		}
	}

	private void processDefaultInput()
	{

	}

	private void processBuildInput()
	{
		Vector3 _newMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		int tilePosX = Mathf.RoundToInt(_newMousePosition.x / OutlineTileSize);
		int tilePosY = Mathf.RoundToInt(_newMousePosition.y / OutlineTileSize);
		_newMousePosition = new Vector3(tilePosX, tilePosY, 0);

		if (Input.GetMouseButtonDown(0))
		{

		}
		else if (Input.GetMouseButtonUp(0))
		{

		}
		else if (CreatingOutline)
		{

		}
		else
		{
			Outline.transform.position = new Vector3(tilePosX * OutlineTileSize, tilePosY * OutlineTileSize, 0);
		}
	}

	public void EnterBuildState()
	{
		//GameState = GameStates.BuildState;
		Debug.Log("Entered Build State");

		OutlineHolder = new GameObject("OutlineHolder");
		Debug.Log("OutlineHolder Created");

		Outline = Instantiate(OutlinePrefab) as RoomOutline;
		Debug.Log("Outline Instantiated");

		Outline.transform.SetParent(OutlineHolder.transform);
		Debug.Log ("Outline Holder Set");

		Outline.GetComponent<SpriteRenderer>().color = new Color32(165, 165, 165, 100);
		Debug.Log("Outline Color Set");

		CreatingOutline = false;
	}

	public enum GameStates
	{
		DefaultlState,
		BuildState,
	}
}
