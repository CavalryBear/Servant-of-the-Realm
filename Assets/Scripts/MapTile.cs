﻿using UnityEngine;
using System.Collections;

public class MapTile : MonoBehaviour
{
	public string tileName;
	public TileType tileType;

	public enum TileType
	{
		Floor,
		Wall,
	}
}
