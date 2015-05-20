using UnityEngine;
using System.Collections;

public class RoomOutline : MonoBehaviour
{
	public bool mouseInside;

	void Awake ()
	{
		Debug.Log("Outline Awake!");
	}

	void Update ()
	{

	}

	void OnMouseEnter()
	{
		mouseInside = true;
	}

	void OnMouseExit()
	{
		mouseInside = false;
	}
}
