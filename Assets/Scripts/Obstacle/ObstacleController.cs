using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class ObstacleController : MonoBehaviour {

	public float startingSpeed;			// Initial movement speed
	public float maxSpeed;				// Maximum movement speed
	public float speedIncrement;		// Amount to increase speed by
	public float direction;				// Direction to move in

	private Shape shape;				// Current shape set
	private PlayerController player;	// Reference to PlayerController
	private Rigidbody2D myRigidbody2D;	// Reference to this object's rigidbody2D component
	public float currentSpeed;			// Current movement speed

	void Awake(){
		myRigidbody2D = rigidbody2D;
	}

	// Use this for initialization
	void Start () {
		GameManager.Instance.stateMachine.onStateChanged += HandleStateChange;
		GameManager.Instance.onScore += IncreaseSpeed;
		currentSpeed = startingSpeed;
	}

	void FixedUpdate(){
		Move();
	}

	/* Forces rigidbody to move in set direction at set current speed */
	private void Move(){
		myRigidbody2D.velocity = Vector2.zero;
		myRigidbody2D.AddForce(new Vector2(direction * currentSpeed, 0), ForceMode2D.Impulse);
	}

	/* If a collision occurs, this obstacle is destroyed. In case of player collision, 
	a comparison is made between this obstacle's shape and the player's to determine an
	outcome */
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("Player")){
			if(player == null){
				player = other.transform.parent.GetComponent<PlayerController>();
			}
			if(player.Shape.GetShape() == shape.GetShape()){
				// Increment game score if player shape and obstacle shape compare
				GameManager.Instance.IncrementScore();
			}else{
				player.Destroy();
			}
		}
		Destroy();
	}

	/* Increase speed by increment value */
	private void IncreaseSpeed(){
		currentSpeed += speedIncrement;
		if(currentSpeed >= maxSpeed){
			currentSpeed = maxSpeed;
		}
	}

	private void HandleStateChange(){
		// Set game object inactive and reset current speed
		if(GameManager.Instance.stateMachine.CurrentStateEquals<GameReady>()){
			currentSpeed = startingSpeed;
			Destroy();
		}
	}

	private void Destroy(){
		gameObject.SetActive(false);
	}

	public Shape Shape {
		get { 
			if(shape == null){
				shape = GetComponentInChildren<Shape>();
			}
			return shape; 
		}
	}

}
