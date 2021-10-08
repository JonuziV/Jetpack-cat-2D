using UnityEngine;
using UnityEngine.UI;
using Prototyping.Utils;
using System.Collections.Generic;

public class WinWindow : MonoBehaviour {

	private Text missionScore;

	private void Awake() {
		transform.Find("Play Btn").GetComponent<Button_UI>().ClickFunc =  () => { Loader.Load(Loader.Scene.GameScene); };
		transform.Find("Play Btn").GetComponent<Button_UI>().AddButtonSound();
		transform.Find("Menu Btn").GetComponent<Button_UI>().ClickFunc =  () => { Loader.Load(Loader.Scene.MainMenu); };
		transform.Find("Menu Btn").GetComponent<Button_UI>().AddButtonSound();

		missionScore = transform.Find("missionScoreText").GetComponent<Text>();
	}

	// Use this for initialization
	void Start () {
		Cat.GetInstance().OnMissionSuccess += WinWindow_OnMissionSuccess;
		Hide();
	}

	private void WinWindow_OnMissionSuccess (object sender, System.EventArgs e){
		Score.TrySetNewHighscore(Level.GetInstance().GetCatDistance());
		missionScore.text = Score.GetHighscore().ToString();
		SoundManager.PlaySound(SoundManager.Sound.Highscore);
		GetMissionData();
		Time.timeScale = 0;
		Show();
	}

	private void Hide(){
		gameObject.SetActive(false);
	}

	private void Show(){
		gameObject.SetActive(true);
	}

	private void GetMissionData(){
		var data1 = new Dictionary <string, object> {{"distance", Score.GetHighscore()}};
		var data2 = new Dictionary <string, object> {{"berryCount", Cat.GetInstance().GetStrawberryItemCount()}};
		Tabtale.Services.LogService.SendEvent("missionCompleted", data1);
		Tabtale.Services.LogService.SendEvent("missionCompleted", data2);
	}
}
