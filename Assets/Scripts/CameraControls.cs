using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour
{
	public float ScrollDistance;
	public float ScrollSpeed;

	// Update is called once per frame
	void Update ()
	{
		float _mousePosX = Input.mousePosition.x;
		float _mousePosY = Input.mousePosition.y;

		if (_mousePosX <= ScrollDistance)
		{
			transform.Translate(Vector3.right * -ScrollSpeed * Time.deltaTime);
		}
		else if (_mousePosX >= Screen.width - ScrollDistance)
		{
			transform.Translate(Vector3.right * ScrollSpeed * Time.deltaTime);
		}

		if (_mousePosY <= ScrollDistance)
		{
			transform.Translate(transform.up * -ScrollSpeed * Time.deltaTime);
		}
		else if (_mousePosY >= Screen.height - ScrollDistance)
		{
			transform.Translate(transform.up * ScrollSpeed * Time.deltaTime); 
		}
	}
}
