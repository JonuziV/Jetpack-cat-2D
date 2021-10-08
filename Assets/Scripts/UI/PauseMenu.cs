using UnityEngine;
using Prototyping.Utils;

public class PauseMenu : MonoBehaviour {

	private static PauseMenu instance;

	public static PauseMenu GetInstance(){
		return instance;
	}

	private void Awake(){
		instance = this;
		transform.Find("Continue Btn").GetComponent<Button_UI>().ClickFunc =  () => { Hide(); /*Tabtale.Services.AdService.Instance.Show(); */};
		transform.Find("Continue Btn").GetComponent<Button_UI>().AddButtonSound();
		transform.Find("Menu Btn").GetComponent<Button_UI>().ClickFunc =  () => { Loader.Load(Loader.Scene.MainMenu); };
		transform.Find("Menu Btn").GetComponent<Button_UI>().AddButtonSound();

	}

	private void Start() {
		Hide();	
	}

	private void Hide(){
		Level.GetInstance().isGamePaused = false;
		Time.timeScale = 1;
		AudioListener.pause = false;
		gameObject.SetActive(false);
	}

	public void Show(){
		Level.GetInstance().isGamePaused = true;
		Time.timeScale = 0;
		AudioListener.pause = true;
		gameObject.SetActive(true);
		/*
		*	Set Ad to show on Pause Btn click, after game get's paused 
			Can be set-up to work pretty much anywhere, example on Continue Btn
		*/
		Tabtale.Services.AdService.Instance.Show();
	}
}
