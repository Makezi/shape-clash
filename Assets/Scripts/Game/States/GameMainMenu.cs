using UnityEngine;
using System.Collections;
using Makezi.StateMachine;

public class GameMainMenu : State<GameManager> {

	private bool didSpawn;		// Flag to determine if spawner has spawned

	public override void Enter(){
		context.player.gameObject.SetActive(false);
		didSpawn = false;
	}

	public override void Update(){
		// Spawn once reference to spawn manager is not null
		if(!didSpawn && SpawnManager.Instance != null){
			SpawnManager.Instance.Spawn(true);
			didSpawn = true;
		}
	}

	public override void FixedUpdate(){

	}

}
