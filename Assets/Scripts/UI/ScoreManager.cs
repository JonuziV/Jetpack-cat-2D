using UnityEngine;
using UnityEngine.UI;
using Prototyping.Utils;

public class ScoreManager : MonoBehaviour {
	private Text distanceScore;
	private Text strawberrysPickedUp;
	

	private void Awake(){
		distanceScore = transform.Find("Distance").GetComponent<Text>();
		strawberrysPickedUp = transform.Find("Strawberries").GetComponent<Text>();
		transform.Find("Pause Btn").GetComponent<Button_UI>().ClickFunc =  () => { PauseMenu.GetInstance().Show();};
	}

	private void Start(){
		Cat.GetInstance().OnDied += ScoreManager_OnDied;
		Cat.GetInstance().OnMissionSuccess += ScoreManager_OnMissionSuccess;
		
	}
	private void ScoreManager_OnDied(object sender, System.EventArgs e){
		Hide();
	}

	private void ScoreManager_OnMissionSuccess(object sender, System.EventArgs e){
		Hide();
	}

	private void Update(){
		distanceScore.text = Level.GetInstance().GetCatDistance().ToString();
		strawberrysPickedUp.text = Cat.GetInstance().GetStrawberryItemCount().ToString();
		
	}

	private void Hide(){
		gameObject.SetActive(false);
	}
}
