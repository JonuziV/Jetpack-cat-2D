using UnityEngine;
using System;

public class GameAssets : MonoBehaviour {
	
	private static GameAssets INSTANCE;

	public static GameAssets GetInstance(){
		return INSTANCE;
	}

	private void Awake(){
		INSTANCE = this;
	}

	
	public Sprite enemyBirdSprite;
	public Transform enemyBirdPosition;
	
	public Sprite strawberryItemSprite;
	public Transform strawberryItemPosition;

	public Transform cloudBottom;

	public Transform skyCloud;

	public SoundAudioClip[] soundAudioClipArray;

	[Serializable]
	public class SoundAudioClip { 
		public SoundManager.Sound sound;
		public AudioClip audioClip;
	}
}
