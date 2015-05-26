using UnityEngine;
using System.Collections;

public class MapManager : MonoBehaviour
{
	public GameObject mapHolder;

	private float _mapStartingX;
	private float _mapStartingY;
	private int _mapWidth;
	private int _mapHeight;
	private GameObject _baseTile;
	private float _tileSize;

	private GameObject[,] _tileMap;

	public Vector2 GetTileCoordinates(float posX, float posY)
	{
		return new Vector2((int)((posX - _mapStartingX) / _tileSize), (int)((_mapStartingY - posY) / _tileSize));
	}

	public Vector3 GetWorldCoordinates(int posX, int posY)
	{
		return new Vector3(_mapStartingX + posX * _tileSize, _mapStartingY - posY * _tileSize, 0);
	}

	public void GenerateMap(float mapStartingX, float mapStartingY, int mapWidth, int mapHeight, float tileSize, GameObject baseTile)
	{
		_mapStartingX = mapStartingX;
		_mapStartingY = mapStartingY;
		_mapWidth = mapWidth;
		_mapHeight = mapHeight;
		_baseTile = baseTile;
		_tileSize = tileSize;

		_tileMap = new GameObject[mapWidth, mapHeight];

		mapHolder = new GameObject("Map");

		for (int i = 0; i < mapWidth; i++)
		{
			for (int j = 0; j < mapHeight; j++)
			{
				GameObject newTile = Instantiate(_baseTile, new Vector3(_mapStartingX + _tileSize * i, _mapStartingY - _tileSize * j, 0), Quaternion.identity) as GameObject;
				newTile.transform.SetParent(mapHolder.transform);
				newTile.name = "X: " + i + ", Y: " + j + " Grass";
				_tileMap[i, j] = newTile;
			}
		}
	}

	public void ReplaceTile(int posX, int posY, GameObject tile, string name)
	{
		Debug.Log("Replacing Tile " + posX + ", " + posY);
		GameObject newTile = Instantiate(tile, _tileMap[posX, posY].transform.position, Quaternion.identity) as GameObject;
		newTile.transform.SetParent(mapHolder.transform);
		newTile.name = "X: " + posX + ", " + posY + " " + name;
		DestroyObject(_tileMap[posX, posY]);
		_tileMap[posX, posY] = newTile;
	}
}
