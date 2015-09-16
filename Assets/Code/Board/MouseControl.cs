using UnityEngine;
using System.Collections;

public class MouseControl : MonoBehaviour {
	Vector3 LastMousePos;
	bool Clicked = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.mousePosition.x < Screen.width - 270) {
			if (Input.GetMouseButton (0)) {
				if (!Clicked) {
					LastMousePos = Input.mousePosition;
					Clicked = true;
				} else {
					Vector2 MouseShift = (Vector2)(Input.mousePosition - LastMousePos);

					transform.localPosition += (Vector3)MouseShift;

					LastMousePos = Input.mousePosition;
				}
			} else {
				Clicked = false;
			}
		} else {
			Clicked = false;
		}
	}
}
