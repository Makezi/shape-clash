using UnityEngine;
using System.Collections;
using Makezi.StateMachine;

public class GameOver : State<GameManager> {

	public override void Enter(){
		// Stop spawning obstacles
		SpawnManager.Instance.Spawn(false);
	}

	public override void Update(){
	}

	public override void FixedUpdate(){

	}

}
