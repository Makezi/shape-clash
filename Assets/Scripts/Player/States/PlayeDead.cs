using UnityEngine;
using System.Collections;
using Makezi.StateMachine;
using Makezi.ObjectPool;

public class PlayerDead : State<PlayerController> {

	public override void Enter(){
		context.rigidbody2D.velocity = Vector2.zero;
	}

	public override void Update(){
	}

	public override void FixedUpdate(){

	}

}
