using UnityEngine;
using System.Collections;

public class BuildState : MonoBehaviour, IGameManagerState
{
	public GameObject OutlinePrefab;
	public GameObject OutlineHolder;
	public float OutlineTileSize;
	public GameObject Outline;
	public bool CreatingOutline;

	private Vector3 _firstTilePosition;
	private Vector3 _oldMousePosition;
	private GameManager _gameManager;

	public void Enter(GameManager gameManager)
	{
		_gameManager = gameManager;

		OutlineHolder = new GameObject("OutlineHolder");

		Outline = Instantiate(OutlinePrefab) as GameObject;
		Outline.transform.SetParent(OutlineHolder.transform);
		Outline.GetComponent<SpriteRenderer>().color = new Color32(165, 165, 165, 100);

		CreatingOutline = false;
	}

	public void HandleInput()
	{
		Vector3 _newMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		int tilePosX = Mathf.RoundToInt(_newMousePosition.x / OutlineTileSize);
		int tilePosY = Mathf.RoundToInt(_newMousePosition.y / OutlineTileSize);
		_newMousePosition = new Vector3(tilePosX, tilePosY, 0);

		if (Input.GetMouseButtonDown(0))
		{
			Debug.Log("Mouse Left Button Down Detected!");
			Outline.GetComponent<SpriteRenderer>().color = new Color32(84, 189, 84, 100);
			_oldMousePosition = _firstTilePosition = _newMousePosition;
			Debug.Log("Initial Mouse Position: " + _oldMousePosition.x + ", " + _oldMousePosition.y);
			CreatingOutline = true;
		}
		else if (Input.GetMouseButtonUp(0) && CreatingOutline)
		{
			Debug.Log("Mouse Left Button Up Detected!");
			Outline.GetComponent<SpriteRenderer>().color = new Color32(165, 165, 165, 100);
			CreatingOutline = false;
			Outline.transform.localScale = new Vector3(1, 1);
			Outline.transform.position = new Vector3(tilePosX * OutlineTileSize, tilePosY * OutlineTileSize, 0);
		}
		else if (CreatingOutline)
		{
			if (!_oldMousePosition.Equals(_newMousePosition))
			{
				Debug.Log("Creating Outline!");
				Debug.Log("New Mouse Position: " + _newMousePosition.x + ", " + _newMousePosition.y);
				float _leftBorder = (Mathf.Min(_firstTilePosition.x, _newMousePosition.x) - 0.5f) * OutlineTileSize;
				float _bottomBorder = (Mathf.Min(_firstTilePosition.y, _newMousePosition.y) - 0.5f) * OutlineTileSize;
				float _rightBorder = (Mathf.Max(_firstTilePosition.x, _newMousePosition.x) + 0.5f) * OutlineTileSize;
				float _topBorder = (Mathf.Max(_firstTilePosition.y, _newMousePosition.y) + 0.5f) * OutlineTileSize;
				
				Outline.transform.position = new Vector3((_rightBorder + _leftBorder) / 2, (_topBorder + _bottomBorder) / 2);
				Outline.transform.localScale = new Vector3((_rightBorder - _leftBorder) / OutlineTileSize, (_topBorder - _bottomBorder) / OutlineTileSize);
				
				_oldMousePosition = _newMousePosition;
			}
		}
		else if (Input.GetMouseButtonDown(1))
		{
			_gameManager.ChangeState(_gameManager.manageState);
		}
		else
		{
			Outline.transform.position = new Vector3(tilePosX * OutlineTileSize, tilePosY * OutlineTileSize, 0);
		}
	}

	public void Exit()
	{
		CreatingOutline = false;
		DestroyObject(OutlineHolder);
	}
}
