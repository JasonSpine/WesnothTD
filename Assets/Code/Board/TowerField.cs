using UnityEngine;
using System.Collections;

public class TowerField : MonoBehaviour {
	SpriteRenderer _SpriteRenderer;
	public bool TowerPlaced = false;

	void OnMouseEnter() {
		_SpriteRenderer.color = Color.yellow;

		MouseTowerPlacement.instance.TowerFieldHighlighted = true;
	}

	void OnMouseOver() {
		if (MouseTowerPlacement.instance.gameObject.activeSelf && !TowerPlaced) {
			MouseTowerPlacement.instance.TowerParent.transform.position = transform.position;

			if (Input.GetMouseButtonDown(0)) {
				GameObject InstantiatedTower = (GameObject)Instantiate(MouseTowerPlacement.instance.TowerPrefab);
				InstantiatedTower.transform.SetParent(transform);
				InstantiatedTower.transform.localPosition = Vector3.zero;
				TowerPlaced = true;

				Cash.instance.DecCash(MouseTowerPlacement.instance.TowerPrefab.GetComponent<TowerController>().Price);
				OnMouseExit();
				MouseTowerPlacement.instance.gameObject.SetActive(false);
			}
		} else {
			_SpriteRenderer.color = Color.white;
			MouseTowerPlacement.instance.TowerFieldHighlighted = false;
		}
	}

	void OnMouseExit() {
		_SpriteRenderer.color = Color.white;
		MouseTowerPlacement.instance.TowerParent.transform.localPosition = Vector3.zero;

		MouseTowerPlacement.instance.TowerFieldHighlighted = false;
	}

	// Use this for initialization
	void Start () {
		_SpriteRenderer = GetComponentInChildren<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
