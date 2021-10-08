using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototyping;
using Prototyping.Utils;

public class Level : MonoBehaviour {

	private const float CLOUD_BOTTOM_DESTROY_X_POSITION = -68.5f;
	private const float CLOUD_BOTTOM_SPEED = 25f;
	private const float SKY_CLOUD_DESTROY_X_POSITION = -45f;	
	private const float SKY_CLOUD_SPAWN_POSITION = 45f;
	private const float SKY_CLOUD_SPEED = 4f;
	private const float ENEMY_BIRD_SPEED = 20f;
	private const float ENEMY_BIRD_DESTROY_X_POSITION = -40f;	
	private const float ENEMY_BIRD_SPAWN_POSITION = 40f;
	private const float STRAWBERRY_ITEM_SPEED = 20f;
	private const float STRAWBERRY_ITEM_DESTROY_X_POSITION = -40f;
	private const float STRAWBERRY_ITEM_SPAWN_POSITION = 40f; 

	private static Level instance;

	public static Level GetInstance(){
		return instance;
	}

	private List<Transform> cloudBottomList;
	private List<Transform> skyCloudList;
	private float skyCloudSpawnTimer=0f;
	private float skyCloudSpawnTimerMax;
	private List<EnemyBird> enemyBirdList;
	private float enemyBirdSpawnTimer = 1f;
	private float enemyBirdSpawnTimerMax;
	private List<StrawberryItem> strawberryItemList;
	private float strawberryItemSpawnTimer = 2f;
	private float strawberryItemSpawnTimerMax;
	private int catDistance;
	private int missionscore;
	private int maxMissionValue;
	private State state;
	public bool isGamePaused = false;

	public enum Difficulty{
		Easy,
		Normal,
		Hard,
		Impossible,
	}

	private enum State {
		WaitingToStart,
		Playing,
		Dead,
		Win,
	}
	private void Awake(){
		instance = this;
		SpawnInitialCloudBottom();
		SpawnInitialSkyCloud();
		enemyBirdList = new List<EnemyBird>();
		SetDifficulty(Difficulty.Easy);
		strawberryItemList = new List<StrawberryItem>();
		strawberryItemSpawnTimerMax = 5f;
		state = State.WaitingToStart;
		maxMissionValue= Score.GetHighscore();
		missionscore = maxMissionValue + Random.Range(300,maxMissionValue);
	}
	private void Start(){
		//Score.ResetHighscore(); // To reset the highscore for easier testing;
 		Cat.GetInstance().OnDied += Cat_OnDied;
		Cat.GetInstance().OnStartedPlaying += Cat_StartedPlaying;
		Cat.GetInstance().OnMissionSuccess += Cat_Win;
	}

	private void Cat_OnDied(object sender, System.EventArgs e){
		state = State.Dead;
		FunctionTimer.Create(() => {
			Time.timeScale = 0f;
		},1f);
	}

	private void Cat_StartedPlaying(object sender, System.EventArgs e){
		state = State.Playing;
		Time.timeScale = 1;
	}

	public void Cat_Win(object sender, System.EventArgs e){
		state = State.Win;
	}

	private void Update(){
		if(state == State.Playing && !isGamePaused){
			catDistance ++;
			HandleEnemyBirdMovement();
			HandleEnemyBirdSpawning();
			HandleStrawberryItemSpawning();
			HandleStrawberryItemMovement();
			HandleCloudBottom();
			HandleSkyClouds();
		} 
		
		
	}

