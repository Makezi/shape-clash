using UnityEngine;
using System.Collections;
using Makezi.StateMachine;

public class GameReady : State<GameManager> {

	public override void Enter(){
		context.player.Init();
	}

	public override void Update(){

	}

	public override void FixedUpdate(){

	}
}
