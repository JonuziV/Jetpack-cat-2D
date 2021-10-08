using UnityEngine;
using UnityEngine.UI;

public class WaitingToStartWindow : MonoBehaviour {


	private Text missionScore;

	private void Awake(){
		missionScore = transform.Find("missionScoreText").GetComponent<Text>();
	}


	private void Start(){
		missionScore.text = Level.GetInstance().GetMissionScore().ToString();
		Cat.GetInstance().OnStartedPlaying += WaitingToStartWindow_OnStartedPlaying;
	}

	private void WaitingToStartWindow_OnStartedPlaying (object sender, System.EventArgs e){
		Hide();
	}

	private void Hide(){
		gameObject.SetActive(false);
	}

	private void Show(){
		gameObject.SetActive(true);
	}
}