	//Handles the movement of the EnemyBirds
	private void HandleEnemyBirdMovement(){
		for (int i =0; i<enemyBirdList.Count; i++ ){
			EnemyBird enemyBird = enemyBirdList[i]; 
			enemyBird.Move();
			if(enemyBird.GetXPosition() <ENEMY_BIRD_DESTROY_X_POSITION ){
				//Destory the enemy bird
				enemyBird.DestroySelf();
				enemyBirdList.Remove(enemyBird);
				i--;
			}
		}
	}
	//Handles Enemy bird spawning 
	private void HandleEnemyBirdSpawning(){
		enemyBirdSpawnTimer -= Time.deltaTime;
		if(enemyBirdSpawnTimer < 0){
			enemyBirdSpawnTimer += enemyBirdSpawnTimerMax;
			float maxHeight = -48f;
			float minHeight = 50f;
			float height = Random.Range(minHeight,maxHeight);
			SpawnEnemyBird(height,ENEMY_BIRD_SPAWN_POSITION);
			SetDifficulty(GetDifficulty());
		}
	}	
	//Handles the spawning for the strawberry pickable Items
	private void HandleStrawberryItemSpawning(){
		strawberryItemSpawnTimer -= Time.deltaTime;
		if(strawberryItemSpawnTimer < 0){
			strawberryItemSpawnTimer += strawberryItemSpawnTimerMax;
			float maxHeight = -45f;
			float minHeight = 45f;
			float height = Random.Range(minHeight,maxHeight);
			SpawnStrawberryItem(height,STRAWBERRY_ITEM_SPAWN_POSITION);
		}
	}	

	//Hanldes the movements for the spawned Strawberry Items
	private void HandleStrawberryItemMovement(){
		for (int i =0; i<strawberryItemList.Count; i++ ){
			StrawberryItem strawberryItem = strawberryItemList[i]; 
			strawberryItem.Move();
			if(strawberryItem.GetXPosition() <STRAWBERRY_ITEM_DESTROY_X_POSITION ){
				//Destory the strawberry item
				strawberryItem.DestroySelf();
				strawberryItemList.Remove(strawberryItem);
				i--;
			}
		}
	}
	//Spawns the initial first cloud that appears on the sky when the game starts
	private void SpawnInitialSkyCloud(){
		skyCloudList = new List<Transform>();
		Transform skyCloudTransform;
		float skyCloudY= 25.5f;
		skyCloudTransform = Instantiate(GameAssets.GetInstance().skyCloud, new Vector3(0,skyCloudY,0), Quaternion.identity);
		skyCloudList.Add(skyCloudTransform);
	}

	private void HandleSkyClouds(){
	//Handles sky clouds spawning 
		skyCloudSpawnTimer -= Time.deltaTime;
		if(skyCloudSpawnTimer < 0) {
			//Time to spawn another cloud
			float skyCloudSpawnTimerMax = 25f;
			skyCloudSpawnTimer = skyCloudSpawnTimerMax;
			float skyCloudYmin = 15f;
			float skyCloudYmax = 30f;
			float skyCloudY= Random.Range(skyCloudYmin,skyCloudYmax);
			Transform skyCloudTransform = Instantiate(GameAssets.GetInstance().skyCloud, new Vector3(SKY_CLOUD_SPAWN_POSITION,skyCloudY,0), Quaternion.identity);
			skyCloudList.Add(skyCloudTransform);
		}

		//Handles sky clouds movements
	for (int i=0; i<skyCloudList.Count; i++){
		Transform skyCloudTransform = skyCloudList[i];
			//Move clouds slower for a better parallax effect
			skyCloudTransform.position += new Vector3(-1,0,0) * SKY_CLOUD_SPEED * Time.deltaTime * 0.75f;

			if(skyCloudTransform.position.x < SKY_CLOUD_DESTROY_X_POSITION) {
				//Destroy cloud after certain position
				Destroy(skyCloudTransform.gameObject);
				skyCloudList.RemoveAt(i);
				i--;
			}
		}
		
	}

	//Spawns the initial clouds in the bottom 
	private void SpawnInitialCloudBottom(){
		cloudBottomList = new List<Transform>();
		Transform cloudBottomTransform;
		float cloudBottomY= -46.5f;
		float cloudBottomWidth = 64.45f;
		cloudBottomTransform = Instantiate(GameAssets.GetInstance().cloudBottom, new Vector3(0,cloudBottomY,0), Quaternion.identity);
		cloudBottomList.Add(cloudBottomTransform);
		cloudBottomTransform = Instantiate(GameAssets.GetInstance().cloudBottom, new Vector3(cloudBottomWidth,cloudBottomY,0), Quaternion.identity);
		cloudBottomList.Add(cloudBottomTransform);
		cloudBottomTransform = Instantiate(GameAssets.GetInstance().cloudBottom, new Vector3(cloudBottomWidth * 2f,cloudBottomY,0), Quaternion.identity);
		cloudBottomList.Add(cloudBottomTransform);
	}

