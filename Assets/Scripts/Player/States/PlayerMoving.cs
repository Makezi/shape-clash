using UnityEngine;
using System.Collections;
using Makezi.StateMachine;

public class PlayerMoving : State<PlayerController> {

	private float direction;			// Direction to move in
	private bool canMove = true;		// Flag that determines if the player can move

	public override void Enter(){
		direction = -1;
	}

	public override void Update(){
		HandleInput();
	}

	public override void FixedUpdate(){
		Move();
	}

	/* Takes input and links it to actions */
	private void HandleInput(){
		canMove = !Input.GetMouseButton(0);  // HOLD DOWN
		// Toggle directional movement
		if(Input.GetMouseButtonDown(0)){
			direction = -direction;
		}
	}

	/* Forces rigidbody to move in set direction at set current speed */
	private void Move(){
		context.rigidbody2D.velocity = Vector2.zero;
		if(!canMove){ return; }
		context.rigidbody2D.AddForce(new Vector2(0, direction * context.moveSpeed), ForceMode2D.Impulse);
	}

}
