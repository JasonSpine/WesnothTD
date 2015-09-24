using UnityEngine;
using System.Collections;

/// <summary>
/// used to play random sounds from an array
/// </summary>
public class SoundHelper {

	public static void PlayRandomFromArray(AudioClip[] SoundsArray) {
		SoundHelper.PlayRandomFromArray (SoundsArray, 1.0f);
	}

	public static void PlayRandomFromArray(AudioClip[] SoundsArray, float volume) {
		AudioSource.PlayClipAtPoint(SoundsArray[Random.Range(0,SoundsArray.Length)], Vector3.zero, volume);
	}

	public static void PlaySingle(AudioClip clip) {
		SoundHelper.PlaySingle (clip, 1.0f);
	}

	public static void PlaySingle(AudioClip clip, float volume) {
		AudioSource.PlayClipAtPoint(clip, Vector3.zero, volume);
	}
}
