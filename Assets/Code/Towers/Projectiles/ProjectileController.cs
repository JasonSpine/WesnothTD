using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {
	[Header("Animation")]
	public SpriteRenderer ProjectileImage;
	public Sprite[] AnimationSprites;
	int AnimationIdx = 0;
	bool AnimationPlay = true;
	[Range(0.0001f,1.0f)]
	public float AnimInterval = 0.02f;
	
	float AnimTimer = 0.0f;
	public bool Loop = true;

	public int Damage;
	public VictimController VictimToAttack;

	public virtual void InstantiateProjectile(GameObject ProjectilePrefab, VictimController VictimToAttack, Transform TowerTransform, int Damage) {
		this.VictimToAttack = VictimToAttack;
		GameObject Projectile = (GameObject)Instantiate (ProjectilePrefab);
		Projectile.transform.SetParent (MapParentHandle.instance.gameObject.transform);
		
		Projectile.transform.position = TowerTransform.position;

		this.Damage = Damage;
	}

	// Use this for initialization
	void Start () {
		AnimTimer = AnimInterval;
		ProjectileImage.sprite = AnimationSprites[AnimationIdx];
	}
	
	// Update is called once per frame
	void Update () {
		// animation
		if (AnimationPlay) {
			if (AnimTimer > 0.0f) {
				AnimTimer -= Time.deltaTime;
			} else {
				AnimationIdx++;
				
				if (AnimationIdx >= AnimationSprites.Length) {
					AnimationIdx = AnimationSprites.Length - 1;
					AnimationPlay = false;
					AnimTimer = AnimInterval;
					HitVictimAfterAnimation();
				}
				
				ProjectileImage.sprite = AnimationSprites [AnimationIdx];
				
				AnimTimer = AnimInterval;
			}
		} else if (!Loop) {
			if (AnimTimer > 0.0f) {
				AnimTimer -= Time.deltaTime;
			} else {
				ProjectileImage.sprite = AnimationSprites [AnimationIdx];
				AnimationIdx--;
				
				if (AnimationIdx < 0) {
					AnimationIdx = 0;
					ProjectileImage.sprite = AnimationSprites [AnimationIdx];
					Destroy(gameObject);
				}
				
				AnimTimer = AnimInterval;
			}
		}
	}

	protected virtual void HitVictimAfterAnimation() {

	}

	protected virtual void HitVictimOnCollision() {
		
	}
}
