using UnityEngine;
using System.Collections;

public class FoundationBuilder : MonoBehaviour
{
	public GameObject wallSpritePrefab;
	public GameObject floorSpritePrefab;
	public GameObject foundationHolder;
	public float tileSize;

	void Awake()
	{
		foundationHolder = new GameObject("Foundation");
	}

	public void BuildFoundation(int initPosX, int initPosY, int finalPosX, int finalPosY)
	{
		for (int x = initPosX; x <= finalPosX; x++)
		{
			for (int y = initPosY; y <= finalPosY; y++)
			{
				GameObject tile;
				if (x == initPosX || x == finalPosX || y == initPosY || y == finalPosY)
				{
					tile = Instantiate(wallSpritePrefab, new Vector3(x * tileSize, y * tileSize), Quaternion.identity) as GameObject;
				}
				else
				{
					tile = Instantiate(floorSpritePrefab, new Vector3(x * tileSize, y * tileSize), Quaternion.identity) as GameObject;
				}

				tile.transform.SetParent(foundationHolder.transform);
			}
		}
	}
}
