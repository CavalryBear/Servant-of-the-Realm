using UnityEngine;
using System.Collections;

public class MapGeneratorPlaceholder : MonoBehaviour {

	public GameObject MapTile;
	public int MapWidth;
	public int MapHeight;
	public float TileSize;

	public GameObject MapHolder;

	// Use this for initialization
	void Start ()
	{
		MapHolder = new GameObject("Map");
		generateMap(MapWidth, MapHeight, TileSize);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	private void generateMap(int mapWidth, int mapHeight, float tileSize)
	{
		float startingX = mapWidth % 2 == 0 ? -tileSize * mapWidth / 2 : (-tileSize * (mapWidth - 1) - tileSize) / 2;
		float startingY = mapHeight % 2 == 0 ? tileSize * mapHeight / 2 : (tileSize * (mapHeight - 1) + tileSize) / 2;

		Debug.Log("Starting X: " + startingX);
		Debug.Log("Starting Y: " + startingY);

		for (int i = 0; i < mapWidth; i++)
		{
			for (int j = 0; j < mapHeight; j++)
			{
				GameObject newSprite = Instantiate(MapTile, new Vector3(startingX + tileSize * i, startingY - tileSize * j, 0), Quaternion.identity) as GameObject;
				newSprite.transform.SetParent(MapHolder.transform);
			}
		}
	}
}
