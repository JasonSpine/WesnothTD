using UnityEngine;
using System.Collections;

public class FieldsMainController : MonoBehaviour {
	public static float MapWidth = -1.0f;
	public static float MapHeight = 1.0f;
	// Use this for initialization
	void Start () {
		foreach (RectTransform field in GetComponentsInChildren<RectTransform> ()) {
			if (field.anchoredPosition.x > MapWidth) {
				MapWidth = field.anchoredPosition.x;
			}

			if (field.anchoredPosition.y < MapHeight) {
				MapHeight = field.anchoredPosition.y;
			}
		}

		MapHeight = Mathf.Abs (MapHeight);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
