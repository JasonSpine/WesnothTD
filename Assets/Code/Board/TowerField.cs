using UnityEngine;
using System.Collections;

public class TowerField : MonoBehaviour {
	SpriteRenderer _SpriteRenderer;
	public bool TowerPlaced = false;

	public TowerController InstantiatedTowerController = null;

	public void ResetField() {
		TowerPlaced = false;
		InstantiatedTowerController = null;
	}

	void OnMouseEnter() {
		_SpriteRenderer.color = Color.yellow;

		MouseTowerPlacement.instance.TowerFieldHighlighted = true;

		if (InstantiatedTowerController != null) {
			InstantiatedTowerController._LineRenderer.enabled = true;
		}

		if (InstantiatedTowerController != null && !MouseTowerPlacement.instance.gameObject.activeSelf && !UpgradeController.instance.isPermanent()) {
			UpgradeController.instance.SetDescriptionValues(InstantiatedTowerController);
			UpgradeController.instance.gameObject.SetActive(true);
		}
	}

	void OnMouseOver() {
		bool NotOverMenu = false;
		if (Input.mousePosition.x < (float)Screen.width - (270.0f * (float)Screen.width / CameraController.instance._CanvasScaler.referenceResolution.x)) {
			NotOverMenu = true;
		}

		if (MouseTowerPlacement.instance.gameObject.activeSelf && !TowerPlaced) {
			MouseTowerPlacement.instance.TowerParent.transform.position = transform.position;

			if (Input.GetMouseButtonDown(0)) {
				if (NotOverMenu) {
					GameObject InstantiatedTower = (GameObject)Instantiate(MouseTowerPlacement.instance.TowerPrefab);
					InstantiatedTower.transform.SetParent(transform);
					InstantiatedTower.transform.localPosition = Vector3.zero;
					TowerPlaced = true;

					InstantiatedTowerController = InstantiatedTower.GetComponent<TowerController>();

					Cash.instance.DecCash(MouseTowerPlacement.instance.TowerPrefab.GetComponent<TowerController>().Price);
					OnMouseExit();
					MouseTowerPlacement.instance.gameObject.SetActive(false);
				}
			}
		} else if (InstantiatedTowerController != null && !MouseTowerPlacement.instance.gameObject.activeSelf) {
			if (Input.GetMouseButtonDown(0)) {
				if (NotOverMenu) {
					_SpriteRenderer.color = Color.red;
					UpgradeController.instance.SetPermanent (true);
					UpgradeController.instance.SetDescriptionValues(InstantiatedTowerController);
					UpgradeController.instance.gameObject.SetActive(true);
				}
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

		if (!UpgradeController.instance.isPermanent()) {
			UpgradeController.instance.gameObject.SetActive (false);
		}

		if (InstantiatedTowerController != null) {
			InstantiatedTowerController._LineRenderer.enabled = false;
		}
	}

	// Use this for initialization
	void Start () {
		_SpriteRenderer = GetComponentInChildren<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
