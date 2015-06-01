using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
	public float mapStartingX;
	public float mapStartingY;
	public int mapWidth;
	public int mapHeight;
	public MapTile baseTile;
	public float tileSize;

	public List<TileLayer> tileLayers;
	private GameObject[,] _tileMap;

	public Outline outline;

	public Vector2 GetTileCoordinates(Layers layer, float posX, float posY)
	{

		return tileLayers[(int)layer].GetTileCoordinates(posX, posY);
	}

	public Vector3 GetWorldCoordinates(Layers layer, int posX, int posY)
	{
		return tileLayers[(int)layer].GetWorldCoordinates(posX, posY);
	}

	public void GenerateMap(MapTile baseTile)
	{
		mapStartingX = mapWidth % 2 == 0 ? -tileSize * mapWidth / 2 : (-tileSize * (mapWidth - 1) - tileSize) / 2;
		mapStartingY = mapHeight % 2 == 0 ? tileSize * mapHeight / 2 : (tileSize * (mapHeight - 1) + tileSize) / 2;
		_tileMap = new GameObject[mapWidth, mapHeight];

		outline = Instantiate(outline);
		outline.creatingOutline = false;
		outline.name = "Outline";
		outline.transform.SetParent(transform);

		for (int i = 0; i < tileLayers.Count; i++)
		{
			TileLayer layer = tileLayers[i];
			string layerName = layer.name;
			layer = Instantiate(layer) as TileLayer;
			layer.name = layerName;
			layer.transform.SetParent(transform);
			layer.startingX = mapStartingX;
			layer.startingY = mapStartingY;
			layer.CreateMap(mapWidth, mapHeight);
			tileLayers[i] = layer;
		}

		tileLayers[(int)Layers.Floor].FillLayer(this.baseTile);
	}

	public void ReplaceTile(Layers layer, int posX, int posY, MapTile tile)
	{
		tileLayers[(int)layer].ReplaceTile(posX, posY, tile);
	}

	public void ReplaceTile(int posX, int posY, GameObject tile, string name)
	{
		Debug.Log("Replacing Tile " + posX + ", " + posY);
		GameObject newTile = Instantiate(tile, _tileMap[posX, posY].transform.position, Quaternion.identity) as GameObject;
		newTile.name = "X: " + posX + ", " + posY + " " + name;
		DestroyObject(_tileMap[posX, posY]);
		_tileMap[posX, posY] = newTile;
	}

	public void ActivateOutline(Vector3 initialPosition)
	{
		outlinePosition = initialPosition;
		outline.gameObject.SetActive(true);
		outline.Unselect();
	}

	public void DeactivateOutline()
	{
		outline.Unselect();
		outline.gameObject.SetActive(false);
	}

	public void SelectOutline()
	{
		outline.Select();
	}

	public void StretchOutline(bool area, Vector3 mousePosition)
	{
//		float leftBorder, topBorder, rightBorder, bottomBorder;

//		if (area)
//		{
//			leftBorder = mapStartingX + (Mathf.Min(outline.firstSelectedTile.x, outline.currentTile.x) - 0.5f) * tileSize;
//			bottomBorder = mapStartingY - (Mathf.Min(outline.firstSelectedTile.y, outline.currentTile.y) - 0.5f) * tileSize;
//			rightBorder = mapStartingX + (Mathf.Max(outline.firstSelectedTile.x, outline.currentTile.x) + 0.5f) * tileSize;
//			topBorder = mapStartingY - (Mathf.Max(outline.firstSelectedTile.y, outline.currentTile.y) + 0.5f) * tileSize;
//		}


		outline.Stretch(GetTileCoordinates(Layers.Floor, mousePosition.x, mousePosition.y), area, tileSize);
	}

	public Vector3 outlinePosition
	{
		set
		{
			outline.SetTile(GetTileCoordinates(Layers.Floor, value.x, value.y));
			outline.transform.position = GetWorldCoordinates(Layers.Floor, (int)outline.currentTile.x, (int)outline.currentTile.y);
		}
	}

	public bool creatingOutline
	{
		get { return outline.creatingOutline; }
	}

	public enum Layers
	{
		Floor,
		Wall,
	}
}
