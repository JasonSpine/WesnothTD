using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class TowerController : MonoBehaviour {
	[Header("Animation")]
	public SpriteRenderer TowerImage;
	public Sprite IdleSprite;
	public Sprite[] AttackAnimation;
	int AnimationIdx = 0;
	bool AnimationPlay = false;
	bool ReverseAnimationPlay = false;
	bool TowerFlipped = false;

	[Range(0.0001f,1.0f)]
	public float AnimInterval = 0.02f;

	float AnimTimer = 0.0f;

	[Header("Projectile")]
	public GameObject ProjectilePrefab;
	public int Damage = 10;
	public bool InstantiateProjectileAfterAnimation = true;
	VictimController VictimToAttack = null;
	[Range(60.0f,1000.0f)]
	public float TowerRange = 160.0f;
	public bool CanAttack = true;
	[Range(0.05f,5.0f)]
	public float AttackInterval = 0.5f;
	public float AttackIntervalTimer = 0.0f;

	[Header("GUI")]
	public LineRenderer _LineRenderer;
	public int Price;

	// Use this for initialization
	void Start () {
		TowerImage.sprite = IdleSprite;
		_LineRenderer.sortingOrder = 7;
		GenRangeDisplay ();
	}

	void GenRangeDisplay() {
		int VertexCount = 37;
		_LineRenderer.SetVertexCount (VertexCount);
		for (int i = 0; i < VertexCount; i++) {
			_LineRenderer.SetPosition (i, new Vector3(
				TowerRange * Mathf.Cos (Mathf.Deg2Rad * 360.0f * (float)i/(float)(VertexCount - 1)),
				TowerRange * Mathf.Sin (Mathf.Deg2Rad * 360.0f * (float)i/(float)(VertexCount - 1)),
				0.0f));
		}
	}
	
	// Update is called once per frame
	void Update () {
		// animation
		if (AnimationPlay) {
			if (AnimTimer > 0.0f) {
				AnimTimer -= Time.deltaTime;
			} else {
				AnimationIdx++;

				if (AnimationIdx >= AttackAnimation.Length) {
					AnimationIdx = AttackAnimation.Length - 1;
					AnimationPlay = false;
					ReverseAnimationPlay = true;

					if (InstantiateProjectileAfterAnimation) {
						InstantiateProjectile(VictimToAttack);
					}
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
					//AnimationPlay = true;
					ReverseAnimationPlay = false;
					TowerImage.sprite = IdleSprite;
				} else {
					TowerImage.sprite = AttackAnimation [AnimationIdx];
				}
				
				AnimTimer = AnimInterval;
			}
		}

		//Attack cooldown
		if (!CanAttack) {
			if (AttackIntervalTimer > 0.0f) {
				AttackIntervalTimer -= Time.deltaTime;
			} else {
				CanAttack = true;
			}
		}
	}

	public void Attack(VictimController VictimToAttack) {
		this.VictimToAttack = VictimToAttack;
		FlipToVictim (VictimToAttack.transform.position.x);

		CanAttack = false;
		AttackIntervalTimer = AttackInterval;
		AnimationPlay = true;

		if (!InstantiateProjectileAfterAnimation) {
			InstantiateProjectile(VictimToAttack);
		}
	}

	void InstantiateProjectile(VictimController VictimToAttack) {
		ProjectilePrefab.GetComponent<ProjectileController> ().InstantiateProjectile (ProjectilePrefab, VictimToAttack, transform);
	}

	void FlipToVictim(float VictimPosX) {
		if (VictimPosX > transform.position.x) {
			TowerFlipped = false;
			transform.localRotation = Quaternion.AngleAxis(0.0f, Vector3.up);
		} else {
			TowerFlipped = true;
			transform.localRotation = Quaternion.AngleAxis(180.0f, Vector3.up);
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere (transform.position, TowerRange);
	}
}
