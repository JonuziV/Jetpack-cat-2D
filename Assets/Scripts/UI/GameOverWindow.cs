using UnityEngine;
using UnityEngine.UI;
using Prototyping.Utils;
public class GameOverWindow : MonoBehaviour {

	private Text scoreText;
	private Text strawberryCountText;
	private Text highscoreText;
	private Text missionScore;

	private void Awake(){
		GetReferences();
		transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
	}

	private void Start() {
		 Cat.GetInstance().OnDied += Cat_OnDied;
		 Hide();
	}

	private void Cat_OnDied(object sender, System.EventArgs e){
		scoreText.text = Level.GetInstance().GetCatDistance().ToString();
		strawberryCountText.text = Cat.GetInstance().GetStrawberryItemCount().ToString();
		highscoreText.text =Score.GetHighscore().ToString();
		missionScore.text = Level.GetInstance().GetMissionScore().ToString();
		if(Level.GetInstance().GetCatDistance() >= Score.GetHighscore()){
			highscoreText.text = Score.GetHighscore().ToString();
		} 
		Show();
	}

	private void GetReferences(){
		scoreText = transform.Find("scoreText").GetComponent<Text>();
		strawberryCountText = transform.Find("strawberryCountText").GetComponent<Text>();
		highscoreText = transform.Find("highscoreText").GetComponent<Text>();
		missionScore = transform.Find("missionScoreText").GetComponent<Text>();
		transform.Find("Retry Btn").GetComponent<Button_UI>().ClickFunc =  () => { Loader.Load(Loader.Scene.GameScene); };
		transform.Find("Retry Btn").GetComponent<Button_UI>().AddButtonSound();
		transform.Find("Menu Btn").GetComponent<Button_UI>().ClickFunc =  () => { Loader.Load(Loader.Scene.MainMenu); };
		transform.Find("Menu Btn").GetComponent<Button_UI>().AddButtonSound();
	}

	private void Hide(){
		gameObject.SetActive(false);
	}

	private void Show(){
		gameObject.SetActive(true);
	}
	
}
