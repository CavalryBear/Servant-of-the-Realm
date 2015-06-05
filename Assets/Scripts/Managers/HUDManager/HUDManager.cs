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
		case 0: case 11: case 12: case 21: case 22:
			gameManager.ChangeState(gameManager.buildState, buttonIndex);
			break;
		case 10:
			floorsMenu.SetActive(false);
			wallsMenu.SetActive(!wallsMenu.activeSelf);
			break;
		case 20:
			wallsMenu.SetActive(false);
			floorsMenu.SetActive(!floorsMenu.activeSelf);
			break;
		}
	}
}
