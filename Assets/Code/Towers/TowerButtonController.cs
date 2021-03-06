﻿using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class TowerButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public DescriptionController Description;

	public GameObject TowerPrefab;

	public void OnPointerEnter (PointerEventData eventData) 
	{
		TowerController _TowerController = TowerPrefab.GetComponent<TowerController> ();
		Description.SetDescriptionValues (_TowerController);
		Description.gameObject.SetActive (true);
		if (UpgradeController.instance.isPermanent()) {
			UpgradeController.instance.gameObject.SetActive(false);
		}
	}

	public void OnPointerExit (PointerEventData eventData) 
	{
		Description.gameObject.SetActive (false);
		if (UpgradeController.instance.isPermanent()) {
			UpgradeController.instance.gameObject.SetActive(true);
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ClickAction() {
		TowerController _TowerController = TowerPrefab.GetComponent<TowerController>();
		MouseTowerPlacement.instance.Initialize (_TowerController);
		UpgradeController.instance.SetPermanent(false);
	}
}
