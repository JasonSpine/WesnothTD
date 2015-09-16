using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class TowerButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public int Price = 100;

	public DescriptionController Description;

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
		Description.SetDescriptionValues (Price, 1111);
		Description.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
