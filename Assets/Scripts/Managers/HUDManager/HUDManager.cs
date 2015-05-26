using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour
{
	public GameManager gameManager;
	public GameObject wallsMenu;
	public GameObject floorsMenu;

	public void HandleButton(int buttonIndex)
	{
		switch (buttonIndex)
		{
		case 0: case 1: case 2:
			gameManager.ChangeState(gameManager.buildState, buttonIndex);
			break;
		case 100:
			floorsMenu.SetActive(false);
			wallsMenu.SetActive(!wallsMenu.activeSelf);
			break;
		case 101:
			wallsMenu.SetActive(false);
			floorsMenu.SetActive(!floorsMenu.activeSelf);
			break;
		}
	}
}
