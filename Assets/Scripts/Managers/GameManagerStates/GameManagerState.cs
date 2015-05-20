using UnityEngine;
using System.Collections;

public interface IGameManagerState
{
	void Enter();

	void HandleInput();

	void Exit();
}
