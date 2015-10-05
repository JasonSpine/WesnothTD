using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class UpgradeController : MonoBehaviour {
	public Text TowerNameText;
	public Text DescriptionValuesText;
	public Text UpgradeDescriptionValuesText;
	public Text TowerLevelValueText;
	public Text TowerAIValueText;
	int UpgradeCost;
	int SellRefound;
	float UpgradeDamage;
	float UpgradeRange;
	public Text TowerUpgradePrice;
	public static UpgradeController instance;
	public bool Permanent = false;

	public TowerController SelectedTower;

	public Button UpgradeButton;
	public Text SellTowerValueText;


	public void SetDescriptionValues(TowerController _TowerController) {
		SelectedTower = _TowerController;

		UpgradeRange = _TowerController.TowerRange * 1.07f;
		UpgradeCost = _TowerController.Price / 2;
		UpgradeDamage = _TowerController.Damage * 1.5f;

		SellRefound = (int)((float)(((_TowerController.TowerLevel - 1) * UpgradeCost) + // upgrades
			_TowerController.Price) * 0.75f); // base price


		TowerNameText.text = _TowerController.TowerName;

		TowerLevelValueText.text = _TowerController.TowerLevel.ToString ();

		UpgradeDescriptionValuesText.text =
				UpgradeDamage.ToString ("F0") + "\n"
				+ UpgradeRange.ToString ("F0");

		DescriptionValuesText.text =
				_TowerController.Damage.ToString ("F0") + "\n"
				+ _TowerController.TowerRange.ToString ("F0");

		TowerUpgradePrice.text = "(price: " + UpgradeCost.ToString () + ")";

		TowerAIValueText.text = AIBehaviorToString(_TowerController.TowerAIBehavior);

		SellTowerValueText.text = "for: $" + SellRefound;

		if (SelectedTower.TowerLevel >= 10) {
			UpgradeButton.interactable = false;
			TowerUpgradePrice.color = Color.cyan;
			TowerUpgradePrice.text = "Can't upgrade";
		} else {
			if (Cash.instance.CashValue < UpgradeCost) {
				TowerUpgradePrice.color = Color.red;
				UpgradeButton.interactable = false;
			} else {
				TowerUpgradePrice.color = Color.green;
				UpgradeButton.interactable = true;
			}
		}
	}

	string AIBehaviorToString(TowerController.AIBehavior ai) {
		switch (ai) {
		case TowerController.AIBehavior.CLOSEST:
			return "near";
		case TowerController.AIBehavior.FURTHEST:
			return "far";
		case TowerController.AIBehavior.SLOWEST:
			return "slow";
		case TowerController.AIBehavior.FASTEST:
			return "fast";
		case TowerController.AIBehavior.LESS_HP:
			return "low hp";
		case TowerController.AIBehavior.MORE_HP:
			return "high hp";
		case TowerController.AIBehavior.LESS_POISONED:
			return "less poison";
		case TowerController.AIBehavior.MORE_POISONED:
			return "more poison";
		}

		return "NOAI";
	}

	// Use this for initialization
	void Start () {
		instance = this;
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown && !Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1)) {
			gameObject.SetActive(false);
			Permanent = false;
		}

		if (Permanent) {
			if (SelectedTower != null) {
				if (!SelectedTower._LineRenderer.enabled) {
					SelectedTower._LineRenderer.enabled = true;
				}
			}
		}
	}

	public void ChangeAIButtonAction() {
		var AIValues = System.Enum.GetValues(typeof(TowerController.AIBehavior));
		int AIValue = (int)SelectedTower.TowerAIBehavior;
		AIValue ++;
		if (AIValue >= AIValues.Length) {
			AIValue = 0;
		}

		SelectedTower.TowerAIBehavior = (TowerController.AIBehavior)AIValue;

		SetDescriptionValues (SelectedTower);
	}

	public void UpgradeButtonAction() {
		Cash.instance.DecCash (UpgradeCost);
		SelectedTower.TowerRange = UpgradeRange;
		SelectedTower.Damage = UpgradeDamage;
		SelectedTower.TowerLevel++;

		SelectedTower.PoisonStrength *= 1.2f;

		SelectedTower.GenRangeDisplay ();

		SetDescriptionValues (SelectedTower);
	}

	public void SellButtonAction() {
		SetPermanent (false);

		Cash.instance.IncCash (SellRefound);

		SelectedTower.GetComponentInParent<TowerField> ().ResetField();
		gameObject.SetActive (false);

		Destroy (SelectedTower.gameObject);
	}

	public void SetPermanent(bool p) {
		if (p) {
			if (SelectedTower != null) {
				SelectedTower._LineRenderer.enabled = false;
			}
			Permanent = true;
		} else {
			Permanent = false;
			if (SelectedTower != null) {
				SelectedTower._LineRenderer.enabled = false;
			}
		}
	}

	public bool isPermanent() {
		return gameObject.activeSelf && Permanent;
	}
}
