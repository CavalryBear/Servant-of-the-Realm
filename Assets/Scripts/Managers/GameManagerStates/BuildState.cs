using UnityEngine;
using System.Collections;

public class BuildState : MonoBehaviour, IGameManagerState
{
	public GameObject OutlinePrefab;
	public GameObject OutlineHolder;
	public float OutlineTileSize;
	public GameObject Outline;
	public bool CreatingOutline;

	private Vector3 _oldMousePosition;

	public void Enter()
	{
		OutlineHolder = new GameObject("OutlineHolder");
		Debug.Log("OutlineHolder Created");
		
		Outline = Instantiate(OutlinePrefab) as GameObject;
		Debug.Log("Outline Instantiated");
		
		Outline.transform.SetParent(OutlineHolder.transform);
		Debug.Log ("Outline Holder Set");
		
		Outline.GetComponent<SpriteRenderer>().color = new Color32(165, 165, 165, 100);
		Debug.Log("Outline Color Set");
		
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

	public void Exit()
	{

	}
}
