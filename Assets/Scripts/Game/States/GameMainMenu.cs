using UnityEngine;
using System.Collections;
using Makezi.StateMachine;

public class GameMainMenu : State<GameManager> {

	public override void Enter(){
		// Debug.Log("MAIN MENU STATE");
	}

	public override void Update(){
		if(Input.GetMouseButtonDown(0)){
			GameManager.Instance.stateMachine.SetState<GameReady>();
		}
	}

	public override void FixedUpdate(){

	}

}
