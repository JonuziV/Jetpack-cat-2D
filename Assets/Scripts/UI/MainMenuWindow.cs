using UnityEngine;
using Prototyping.Utils;

public class MainMenuWindow : MonoBehaviour {

	private void  Awake() {
		transform.Find("Play Btn").GetComponent<Button_UI>().ClickFunc = () => { Loader.Load(Loader.Scene.GameScene); };
		transform.Find("Play Btn").GetComponent<Button_UI>().AddButtonSound();
		transform.Find("Quit Btn").GetComponent<Button_UI>().ClickFunc = () => { Application.Quit(); };
		transform.Find("Quit Btn").GetComponent<Button_UI>().AddButtonSound();
	}
}
