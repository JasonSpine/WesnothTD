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
		if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < 409.0f) {
			if (Input.GetMouseButton (0)) {
				if (!Clicked) {
					LastMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					Clicked = true;
				} else {
					Vector2 MouseShift = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - LastMousePos);

					Vector2 NewPos = MouseShift + _RectTransform.anchoredPosition;

					if (NewPos.x < -FieldsMainController.MapWidth + 1366.0f - 270.0f - 64.0f - BoundarySize) {
						NewPos.x = -FieldsMainController.MapWidth + 1366.0f - 270.0f - 64.0f - BoundarySize;
					} else if (NewPos.x > BoundarySize) {
						NewPos.x = BoundarySize;
					}

					if (NewPos.y > FieldsMainController.MapHeight -768.0f + 64.0f + BoundarySize) {
						NewPos.y = FieldsMainController.MapHeight -768.0f + 64.0f + BoundarySize;
					} else if (NewPos.y < -BoundarySize) {
						NewPos.y = -BoundarySize;
					}

					_RectTransform.anchoredPosition = NewPos;



					LastMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				}
			} else {
				Clicked = false;
			}
		} else {
			Clicked = false;
		}
	}
}
