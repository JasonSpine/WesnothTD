using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {
	[Header("Animation")]
	public bool RotatingProjectile = false;
	public float RotatingProjectileRate = 100.0f;
	public SpriteRenderer ProjectileImage;

	[System.Serializable]
	public class SpritesArray
	{
		public Sprite[] Sprites;
	}

	public SpritesArray[] AnimationsSprites;
	protected SpritesArray AnimationSprites;
	protected int AnimationIdx = 0;
	protected bool AnimationPlay = true;
	[Range(0.0001f,1.0f)]
	public float AnimInterval = 0.02f;
	
	protected float AnimTimer = 0.0f;

	public int Damage;

	public AudioClip[] FireSounds;

	public VictimController VictimToAttack;
	
	public float MoveSpeed;
	public float RotateSpeed;

	public GameObject BlastEffect = null;

	public virtual void InstantiateProjectile(GameObject ProjectilePrefab, VictimController VictimToAttack, Transform TowerTransform, int Damage) {
		if (VictimToAttack == null)
			return;

		GameObject Projectile = (GameObject)Instantiate (ProjectilePrefab);
		Projectile.transform.SetParent (MapParentHandle.instance.gameObject.transform);
		
		Projectile.transform.position = TowerTransform.position;

		ProjectileController _ProjectileController = Projectile.GetComponent<ProjectileController> ();

		_ProjectileController.VictimToAttack = VictimToAttack;
		
		_ProjectileController.Damage = Damage;

		Projectile.transform.localRotation = Quaternion.AngleAxis (
			Mathf.Rad2Deg * Mathf.Atan2(VictimToAttack.transform.position.y - Projectile.transform.position.y,
		                            VictimToAttack.transform.position.x - Projectile.transform.position.x),
			Vector3.forward);

		foreach (ParticleSystem PS in Projectile.GetComponentsInChildren<ParticleSystem>(true)) { // to prevent trail when set start position
			PS.gameObject.SetActive(true);
		}
	}

	// Use this for initialization
	void Start () {
		AnimationSprites = AnimationsSprites [Random.Range (0, (AnimationsSprites.Length))];
		AnimTimer = AnimInterval;
		ProjectileImage.sprite = AnimationSprites.Sprites[AnimationIdx];

		SoundHelper.PlayRandomFromArray (FireSounds);
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		if (VictimToAttack == null) {
			Destroy (gameObject);
			return;
		}

		//rotation
		transform.localRotation = Quaternion.RotateTowards (
		transform.localRotation,
		Quaternion.AngleAxis (Mathf.Rad2Deg * Mathf.Atan2 (VictimToAttack.transform.position.y - transform.position.y,
	                            VictimToAttack.transform.position.x - transform.position.x), Vector3.forward), RotateSpeed * Time.deltaTime
		);

		//self rotation
		if (RotatingProjectile) {
			ProjectileImage.transform.Rotate (new Vector3(0.0f, 0.0f, RotatingProjectileRate * Time.deltaTime));
		}

		// move
		transform.position += transform.right * MoveSpeed;

		// animation
		if (AnimationPlay) {
			if (AnimTimer > 0.0f) {
				AnimTimer -= Time.deltaTime;
			} else {
				AnimationIdx++;
			
				if (AnimationIdx >= AnimationSprites.Sprites.Length) {
					AnimationIdx = AnimationSprites.Sprites.Length - 1;
					AnimationPlay = false;
					AnimTimer = AnimInterval;
					HitVictimAfterAnimation ();

					AnimationPlay = true;
					AnimationIdx = 0;
				}
			
				ProjectileImage.sprite = AnimationSprites.Sprites [AnimationIdx];
			
				AnimTimer = AnimInterval;
			}
		}
	}

	protected virtual void HitVictimAfterAnimation() {

	}

	protected virtual void HitVictimOnCollision(Collider2D other) {
		if (other.gameObject.GetComponent<VictimController>() == VictimToAttack) {
			if (BlastEffect != null) {
				GameObject BE = Instantiate(BlastEffect);
				
				BE.transform.SetParent(MapParentHandle.instance.gameObject.transform);
				BE.transform.position = transform.position;
			}

			VictimToAttack.DecHP(Damage);
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		HitVictimOnCollision (other);
	}
}
