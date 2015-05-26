using UnityEngine;
using System.Collections;

public class ManageState : MonoBehaviour, IGameManagerState
{
	private GameManager _gameManager;

	public void Enter(GameManager gameManager, int operationCode)
	{
		_gameManager = gameManager;
	}

	public void HandleInput()
	{

	}

	public void Exit()
	{

	}
}
