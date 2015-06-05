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
		creatingOutline = false;
		GetComponent<SpriteRenderer>().color = passiveColor;
	}

	public void SetTile(Vector2 newTilePosition)
	{
		_oldTilePosition = currentTile;
		currentTile = newTilePosition;
	}

	public void Stretch(Vector2 newTilePosition, bool area, float tileSize)
	{
		if (!_oldTilePosition.Equals(newTilePosition))
		{
			if (area)
			{

			}
		}

		transform.position = new Vector3((rightBorder + leftBorder) / 2, (topBorder + bottomBorder) / 2);
		transform.localScale = new Vector3((rightBorder - leftBorder) / tileSize, (topBorder - bottomBorder) / tileSize);
	}
}
