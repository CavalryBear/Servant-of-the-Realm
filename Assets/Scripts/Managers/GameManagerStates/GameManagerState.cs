using UnityEngine;
using System.Collections;

public interface IGameManagerState
{
	void Enter(GameManager gameManager, int operationCode);

	void HandleInput();

	void Exit();
}
