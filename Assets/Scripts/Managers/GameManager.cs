using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public FoundationBuilder foundationBuilder;

	public BuildState buildState;
	public ManageState manageState;
	
	public IGameManagerState GameState;
	

	private Vector3 _oldMousePosition;
	
	void Start ()
	{
		GameState = manageState;
		ChangeState(buildState);

		foundationBuilder.BuildFoundation(-2, -2, 2, 2);
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
