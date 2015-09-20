using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class TowerController : MonoBehaviour {
	[Header("Animation")]
	public SpriteRenderer TowerImage;
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
	[Range(60.0f,1000.0f)]
	public float TowerRange = 160.0f;
	public bool CanAttack = true;
	[Range(0.05f,5.0f)]
	public float AttackInterval = 0.5f;
	public float AttackIntervalTimer = 0.0f;
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

	void OnDrawGizmos() {
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere (transform.position, TowerRange);
	}
}
