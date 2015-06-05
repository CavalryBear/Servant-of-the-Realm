using UnityEngine;
using System.Collections;

public class TileLayer : MonoBehaviour
{
	public float startingX;
	public float startingY;
	public int width;
	public int height;
	public float tileSize;

	private MapTile[,] _layerTiles;

	public Vector2 GetTileCoordinates(float posX, float posY)
	{
		return new Vector2((int)((posX - startingX) / tileSize), (int)((startingY - posY) / tileSize));
	}

	public Vector3 GetWorldCoordinates(int posX, int posY)
	{
		return new Vector3(startingX + posX * tileSize, startingY - posY * tileSize, 0);
	}

	public void CreateMap(int width, int height)
	{
		this.width = width;
		this.height = height;
		_layerTiles = new MapTile[width, height];
	}

	public void PutTile(int posX, int posY, MapTile tile)
	{
		Debug.Log(transform.name + " Creating Tile X: " + posX + ", Y: " + posY);
		MapTile newTile = Instantiate(tile, GetWorldCoordinates(posX, posY), Quaternion.identity) as MapTile;
		newTile.name = tile.tileName + " X: " + posX + ", Y: " + posY;
		newTile.transform.SetParent(transform);
		_layerTiles[posX, posY] = newTile;
	}

	public void FillLayer(MapTile tile)
	{
		Debug.Log("Filling " + transform.name + " Layer of " + this.width + " by " + this.height);
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				PutTile(x, y, tile);
			}
		}
	}

	public void ReplaceTile(int posX, int posY, MapTile tile)
	{
		Debug.Log(transform.name + " Replacing Tile " + posX + ", " + posY);
		DestroyObject(_layerTiles[posX, posY]);
		PutTile(posX, posY, tile);
	}
}
