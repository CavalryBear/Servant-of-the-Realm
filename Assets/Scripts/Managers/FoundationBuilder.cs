using UnityEngine;
using System.Collections;

public class FoundationBuilder : MonoBehaviour
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
