using UnityEngine;
using System.Collections;

public class BuildState : MonoBehaviour, IGameManagerState
{
	public GameObject outlinePrefab;
	public GameObject outlineHolder;
	public FoundationBuilder foundationBuilder;
	public float outlineTileSize;
	public GameObject outline;
	public bool creatingOutline;

	private Vector3 _firstTilePosition;
	private Vector3 _oldMousePosition;
	private GameManager _gameManager;

	public void Enter(GameManager gameManager)
	{
		_gameManager = gameManager;

		outlineHolder = new GameObject("OutlineHolder");

		outline = Instantiate(outlinePrefab) as GameObject;
		outline.transform.SetParent(outlineHolder.transform);
		outline.GetComponent<SpriteRenderer>().color = new Color32(165, 165, 165, 100);

		creatingOutline = false;
	}

	public void HandleInput()
	{
		Vector3 _newMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		int tilePosX = Mathf.RoundToInt(_newMousePosition.x / outlineTileSize);
		int tilePosY = Mathf.RoundToInt(_newMousePosition.y / outlineTileSize);
		_newMousePosition = new Vector3(tilePosX, tilePosY, 0);

		if (Input.GetMouseButtonDown(0))
		{
			Debug.Log("Mouse Left Button Down Detected!");
			outline.GetComponent<SpriteRenderer>().color = new Color32(84, 189, 84, 100);
			_oldMousePosition = _firstTilePosition = _newMousePosition;
			Debug.Log("Initial Mouse Position: " + _oldMousePosition.x + ", " + _oldMousePosition.y);
			creatingOutline = true;
		}
		else if (Input.GetMouseButtonUp(0) && creatingOutline)
		{
			Debug.Log("Mouse Left Button Up Detected!");
			foundationBuilder.BuildFoundation((int)_firstTilePosition.x, (int)_firstTilePosition.y, (int)_oldMousePosition.x, (int)_oldMousePosition.y, _gameManager.foundationHolder);
			_gameManager.ChangeState(_gameManager.manageState);
		}
		else if (creatingOutline)
		{
			if (!_oldMousePosition.Equals(_newMousePosition))
			{
				Debug.Log("Creating Outline!");
				Debug.Log("New Mouse Position: " + _newMousePosition.x + ", " + _newMousePosition.y);
				float _leftBorder = (Mathf.Min(_firstTilePosition.x, _newMousePosition.x) - 0.5f) * outlineTileSize;
				float _bottomBorder = (Mathf.Min(_firstTilePosition.y, _newMousePosition.y) - 0.5f) * outlineTileSize;
				float _rightBorder = (Mathf.Max(_firstTilePosition.x, _newMousePosition.x) + 0.5f) * outlineTileSize;
				float _topBorder = (Mathf.Max(_firstTilePosition.y, _newMousePosition.y) + 0.5f) * outlineTileSize;
				
				outline.transform.position = new Vector3((_rightBorder + _leftBorder) / 2, (_topBorder + _bottomBorder) / 2);
				outline.transform.localScale = new Vector3((_rightBorder - _leftBorder) / outlineTileSize, (_topBorder - _bottomBorder) / outlineTileSize);
				
				_oldMousePosition = _newMousePosition;
			}
		}
		else if (Input.GetMouseButtonDown(1))
		{
			_gameManager.ChangeState(_gameManager.manageState);
		}
		else
		{
			outline.transform.position = new Vector3(tilePosX * outlineTileSize, tilePosY * outlineTileSize, 0);
		}
	}

	public void Exit()
	{
		creatingOutline = false;
		DestroyObject(outlineHolder);
	}
}
