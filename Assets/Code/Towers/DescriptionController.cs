using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class DescriptionController : MonoBehaviour {
	public Text TowerNameText;
	public Text DescriptionValuesText;
	public Text ProjectileAbilityText;
	public Text MoreDamageToText;
	public Text LessDamageToText;

	public void SetDescriptionValues(TowerController _TowerController) {
		TowerNameText.text = _TowerController.TowerName;
		ProjectileController _ProjectileController = _TowerController.ProjectilePrefab.GetComponent<ProjectileController>();

		MoreDamageToText.text = "+50% damage to " + VictimTypeToString(_ProjectileController.StrongerAgainst);
		LessDamageToText.text = "-50% damage to " + VictimTypeToString(_ProjectileController.WeakerAgainst);

		ProjectileAbilityText.text = _ProjectileController.ProjectileAbility;

		DescriptionValuesText.text =
			"$" + _TowerController.Price.ToString () + "\n"
				+ _TowerController.Damage.ToString ("F0");

	}

	string VictimTypeToString(VictimsMainController.VictimEnumID VictimType) {
		switch (VictimType) {
		case VictimsMainController.VictimEnumID.DRAKE:
			return "drakes";
		case VictimsMainController.VictimEnumID.LIZARD:
			return "lizards";
		case VictimsMainController.VictimEnumID.LOYALIST:
			return "loyalists";
		case VictimsMainController.VictimEnumID.TROLL:
			return "trolls";
		case VictimsMainController.VictimEnumID.UNDEAD:
			return "undeads";
		case VictimsMainController.VictimEnumID.WOLF:
			return "wolf riders";
		}

		return "";
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
