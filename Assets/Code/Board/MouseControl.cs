using UnityEngine;
using System.Collections;

public class MouseControl : MonoBehaviour {
	public RectTransform _RectTransform;
	public float BoundarySize = 20.0f;

	Vector3 LastMousePos;
	bool Clicked = false;
	// Use this for initialization
	void Start () {
		_RectTransform.anchoredPosition = new Vector2(BoundarySize, -BoundarySize);
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

					Vector2 NewPos = MouseShift + _RectTransform.anchoredPosition;

					if (NewPos.x < BoundarySize) {
						NewPos.x = BoundarySize;
					}

					if (NewPos.y > -BoundarySize) {
						NewPos.y = -BoundarySize;
					}

					_RectTransform.anchoredPosition = NewPos;



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
