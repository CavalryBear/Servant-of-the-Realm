using UnityEngine;
using System.Collections;

public class TavernBuilder : MonoBehaviour
{
	public GameObject wallSpritePrefab;
	public GameObject floorSpritePrefab;
	public float tileSize;

	public void BuildFoundation(int initPosX, int initPosY, int finalPosX, int finalPosY, GameObject foundationHolder)
	{
		for (int x = Mathf.Min(initPosX, finalPosX); x <= Mathf.Max(initPosX, finalPosX); x++)
		{
			for (int y = Mathf.Min(initPosY, finalPosY); y <= Mathf.Max(initPosY, finalPosY); y++)
			{
				GameObject tile;
				if (x == initPosX || x == finalPosX || y == initPosY || y == finalPosY)
				{
					tile = Instantiate(wallSpritePrefab, new Vector3(x * tileSize, y * tileSize), Quaternion.identity) as GameObject;
					tile.name = "Wall X: " + x + ", Y: " + y;
				}
				else
				{
					tile = Instantiate(floorSpritePrefab, new Vector3(x * tileSize, y * tileSize), Quaternion.identity) as GameObject;
					tile.name = "Floor X: " + x + ", Y: " + y;
				}

				tile.transform.SetParent(foundationHolder.transform);
			}
		}
	}

	public void BuildObject(GameObject newObject, int initPosX, int initPosY, int finalPosX, int finalPosY, GameObject foundationHolder)
	{
		for (int x = Mathf.Min(initPosX, finalPosX); x <= Mathf.Max(initPosX, finalPosX); x++)
		{
			for (int y = Mathf.Min(initPosY, finalPosY); y <= Mathf.Max(initPosY, finalPosY); y++)
			{
				GameObject tile;
				tile = Instantiate(newObject, new Vector3(x * tileSize, y * tileSize), Quaternion.identity) as GameObject;
				tile.name = "Wall X: " + x + ", Y: " + y;
				tile.transform.SetParent(foundationHolder.transform);
			}
		}
	}
}
