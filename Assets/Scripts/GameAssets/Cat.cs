using System.Collections.Generic;
using System;
using UnityEngine;

public class Cat : MonoBehaviour {

	private ParticleSystem jetpack;
	private float liftAmount = 25f;
	private int strawberryCount = 0;

	private static Cat instance;
	public static Cat GetInstance(){
		return instance;
	}

	public event EventHandler OnDied;
	public event EventHandler OnStartedPlaying;
	public event EventHandler OnMissionSuccess;
	private Rigidbody2D catRigidBody2D;
	private State state;
	private int catDistance;
	private int missionDistance;
	private Animator animator;
	private Animator birdAnimator;

	private enum State{
		WaitingToStart,
		Playing,
		Dead,
		Win,
	}

	private void Awake(){
		instance = this;
		SetInitialGameState();
		SetAnimationStates();
	}

	private void Start(){
		missionDistance = Level.GetInstance().GetMissionScore();
	}

	private void Update(){
		catDistance = Level.GetInstance().GetCatDistance();
		switch (state){
			default:
			case State.WaitingToStart:	
				if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)){
					WaitingToStartGame();
				}
				break;
			case State.Playing:
				if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)){
					Lift(); 
					if(catDistance > missionDistance){
						state = State.Win;
						if(OnMissionSuccess!=null) OnMissionSuccess(this,EventArgs.Empty);
					}
				}
				break;
			case State.Dead:
				catRigidBody2D.bodyType = RigidbodyType2D.Static;
				break;
			case State.Win:
				catRigidBody2D.bodyType = RigidbodyType2D.Static;
				break;
		}
		
	}

	private void Lift(){
		catRigidBody2D.velocity = Vector2.up * liftAmount ;
		jetpack.Play();//Particles for the jetpack 
		//if (jetpack.isEmitting) {
         	//SoundManager.PlaySound(SoundManager.Sound.CatLift);
		//}
	}

	private void OnTriggerEnter2D(Collider2D collider) {
		if(collider.CompareTag("Strawberry")){
			PickUpStrawberyItem();
			collider.gameObject.SetActive(false);
		} else {
			KillCat();
		}
	}

	public int GetStrawberryItemCount(){
		return strawberryCount;
	}

	private void PickUpStrawberyItem(){
		SoundManager.PlaySound(SoundManager.Sound.StrawberryItemPickup);
			strawberryCount++;
	}

	private void KillCat(){
		var data = new Dictionary <string, object> {{"deathReason", "Collision with Enemy"}};
		Tabtale.Services.LogService.SendEvent("missionFailed", data);
		animator.SetBool("isFlying", false);
		animator.SetBool("isDead", true);
		catRigidBody2D.bodyType = RigidbodyType2D.Static;
		SoundManager.PlaySound(SoundManager.Sound.birdDeath);
		if(OnDied !=null) OnDied(this,EventArgs.Empty);
	}
	private void SetInitialGameState(){
		catRigidBody2D = GetComponent<Rigidbody2D>(); 
		catRigidBody2D.bodyType = RigidbodyType2D.Static;
		state = State.WaitingToStart;
		animator = GetComponent<Animator>();
		jetpack =  GetComponentInChildren<ParticleSystem>();
	}

	private void WaitingToStartGame(){
		animator.SetBool("isFlying", true);
		animator.SetBool("isIdle", false);
		state = State.Playing;
		var data = new Dictionary <string, object> {{"Start", null}};
		Tabtale.Services.LogService.SendEvent("missionStarted", data);
		catRigidBody2D.bodyType = RigidbodyType2D.Dynamic;
		Lift();
		if(OnStartedPlaying != null) OnStartedPlaying(this, EventArgs.Empty);
	}
	private void SetAnimationStates(){
		animator.SetBool("isIdle", true);
		animator.SetBool("isDead", false);
		animator.SetBool("isFlying", false);
	}
}
