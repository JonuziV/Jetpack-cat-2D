using UnityEngine;
using Prototyping.Utils;

public static class SoundManager{

	public enum Sound{
		CatLift,
		birdDeath,
		StrawberryItemPickup,
		Highscore,
		ButtonClick,
	}
	public static void PlaySound(Sound sound){
		GameObject gameObject = new GameObject("Sound", typeof(AudioSource));
		AudioSource audioSource = gameObject.GetComponent<AudioSource>();
		audioSource.PlayOneShot(GetAudioClip(sound));
	}

	private static AudioClip GetAudioClip(Sound sound){
		foreach(GameAssets.SoundAudioClip soundAudioClip in GameAssets.GetInstance().soundAudioClipArray){
			if (soundAudioClip.sound == sound){
				return soundAudioClip.audioClip;
			}
		}
		Debug.LogError("Sound" + sound + " not found!");
		return null;
	}

	public static void AddButtonSound(this Button_UI button_UI){
		button_UI.ClickFunc += () => PlaySound(Sound.ButtonClick);
	}
}
