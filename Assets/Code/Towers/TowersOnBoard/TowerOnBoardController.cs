using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class TowerOnBoardController : MonoBehaviour {
	[Header("Animation")]
	public Image TowerImage;
	public Sprite IdleSprite;
	public Sprite[] AttackAnimation;
	int AnimationIdx = 0;
	bool AnimationPlay = true;
	bool ReverseAnimationPlay = true;

	[Range(0.0001f,1.0f)]
	public float AnimInterval = 0.02f;

	float AnimTimer = 0.0f;

	[Header("Projectile")]
	public GameObject ProjectilePrefab;
	bool CanAttack = true;
	// Use this for initialization
	void Start () {
		TowerImage.sprite = IdleSprite;
	}
	
	// Update is called once per frame
	void Update () {
		if (AnimationPlay) {
			if (AnimTimer > 0.0f) {
				AnimTimer -= Time.deltaTime;
			} else {
				AnimationIdx++;

				if (AnimationIdx >= AttackAnimation.Length) {
					AnimationIdx = AttackAnimation.Length - 1;
					AnimationPlay = false;
					ReverseAnimationPlay = true;
				}

				TowerImage.sprite = AttackAnimation [AnimationIdx];

				AnimTimer = AnimInterval;
			}
		} else if (ReverseAnimationPlay) {
			if (AnimTimer > 0.0f) {
				AnimTimer -= Time.deltaTime;
			} else {
				AnimationIdx--;
				
				if (AnimationIdx <= 0) {
					AnimationIdx = 0;
					AnimationPlay = true;
					ReverseAnimationPlay = false;
					TowerImage.sprite = IdleSprite;
				} else {
					TowerImage.sprite = AttackAnimation [AnimationIdx];
				}
				
				AnimTimer = AnimInterval;
			}
		}
	}
}
