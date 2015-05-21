using UnityEngine;
using System.Collections;

public interface IGameManagerState
{
	void Enter(GameManager gameManager);

	void HandleInput();

	void Exit();
}
