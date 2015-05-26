using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public float mapStartingX;
	public float mapStartingY;
	public int mapWidth;
	public int mapHeight;
	public float tileSize;
	public GameObject baseMapTile;
	public MapManager mapManager;

	public BuildState buildState;
	public ManageState manageState;
	
	public IGameManagerState GameState;

	private Vector3 _oldMousePosition;
	
	void Start ()
	{
		GameState = manageState;

		mapStartingX = mapWidth % 2 == 0 ? -tileSize * mapWidth / 2 : (-tileSize * (mapWidth - 1) - tileSize) / 2;
		mapStartingY = mapHeight % 2 == 0 ? tileSize * mapHeight / 2 : (tileSize * (mapHeight - 1) + tileSize) / 2;
		mapManager.GenerateMap(mapStartingX, mapStartingY, mapWidth, mapHeight, tileSize, baseMapTile);
	}

	void Update ()
	{
		handleInput();
	}

	private void handleInput()
	{
		GameState.HandleInput();
	}

	public void ChangeState(IGameManagerState state, int operationCode)
	{
		GameState.Exit();
		GameState = state;
		GameState.Enter(this, operationCode);
	}
}
