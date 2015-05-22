using UnityEngine;
using System.Collections;

public class OutlineListener : MonoBehaviour
{
	public RoomOutline roomOutlinePrefab;
	public GameObject roomOutlineHolder;
	public RoomOutline roomOutline;
	public float tileSize = 0.32f;
	public bool creatingOutline;
	public Vector3 firstOutlineTilePosition;
	private Vector3 _oldMousePosition;
	
	void Awake()
	{
		Debug.Log("Starting Room Outline!");

		roomOutlineHolder = new GameObject("Room Outline");
		roomOutline = Instantiate(roomOutlinePrefab) as RoomOutline;
		roomOutline.transform.SetParent(roomOutlineHolder.transform);
		roomOutline.GetComponent<SpriteRenderer>().color = new Color32(165, 165, 165, 100);

		Vector3 startingMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		int tilePosX = Mathf.RoundToInt(startingMousePosition.x / tileSize);
		int tilePosY = Mathf.RoundToInt(startingMousePosition.y / tileSize);

		Debug.Log("Starting Mouse Position: " + tilePosX + ", " + tilePosY);

		roomOutline.transform.position = new Vector3(tilePosX * tileSize, tilePosY * tileSize, 0);

		creatingOutline = false;
	}

	void Update()
	{
		Vector3 _newMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		int tilePosX = Mathf.RoundToInt(_newMousePosition.x / tileSize);
		int tilePosY = Mathf.RoundToInt(_newMousePosition.y / tileSize);
		_newMousePosition = new Vector3(tilePosX, tilePosY, 0);

		if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
		{
			Debug.Log("Mouse Left Button Down Detected!");
			roomOutline.GetComponent<SpriteRenderer>().color = new Color32(84, 189, 84, 100);
			_oldMousePosition = firstOutlineTilePosition = _newMousePosition;
			Debug.Log("Initial Mouse Position: " + firstOutlineTilePosition.x + ", " + firstOutlineTilePosition.y);
			creatingOutline = true;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			Debug.Log("Mouse Left Button Up Detected!");
			roomOutline.GetComponent<SpriteRenderer>().color = new Color32(165, 165, 165, 100);
			creatingOutline = false;
			roomOutline.transform.localScale = new Vector3(1, 1);
			roomOutline.transform.position = new Vector3(tilePosX * tileSize, tilePosY * tileSize, 0);
		}
		else if (creatingOutline)
		{
			if (!_oldMousePosition.Equals(_newMousePosition))
			{
				Debug.Log("Creating Outline!");
				Debug.Log("New Mouse Position: " + _newMousePosition.x + ", " + _newMousePosition.y);
				float _leftBorder = (Mathf.Min(firstOutlineTilePosition.x, _newMousePosition.x) - 0.5f) * tileSize;
				float _bottomBorder = (Mathf.Min(firstOutlineTilePosition.y, _newMousePosition.y) - 0.5f) * tileSize;
				float _rightBorder = (Mathf.Max(firstOutlineTilePosition.x, _newMousePosition.x) + 0.5f) * tileSize;
				float _topBorder = (Mathf.Max(firstOutlineTilePosition.y, _newMousePosition.y) + 0.5f) * tileSize;

				roomOutline.transform.position = new Vector3((_rightBorder + _leftBorder) / 2, (_topBorder + _bottomBorder) / 2);
				roomOutline.transform.localScale = new Vector3((_rightBorder - _leftBorder) / tileSize, (_topBorder - _bottomBorder) / tileSize);

				_oldMousePosition = _newMousePosition;
			}

		}
		else
		{
			roomOutline.transform.position = new Vector3(tilePosX * tileSize, tilePosY * tileSize, 0);
		}
	}
}
