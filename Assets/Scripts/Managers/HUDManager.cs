using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour
{
	public GameManager gameManager;

	public void HandleButton(int buttonIndex)
	{
		switch (buttonIndex)
		{
		case 0: case 1: case 2:
			gameManager.ChangeState(gameManager.buildState, buttonIndex);
			break;
		}
	}
}
