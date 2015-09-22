﻿using UnityEngine;
using System.Collections;

public class MouseTowerPlacement : MonoBehaviour {
	public static MouseTowerPlacement instance;
	public SpriteRenderer TowerSprite;
	public LineRenderer _LineRenderer;
	public GameObject TowerParent;

	void GenRangeDisplay(float TowerRange) {
		int VertexCount = 37;
		_LineRenderer.SetVertexCount (VertexCount);
		for (int i = 0; i < VertexCount; i++) {
			_LineRenderer.SetPosition (i, new Vector3(
				TowerRange * Mathf.Cos (Mathf.Deg2Rad * 360.0f * (float)i/(float)(VertexCount - 1)),
				TowerRange * Mathf.Sin (Mathf.Deg2Rad * 360.0f * (float)i/(float)(VertexCount - 1)),
				0.0f));
		}
	}

	public void Initialize(TowerController _TowerController) {
		GenRangeDisplay (_TowerController.TowerRange);
		TowerSprite.sprite = _TowerController.TowerImage.sprite;

		Update ();

		gameObject.SetActive (true);
	}

	void Awake() {
		_LineRenderer.sortingOrder = 8;
		instance = this;

		gameObject.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		transform.position = (Vector2)Camera.main.ScreenToWorldPoint (Input.mousePosition);

		if (Input.anyKeyDown) {
			gameObject.SetActive(false);
		}
	}
}
