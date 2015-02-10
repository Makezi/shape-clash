using UnityEngine;
using System.Collections;
using Makezi.StateMachine;
using Makezi.ObjectPool;

public class PlayerDead : State<PlayerController> {

	private CameraShake cameraShake;	// Reference to CameraShake

	public override void Enter(){
		// Halt player movement
		context.rigidbody2D.velocity = Vector2.zero;
		// Shake the camera on player death
		if(cameraShake == null){
			cameraShake = GameObject.FindObjectOfType<CameraShake>();
		}
		cameraShake.Shake(2.0f);
	}

	public override void Update(){
	}

	public override void FixedUpdate(){

	}

}
