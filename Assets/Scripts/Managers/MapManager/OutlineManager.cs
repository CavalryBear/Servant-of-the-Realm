using UnityEngine;
using System.Collections;

public class OutlineManager : MonoBehaviour
{
	public GameObject outlineTile;
	public Color32 passiveColor;
	public Color32 activeColor;

	void Awake()
	{
		outlineTile.GetComponent<SpriteRenderer>().color = passiveColor;
	}

	public void SetActive(bool state)
	{
		outlineTile.SetActive(state);
	}

	public void Select()
	{
		outlineTile.GetComponent<SpriteRenderer>().color = activeColor;
	}

	public void Unselect()
	{
		outlineTile.GetComponent<SpriteRenderer>().color = passiveColor;
	}

	public Vector3 OutlinePosition
	{
		set { outlineTile.transform.position = value; }
	}
}
