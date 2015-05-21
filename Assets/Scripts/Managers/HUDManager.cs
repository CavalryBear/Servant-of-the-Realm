using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour
{
	public GameManager gameManager;

	public void HandleButton(int buttonIndex)
	{
		switch (buttonIndex)
		{
		case 0:
			gameManager.ChangeState(gameManager.buildState);
			break;
		}
	}
}
