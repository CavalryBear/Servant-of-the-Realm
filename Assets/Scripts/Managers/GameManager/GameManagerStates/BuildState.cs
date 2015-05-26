using UnityEngine;
using System.Collections;

/// <summary>
/// Build State of GameManager.
/// Creates UI elements and processes input during game steps relevant to building the tavern.
/// Associated Operations:
/// 	- Build Foundation
/// 	- Build Wall
/// 	- Build Floor
/// </summary>

public class BuildState : MonoBehaviour, IGameManagerState
{
	public GameObject outlinePrefab;
	public GameObject outlineHolder;
	public TavernBuilder tavernBuilder;
	public float outlineTileSize;
	public GameObject outline;
	public bool creatingOutline;
	public Operation operation;

	public GameObject wallSpritePrefab;
	public GameObject floorSpritePrefab;

	private Vector2 _firstTilePosition;
	private Vector2 _oldTilePosition;
	private GameManager _gameManager;
	private MapManager _mapManager;

	private bool _axisDefined;
	private bool _isHorizontal;

	/// <summary>
	/// Function called when GameManager enters this state
	/// </summary>
	/// <param name="gameManager">Callback to the GameManager</param>
	/// <param name="operationCode">OperationCode</param>
	public void Enter(GameManager gameManager, int operationCode)
	{
		_gameManager = gameManager;
		_mapManager = gameManager.mapManager;
		operation = (Operation)operationCode;

		Vector3 _newMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 _tileCoordinates = _mapManager.GetTileCoordinates(_newMousePosition.x, _newMousePosition.y);
//		int tilePosX = Mathf.RoundToInt((_newMousePosition.x - _gameManager.mapStartingX) / _gameManager.tileSize);
//		int tilePosY = Mathf.RoundToInt((_gameManager.mapStartingY - _newMousePosition.y) / _gameManager.tileSize);
//		_newMousePosition = new Vector3(tilePosX, tilePosY, 0);

		outlineHolder = new GameObject("OutlineHolder");

		outline = Instantiate(outlinePrefab, _mapManager.GetWorldCoordinates((int)_tileCoordinates.x, (int)_tileCoordinates.y), Quaternion.identity) as GameObject;
		outline.transform.SetParent(outlineHolder.transform);
		outline.GetComponent<SpriteRenderer>().color = new Color32(165, 165, 165, 100);

		creatingOutline = false;
		_axisDefined = false;
	}

