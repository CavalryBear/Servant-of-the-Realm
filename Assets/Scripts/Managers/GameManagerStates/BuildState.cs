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

	private Vector3 _firstTilePosition;
	private Vector3 _oldMousePosition;
	private GameManager _gameManager;

	private bool _axisDefined;
	private bool _isHorizontal;

	/// <summary>
	/// Function called when GameManager enters this state
	/// </summary>
	/// <param name="gameManager">Callback to the GameManager</param>
	/// <param name="operationCode">OperationCode</param>
	public void Enter(GameManager gameManager, int operationCode)
	{
		Vector3 _newMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		int tilePosX = Mathf.RoundToInt(_newMousePosition.x / outlineTileSize);
		int tilePosY = Mathf.RoundToInt(_newMousePosition.y / outlineTileSize);
		_newMousePosition = new Vector3(tilePosX, tilePosY, 0);

		_gameManager = gameManager;
		operation = (Operation)operationCode;

		outlineHolder = new GameObject("OutlineHolder");

		outline = Instantiate(outlinePrefab, new Vector3(tilePosX * outlineTileSize, tilePosY * outlineTileSize, 0), Quaternion.identity) as GameObject;
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
		int tilePosX = Mathf.RoundToInt(_newMousePosition.x / outlineTileSize);
		int tilePosY = Mathf.RoundToInt(_newMousePosition.y / outlineTileSize);
		_newMousePosition = new Vector3(tilePosX, tilePosY, 0);

		if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
		{
			outline.GetComponent<SpriteRenderer>().color = new Color32(84, 189, 84, 100);
			_oldMousePosition = _firstTilePosition = _newMousePosition;
			creatingOutline = true;
		}
		else if (Input.GetMouseButtonUp(0) && creatingOutline)
		{
			switch (operation)
			{
			case Operation.BuildFoundation:
				tavernBuilder.BuildFoundation((int)_firstTilePosition.x, (int)_firstTilePosition.y, (int)_oldMousePosition.x, (int)_oldMousePosition.y, _gameManager.foundationHolder);
				break;

			case Operation.BuildWall:
				if (_isHorizontal)
				{
					tavernBuilder.BuildObject(wallSpritePrefab, (int)_firstTilePosition.x, (int)_firstTilePosition.y, (int)_oldMousePosition.x, (int)_firstTilePosition.y, _gameManager.foundationHolder);
				}
				else
				{
					tavernBuilder.BuildObject(wallSpritePrefab, (int)_firstTilePosition.x, (int)_firstTilePosition.y, (int)_firstTilePosition.x, (int)_oldMousePosition.y, _gameManager.foundationHolder);
				}
				break;
			case Operation.BuildFloor:
				tavernBuilder.BuildObject(floorSpritePrefab, (int)_firstTilePosition.x, (int)_firstTilePosition.y, (int)_oldMousePosition.x, (int)_oldMousePosition.y, _gameManager.foundationHolder);
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
			if (!_oldMousePosition.Equals(_newMousePosition))
			{
				float _leftBorder, _bottomBorder, _topBorder, _rightBorder;

				switch (operation)
				{
				case Operation.BuildFoundation: case Operation.BuildFloor:
					_leftBorder = (Mathf.Min(_firstTilePosition.x, _newMousePosition.x) - 0.5f) * outlineTileSize;
					_bottomBorder = (Mathf.Min(_firstTilePosition.y, _newMousePosition.y) - 0.5f) * outlineTileSize;
					_rightBorder = (Mathf.Max(_firstTilePosition.x, _newMousePosition.x) + 0.5f) * outlineTileSize;
					_topBorder = (Mathf.Max(_firstTilePosition.y, _newMousePosition.y) + 0.5f) * outlineTileSize;
					
					outline.transform.position = new Vector3((_rightBorder + _leftBorder) / 2, (_topBorder + _bottomBorder) / 2);
					outline.transform.localScale = new Vector3((_rightBorder - _leftBorder) / outlineTileSize, (_topBorder - _bottomBorder) / outlineTileSize);
					break;
				case Operation.BuildWall:

					if (!_axisDefined)
					{
						_axisDefined = true;
						_isHorizontal = (Mathf.Abs(_newMousePosition.x - _firstTilePosition.x) >= Mathf.Abs(_newMousePosition.y - _firstTilePosition.y));
					}
					

					
					if (_isHorizontal)
					{
						_leftBorder = (Mathf.Min(_firstTilePosition.x, _newMousePosition.x) - 0.5f) * outlineTileSize;
						_bottomBorder = (_firstTilePosition.y - 0.5f) * outlineTileSize;
						_topBorder = (_firstTilePosition.y + 0.5f) * outlineTileSize;
						_rightBorder = (Mathf.Max(_firstTilePosition.x, _newMousePosition.x) + 0.5f) * outlineTileSize;
					}
					else
					{
						_leftBorder = (_firstTilePosition.x - 0.5f) * outlineTileSize;
						_rightBorder = (_firstTilePosition.x + 0.5f) * outlineTileSize;
						_bottomBorder = (Mathf.Min(_firstTilePosition.y, _newMousePosition.y) - 0.5f) * outlineTileSize;
						_topBorder = (Mathf.Max(_firstTilePosition.y, _newMousePosition.y) + 0.5f) * outlineTileSize;
					}
					
					outline.transform.position = new Vector3((_rightBorder + _leftBorder) / 2, (_topBorder + _bottomBorder) / 2);
					outline.transform.localScale = new Vector3((_rightBorder - _leftBorder) / outlineTileSize, (_topBorder - _bottomBorder) / outlineTileSize);
					break;
				}

				_oldMousePosition = _newMousePosition;
			}
		}
		else
		{
			outline.transform.position = new Vector3(tilePosX * outlineTileSize, tilePosY * outlineTileSize, 0);
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