	//Hanldes the movement of the clouds in the bottom, moves the one last one in front 
	private void HandleCloudBottom(){
		foreach (Transform cloudBottomTransform in cloudBottomList){
			cloudBottomTransform.position += new Vector3(-1,0,0) * CLOUD_BOTTOM_SPEED * Time.deltaTime;

			if(cloudBottomTransform.position.x < CLOUD_BOTTOM_DESTROY_X_POSITION) {
				//When the bottom cloud passed the left side, relocates on the right
				//Find rightmost X position
				float rightMostXPosition = -97.5f;
				for (int i=0; i<cloudBottomList.Count; i++){
					if(cloudBottomList[i].position.x > rightMostXPosition){
						rightMostXPosition = cloudBottomList[i].position.x;
					}
				}
				// Place Cloud Bottom on the righ most position
				float cloudBottomWidth = 64f;
				cloudBottomTransform.position = new Vector3(rightMostXPosition + cloudBottomWidth, cloudBottomTransform.position.y, cloudBottomTransform.position.z);
			}
		}
	}
	//Handles the sky clouds, lower move speed for better parallax effect
	
	//Used for setting the game difficulty, higher distance > more enemies 
	private void SetDifficulty(Difficulty difficulty){
		switch(difficulty){
			case Difficulty.Easy:
				enemyBirdSpawnTimerMax = 2f;
				break;
			case Difficulty.Normal:
				enemyBirdSpawnTimerMax = 1.5f;
				break;
			case Difficulty.Hard:
				enemyBirdSpawnTimerMax = 1f;
				break;
			case Difficulty.Impossible:
				enemyBirdSpawnTimerMax = 0.75f;
				break;
		}
	}
	//Gets the game difficulty 
	private Difficulty GetDifficulty(){
		if(catDistance >= 5000)return Difficulty.Impossible;
		if(catDistance >= 3000)return Difficulty.Hard;
		if(catDistance >=1000)return Difficulty.Normal;
		return Difficulty.Easy;
	}

	//Used to spawn an EnemyBird
	private void SpawnEnemyBird(float height, float xPosition){
		Transform enemyBirdPosition = Instantiate(GameAssets.GetInstance().enemyBirdPosition);
		enemyBirdPosition.position = new Vector3(xPosition, height);
		EnemyBird enemyBird = new EnemyBird(enemyBirdPosition);
		enemyBirdList.Add(enemyBird);
	}

	//Used to spawn a Strawberry Item
	private void SpawnStrawberryItem(float height, float xPosition){
		Transform strawberryItemPosition = Instantiate(GameAssets.GetInstance().strawberryItemPosition);
		strawberryItemPosition.position = new Vector3(xPosition, height);
		StrawberryItem strawberryItem = new StrawberryItem(strawberryItemPosition);
		strawberryItemList.Add(strawberryItem);
	}

	/*
	* Represents a single EnemyBird
	*/
	private class EnemyBird{
		private Transform enemyBirdTransform;

		public EnemyBird(Transform enemyBirdTransform){
			this.enemyBirdTransform = enemyBirdTransform;
		}

		public void Move(){
			enemyBirdTransform.position += new Vector3(-1,0,0) * ENEMY_BIRD_SPEED * Time.deltaTime;
		}

		public float GetXPosition(){
			return enemyBirdTransform.position.x;
		}

		public void DestroySelf(){
			Destroy(enemyBirdTransform.gameObject);
		}
	}

	/*
	*	Represents a sinlge StrawberyItem that can be pickup in the game 
	*/
	private class StrawberryItem{
		private Transform strawberryItemTransform;

		public StrawberryItem(Transform strawberryItemTransform){
			this.strawberryItemTransform = strawberryItemTransform;
		}

		public void Move(){
			strawberryItemTransform.position += new Vector3(-1,0,0) * STRAWBERRY_ITEM_SPEED * Time.deltaTime;
		}

		public float GetXPosition(){
			return strawberryItemTransform.position.x;
		}

		public void DestroySelf(){
			Destroy(strawberryItemTransform.gameObject);
		}
	}

	//returns the current players distance
	public int GetCatDistance(){
		return catDistance;
	}
	//returns the mission score 
	public int GetMissionScore(){
		return missionscore ;
	}
}
