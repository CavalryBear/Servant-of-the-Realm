using UnityEngine;
using System.Collections;

public class TavernBuilder : MonoBehaviour
{
	public GameObject wallSpritePrefab;
	public GameObject floorSpritePrefab;
	public float tileSize;

	public void BuildFoundation(int initPosX, int initPosY, int finalPosX, int finalPosY, MapManager mapManager)
	{
		for (int x = Mathf.Min(initPosX, finalPosX); x <= Mathf.Max(initPosX, finalPosX); x++)
		{
			for (int y = Mathf.Min(initPosY, finalPosY); y <= Mathf.Max(initPosY, finalPosY); y++)
			{
				if (x == initPosX || x == finalPosX || y == initPosY || y == finalPosY)
				{
					mapManager.ReplaceTile(x, y, wallSpritePrefab, "Wall");
				}
				else
				{
					mapManager.ReplaceTile(x, y, floorSpritePrefab, "Floor");
				}
			}
		}
	}

	public void BuildArea(GameObject newObject, string name, int initPosX, int initPosY, int finalPosX, int finalPosY, MapManager mapManager)
	{
		for (int x = Mathf.Min(initPosX, finalPosX); x <= Mathf.Max(initPosX, finalPosX); x++)
		{
			for (int y = Mathf.Min(initPosY, finalPosY); y <= Mathf.Max(initPosY, finalPosY); y++)
			{
				mapManager.ReplaceTile(x, y, newObject, name);
			}
		}
	}
}
