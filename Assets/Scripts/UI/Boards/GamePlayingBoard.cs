using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GamePlayingBoard : UIBoard {

	public Text scoreText;

	void Awake(){
		if(scoreText != null){
			scoreText.color = new Color(scoreText.color.r, scoreText.color.g, scoreText.color.b, 0);
		}
	}

	protected override void Enter(){
		gameObject.SetActive(true);
		if(scoreText != null){
			UIManager.Instance.FadeText(scoreText, 0.4f, 1.0f);
		}
	}

	public override void Update(){
		UpdateScore();
	}

	/* Retrieves current game session score and update UI */
	private void UpdateScore(){
		if(scoreText != null){ 
			scoreText.text = GameManager.Instance.CurrentScore.ToString(); 
		}
	}

	protected override void Exit(){
		// Fade score UI on state exit
		if(scoreText != null){
			UIManager.Instance.FadeText(scoreText, 0.4f, 0.0f);
		}
		gameObject.SetActive(false);
	}

	protected override void HandleStateChange(){
		DisplayBoard(GameManager.Instance.stateMachine.CurrentStateEquals<GamePlaying>());
	}
}
