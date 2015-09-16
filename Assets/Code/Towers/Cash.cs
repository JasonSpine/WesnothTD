using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class Cash : MonoBehaviour {
	[Header("Cash")]
	public Text CashAmount;
	int CashValue;
	public int InitialCashValue = 500;

	[Header("Lives")]
	public Text LivesAmount;
	public int LivesValue = 20;

	[Header("Technical")]
	public GameObject TowerButtonsParent;
	TowerButtonController[] TowerButtonControllers;
	Button[] TowerButtons;

	public static Cash instance;

	void Awake() {
		instance = this;
	}

	public void SetCash(int c) {
		CashValue = c;
		CashAmount.text = "$" + CashValue.ToString();

		UpdateTowersAvailability ();
	}

	public void SetLives(int l) {
		LivesValue = l;
		LivesAmount.text = LivesValue.ToString();
	}

	public void IncCash(int c) {
		SetCash (CashValue + c);
	}

	public void IncLives(int l) {
		SetLives (LivesValue + l);
	}

	public void DecCash(int c) {
		if (CashValue - c >= 0) {
			SetCash (CashValue - c);
		} else {
			SetCash(0);
		}
	}

	public void DecLives(int l) {
		if (LivesValue - l >= 0) {
			SetLives (LivesValue - l);
		} else {
			SetLives(0);
			//dead!!!
		}
	}

	// Use this for initialization
	void Start () {
		TowerButtonControllers = TowerButtonsParent.GetComponentsInChildren<TowerButtonController> ();
		TowerButtons = TowerButtonsParent.GetComponentsInChildren<Button> ();

		SetCash (InitialCashValue);
		SetLives (LivesValue);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateTowersAvailability() {
		for (int i = 0; i < TowerButtonControllers.Length; i++) {
			if (TowerButtonControllers[i].Price > CashValue) {
				TowerButtons[i].interactable = false;
			} else { 
				TowerButtons[i].interactable = true;
			}
		}
	}
}
