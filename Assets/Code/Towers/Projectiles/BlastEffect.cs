using UnityEngine;
using System.Collections;

public class BlastEffect : MonoBehaviour {
	public SpriteRenderer ChangingSprite;
	public Sprite[] AnimationSprites;
	protected int AnimationIdx = 1;
	protected bool AnimationPlay = true;
	[Range(0.0001f,1.0f)]
	public float AnimInterval = 0.02f;
	
	protected float AnimTimer = 0.0f;

	// Use this for initialization
	void Start () {
		ChangingSprite.sprite = AnimationSprites [0];
		AnimTimer = AnimInterval;
	}
	
	// Update is called once per frame
	void Update () {
		if (AnimationPlay) {
			if (AnimTimer > 0.0f) {
				AnimTimer -= Time.deltaTime;
			} else {
				AnimationIdx++;
				
				if (AnimationIdx >= AnimationSprites.Length) {
					AnimationIdx = AnimationSprites.Length - 1;
					AnimationPlay = false;
				}
				
				ChangingSprite.sprite = AnimationSprites [AnimationIdx];
				
				AnimTimer = AnimInterval;
			}
		} else {
			Destroy(gameObject);
		}
	}
}
