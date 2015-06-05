using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public MapTile baseMapTile;
	public MapManager mapManager;

	public BuildState buildState;
	public ManageState manageState;
	
	public IGameManagerState GameState;

	private Vector3 _oldMousePosition;
	
	void Start ()
	{
		GameState = manageState;

		generateMap();
	}

	void Update ()
	{
		handleInput();
	}

	private void generateMap()
	{
		mapManager = Instantiate(mapManager);
		mapManager.name = "Map";
		mapManager.GenerateMap(baseMapTile);
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
