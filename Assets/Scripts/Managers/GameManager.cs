using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public BuildState buildState;
	public ManageState manageState;
	
	public IGameManagerState GameState;

	public GameObject foundationHolder;

	private Vector3 _oldMousePosition;
	
	void Start ()
	{
		GameState = manageState;
		foundationHolder = new GameObject("Foundation");
	}

	void Update ()
	{
		if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
		{
			handleInput();
		}
	}

	private void handleInput()
	{
		GameState.HandleInput();
	}

	public void ChangeState(IGameManagerState state)
	{
		GameState.Exit();
		GameState = state;
		GameState.Enter(this);
	}
}
