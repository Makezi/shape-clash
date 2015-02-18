using UnityEngine;
using System.Collections;
using Makezi.StateMachine;

public class GameReady : State<GameManager> {

	public override void Enter(){
		context.player.gameObject.SetActive(true);
		context.player.Init();
		SpawnManager.Instance.Spawn(false);
	}

	public override void Update(){
		if(Input.GetMouseButtonDown(0)){
			GameManager.Instance.stateMachine.SetState<GamePlaying>();
		}
	}

	public override void FixedUpdate(){

	}
}
