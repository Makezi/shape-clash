using UnityEngine;
using System.Collections;
using Makezi.StateMachine;

public class PlayerIdle : State<PlayerController> {

	public override void Enter(){

	}

	public override void Update(){
		if(GameManager.Instance.stateMachine.CurrentStateEquals<GamePlaying>()){
			stateMachine.SetState<PlayerMoving>();
		}
	}

	public override void FixedUpdate(){

	}

}