	/// <summary>
	/// Handles the input.
	/// </summary>
	public void HandleInput()
	{
		Vector3 _newMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 _tileCoordinates = _mapManager.GetTileCoordinates(_newMousePosition.x, _newMousePosition.y);

		if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
		{
			Vector2 tile = _mapManager.GetTileCoordinates(_newMousePosition.x, _newMousePosition.y);
			outline.GetComponent<SpriteRenderer>().color = new Color32(84, 189, 84, 100);
			_oldTilePosition = _firstTilePosition = _tileCoordinates;
			creatingOutline = true;
		}
		else if (Input.GetMouseButtonUp(0) && creatingOutline)
		{
			switch (operation)
			{
			case Operation.BuildFoundation:
				tavernBuilder.BuildFoundation((int)_firstTilePosition.x, (int)_firstTilePosition.y, (int)_oldTilePosition.x, (int)_oldTilePosition.y, _mapManager);
				break;

			case Operation.BuildWall:
				if (_isHorizontal)
				{
					tavernBuilder.BuildArea(wallSpritePrefab, "Wall", (int)_firstTilePosition.x, (int)_firstTilePosition.y, (int)_oldTilePosition.x, (int)_firstTilePosition.y, _mapManager);
				}
				else
				{
					tavernBuilder.BuildArea(wallSpritePrefab, "Wall", (int)_firstTilePosition.x, (int)_firstTilePosition.y, (int)_firstTilePosition.x, (int)_oldTilePosition.y, _mapManager);
				}
				break;
			case Operation.BuildFloor:
				tavernBuilder.BuildArea(floorSpritePrefab, "Floor", (int)_firstTilePosition.x, (int)_firstTilePosition.y, (int)_oldTilePosition.x, (int)_oldTilePosition.y, _mapManager);
				break;
			}

			_gameManager.ChangeState(_gameManager.buildState, (int)operation);
		}
		else if (Input.GetMouseButtonDown(1))
		{
			if (Input.GetMouseButton(0))
			{
				_gameManager.ChangeState(_gameManager.buildState, (int)operation);
			}
			else
			{
				_gameManager.ChangeState(_gameManager.manageState, 0);
			}
		}
		else if (creatingOutline)
		{
			if (!_oldTilePosition.Equals(_tileCoordinates))
			{
				float _leftBorder, _bottomBorder, _topBorder, _rightBorder;

				switch (operation)
				{
				case Operation.BuildFoundation: case Operation.BuildFloor:
					_leftBorder = _gameManager.mapStartingX + (Mathf.Min(_firstTilePosition.x, _tileCoordinates.x) - 0.5f) * outlineTileSize;
					_bottomBorder = _gameManager.mapStartingY - (Mathf.Min(_firstTilePosition.y, _tileCoordinates.y) - 0.5f) * outlineTileSize;
					_rightBorder = _gameManager.mapStartingX + (Mathf.Max(_firstTilePosition.x, _tileCoordinates.x) + 0.5f) * outlineTileSize;
					_topBorder = _gameManager.mapStartingY - (Mathf.Max(_firstTilePosition.y, _tileCoordinates.y) + 0.5f) * outlineTileSize;
					
					outline.transform.position = new Vector3((_rightBorder + _leftBorder) / 2, (_topBorder + _bottomBorder) / 2);
					outline.transform.localScale = new Vector3((_rightBorder - _leftBorder) / outlineTileSize, (_topBorder - _bottomBorder) / outlineTileSize);
					break;
				case Operation.BuildWall:

					if (!_axisDefined)
					{
						_axisDefined = true;
						_isHorizontal = (Mathf.Abs(_tileCoordinates.x - _firstTilePosition.x) >= Mathf.Abs(_firstTilePosition.y - _tileCoordinates.y));
					}
					
					if (_isHorizontal)
					{
						_leftBorder = _gameManager.mapStartingX + (Mathf.Min(_firstTilePosition.x, _tileCoordinates.x) - 0.5f) * outlineTileSize;
						_bottomBorder = _gameManager.mapStartingY - (_firstTilePosition.y - 0.5f) * outlineTileSize;
						_topBorder = _gameManager.mapStartingY - (_firstTilePosition.y + 0.5f) * outlineTileSize;
						_rightBorder = _gameManager.mapStartingX + (Mathf.Max(_firstTilePosition.x, _tileCoordinates.x) + 0.5f) * outlineTileSize;
					}
					else
					{
						_leftBorder = _gameManager.mapStartingX + (_firstTilePosition.x - 0.5f) * outlineTileSize;
						_rightBorder = _gameManager.mapStartingX + (_firstTilePosition.x + 0.5f) * outlineTileSize;
						_bottomBorder = _gameManager.mapStartingY - (Mathf.Min(_firstTilePosition.y, _tileCoordinates.y) - 0.5f) * outlineTileSize;
						_topBorder = _gameManager.mapStartingY - (Mathf.Max(_firstTilePosition.y, _tileCoordinates.y) + 0.5f) * outlineTileSize;
					}
					
					outline.transform.position = new Vector3((_rightBorder + _leftBorder) / 2, (_topBorder + _bottomBorder) / 2);
					outline.transform.localScale = new Vector3((_rightBorder - _leftBorder) / outlineTileSize, (_topBorder - _bottomBorder) / outlineTileSize);
					break;
				}

				_oldTilePosition = _tileCoordinates;
			}
		}
		else
		{
			outline.transform.position = _mapManager.GetWorldCoordinates((int)_tileCoordinates.x, (int)_tileCoordinates.y);
		}
	}

	/// <summary>
	/// Function called when GameManager exits this state
	/// </summary>
	public void Exit()
	{
		creatingOutline = false;
		DestroyObject(outlineHolder);
	}

	/// <summary>
	/// List of all operations available to this state
	/// </summary>
	public enum Operation
	{
		BuildFoundation,
		BuildWall,
		BuildFloor,
	}
}
