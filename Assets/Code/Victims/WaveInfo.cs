using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class WaveInfo : MonoBehaviour {
	public Text HpText;
	public Text PrizeText;
	public Image VictimImg;
	public Text CurrentWaveText;

	// Use this for initialization
	void Start () {
		ClearFields ();
	}

	public void ClearFields() {
		VictimImg.gameObject.SetActive (false);
		PrizeText.text = "";
		HpText.text = "";
		if (CurrentWaveText != null) {
			CurrentWaveText.text = "";
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetFields(Sprite VictimSprite, float VictimHP, int VictimPrize, int WaveNumber) {
		if (WaveNumber >= VictimsMainController.instance.WavesCount) {
			ClearFields();
			return;
		}
		VictimImg.gameObject.SetActive (true);
		VictimImg.sprite = VictimSprite;
		VictimImg.rectTransform.sizeDelta = new Vector2(VictimSprite.texture.width, VictimSprite.texture.height);

		PrizeText.text = "$" + VictimPrize.ToString();
		HpText.text = VictimHP.ToString("F0") + " hp";

		if (CurrentWaveText != null) {
			CurrentWaveText.text =
				(WaveNumber + 1).ToString("D2") +
				"/" +
				(VictimsMainController.instance.WavesCount).ToString("D2");
		}
	}
}
