using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class TowerButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public DescriptionController Description;

	public GameObject TowerPrefab;

	public void OnPointerEnter (PointerEventData eventData) 
	{
		Description.gameObject.SetActive (true);
	}

	public void OnPointerExit (PointerEventData eventData) 
	{
		Description.gameObject.SetActive (false);
	}

	// Use this for initialization
	void Start () {
		TowerController _TowerController = TowerPrefab.GetComponent<TowerController> ();
		Description.SetDescriptionValues (_TowerController.Price, _TowerController.Damage);
		Description.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ClickAction() {
		TowerController _TowerController = TowerPrefab.GetComponent<TowerController>();
		MouseTowerPlacement.instance.Initialize (_TowerController);
	}
}
