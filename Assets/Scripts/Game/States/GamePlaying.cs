using UnityEngine;
using System.Collections;
using Makezi.StateMachine;

public class GamePlaying : State<GameManager> {

	public override void Enter(){
		// Start spawning obstacles
		SpawnManager.Instance.Spawn(true);
	}

	public override void Update(){
		
	}

	public override void FixedUpdate(){

	}
	
}
