using UnityEngine;
using System.Collections;

public class Outline : MonoBehaviour
{
	public Vector2 currentTile;
	public Color32 passiveColor;
	public Color32 activeColor;
	public bool creatingOutline;
	public Vector2 firstSelectedTile;

	private Vector2 _oldTilePosition;

	public void Select()
	{
		GetComponent<SpriteRenderer>().color = activeColor;
		creatingOutline = true;
		firstSelectedTile = currentTile;
	}
	
	public void Unselect()
	{
		GetComponent<SpriteRenderer>().color = passiveColor;
	}
}
