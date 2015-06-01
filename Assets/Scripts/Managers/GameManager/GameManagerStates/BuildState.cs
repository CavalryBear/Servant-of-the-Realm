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
	public TavernBuilder tavernBuilder;
	public Operation operation;

	public GameObject wall1SpritePrefab;
	public GameObject wall2SpritePrefab;
	public GameObject floor1SpritePrefab;
	public GameObject floor2SpritePrefab;

	private Vector2 _firstTilePosition;
	private Vector2 _oldTilePosition;
	private GameManager _gameManager;
	private MapManager _mapManager;
	private GameObject spriteToBuild;

	private int _operationCode;
	private bool _axisDefined;
	private bool _isHorizontal;

	/// <summary>
	/// Function called when GameManager enters this state
	/// </summary>
	/// <param name="gameManager">Callback to the GameManager</param>
	/// <param name="operationCode">OperationCode</param>
	public void Enter(GameManager gameManager, int operationCode)
	{
		Debug.Log("Entering BuildState");
		_gameManager = gameManager;
		_mapManager = gameManager.mapManager;
		//_outlineManager = _mapManager.outlineManager;
		_operationCode = operationCode;

		switch (_operationCode)
		{
		case 11:
			spriteToBuild = wall1SpritePrefab;
			break;
		case 12:
			spriteToBuild = wall2SpritePrefab;
			break;
		case 21:
			spriteToBuild = floor1SpritePrefab;
			break;
		case 22:
			spriteToBuild = floor2SpritePrefab;
			break;
		}

		operation = (Operation)(_operationCode / 10);

		//Vector3 _newMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//Vector2 _tileCoordinates = _mapManager.GetTileCoordinates(_newMousePosition.x, _newMousePosition.y);
		_mapManager.ActivateOutline(Camera.main.ScreenToWorldPoint(Input.mousePosition));

		//_outlineManager.OutlinePosition = _mapManager.GetWorldCoordinates();

		//outlineHolder = new GameObject("OutlineHolder");

		//outline = Instantiate(outlinePrefab, _mapManager.GetWorldCoordinates((int)_tileCoordinates.x, (int)_tileCoordinates.y), Quaternion.identity) as GameObject;
		//outline.transform.SetParent(outlineHolder.transform);
		//outline.GetComponent<SpriteRenderer>().color = new Color32(165, 165, 165, 100);

		_axisDefined = false;
	}

	/// <summary>
	/// Handles the input.
	/// </summary>
	public void HandleInput()
	{
//		Vector3 _newMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//		Vector2 _tileCoordinates = _mapManager.GetTileCoordinates(_newMousePosition.x, _newMousePosition.y);
//
		if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
		{
////			Vector2 tile = _mapManager.GetTileCoordinates(_newMousePosition.x, _newMousePosition.y);
////			outline.GetComponent<SpriteRenderer>().color = new Color32(84, 189, 84, 100);
			_mapManager.SelectOutline();
//
//			//_oldTilePosition = _firstTilePosition = _tileCoordinates;
//			creatingOutline = true;
		}
		else if (Input.GetMouseButtonDown(1))
		{
			if (Input.GetMouseButton(0))
			{
				_gameManager.ChangeState(_gameManager.buildState, _operationCode);
			}
			else
			{
				_gameManager.ChangeState(_gameManager.manageState, 0);
			}
		}
		else if (Input.GetMouseButtonUp(0) && _mapManager.creatingOutline)
		{
			switch (operation)
			{
			case Operation.BuildFoundation:
				//tavernBuilder.BuildFoundation((int)_mapManager.outline.firstSelectedTile.x, (int)_mapManager.outline.firstSelectedTile.y, )
				break;

			case Operation.BuildWall:
				break;

			case Operation.BuildFloor:
				break;
			}

			_gameManager.ChangeState(_gameManager.buildState, _operationCode);
		}
		else if (_mapManager.creatingOutline)
		{
			_mapManager.StretchOutline(operation == Operation.BuildWall ? false : true, Camera.main.ScreenToWorldPoint(Input.mousePosition));
		}
		else
		{
			_mapManager.outlinePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
//		else if (Input.GetMouseButtonUp(0) && creatingOutline)
//		{
//			switch (operation)
//			{
//			case Operation.BuildFoundation:
//				tavernBuilder.BuildFoundation((int)_firstTilePosition.x, (int)_firstTilePosition.y, (int)_oldTilePosition.x, (int)_oldTilePosition.y, _mapManager);
//				break;
//
//			case Operation.BuildWall:
//				if (_isHorizontal)
//				{
//					tavernBuilder.BuildArea(spriteToBuild, "Wall", (int)_firstTilePosition.x, (int)_firstTilePosition.y, (int)_oldTilePosition.x, (int)_firstTilePosition.y, _mapManager);
//				}
//				else
//				{
//					tavernBuilder.BuildArea(spriteToBuild, "Wall", (int)_firstTilePosition.x, (int)_firstTilePosition.y, (int)_firstTilePosition.x, (int)_oldTilePosition.y, _mapManager);
//				}
//				break;
//			case Operation.BuildFloor:
//				tavernBuilder.BuildArea(spriteToBuild, "Floor", (int)_firstTilePosition.x, (int)_firstTilePosition.y, (int)_oldTilePosition.x, (int)_oldTilePosition.y, _mapManager);
//				break;
//			}
//
//			_gameManager.ChangeState(_gameManager.buildState, _operationCode);
//		}
//		}
//		else if (creatingOutline)
//		{
//			if (!_oldTilePosition.Equals(_tileCoordinates))
//			{
//				float _leftBorder, _bottomBorder, _topBorder, _rightBorder;
//
//				switch (operation)
//				{
//				case Operation.BuildFoundation: case Operation.BuildFloor:
//					_leftBorder = _mapManager.mapStartingX + (Mathf.Min(_firstTilePosition.x, _tileCoordinates.x) - 0.5f) * outlineTileSize;
//					_bottomBorder = _mapManager.mapStartingY - (Mathf.Min(_firstTilePosition.y, _tileCoordinates.y) - 0.5f) * outlineTileSize;
//					_rightBorder = _mapManager.mapStartingX + (Mathf.Max(_firstTilePosition.x, _tileCoordinates.x) + 0.5f) * outlineTileSize;
//					_topBorder = _mapManager.mapStartingY - (Mathf.Max(_firstTilePosition.y, _tileCoordinates.y) + 0.5f) * outlineTileSize;
//					
//					outline.transform.position = new Vector3((_rightBorder + _leftBorder) / 2, (_topBorder + _bottomBorder) / 2);
//					outline.transform.localScale = new Vector3((_rightBorder - _leftBorder) / outlineTileSize, (_topBorder - _bottomBorder) / outlineTileSize);
//					break;
//				case Operation.BuildWall:
//
//					if (!_axisDefined)
//					{
//						_axisDefined = true;
//						_isHorizontal = (Mathf.Abs(_tileCoordinates.x - _firstTilePosition.x) >= Mathf.Abs(_firstTilePosition.y - _tileCoordinates.y));
//					}
//					
//					if (_isHorizontal)
//					{
//						_leftBorder = _mapManager.mapStartingX + (Mathf.Min(_firstTilePosition.x, _tileCoordinates.x) - 0.5f) * outlineTileSize;
//						_bottomBorder = _mapManager.mapStartingY - (_firstTilePosition.y - 0.5f) * outlineTileSize;
//						_topBorder = _mapManager.mapStartingY - (_firstTilePosition.y + 0.5f) * outlineTileSize;
//						_rightBorder = _mapManager.mapStartingX + (Mathf.Max(_firstTilePosition.x, _tileCoordinates.x) + 0.5f) * outlineTileSize;
//					}
//					else
//					{
//						_leftBorder = _mapManager.mapStartingX + (_firstTilePosition.x - 0.5f) * outlineTileSize;
//						_rightBorder = _mapManager.mapStartingX + (_firstTilePosition.x + 0.5f) * outlineTileSize;
//						_bottomBorder = _mapManager.mapStartingY - (Mathf.Min(_firstTilePosition.y, _tileCoordinates.y) - 0.5f) * outlineTileSize;
//						_topBorder = _mapManager.mapStartingY - (Mathf.Max(_firstTilePosition.y, _tileCoordinates.y) + 0.5f) * outlineTileSize;
//					}
//					
//					outline.transform.position = new Vector3((_rightBorder + _leftBorder) / 2, (_topBorder + _bottomBorder) / 2);
//					outline.transform.localScale = new Vector3((_rightBorder - _leftBorder) / outlineTileSize, (_topBorder - _bottomBorder) / outlineTileSize);
//					break;
//				}
//
//				_oldTilePosition = _tileCoordinates;
//			}
//		}

	}

	/// <summary>
	/// Function called when GameManager exits this state
	/// </summary>
	public void Exit()
	{
		_mapManager.DeactivateOutline();
		//DestroyObject(outlineHolder);
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
