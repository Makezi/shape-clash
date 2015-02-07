using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameReadyBoard : UIBoard {

	public Text bestScoreText;
	public Text instructionText;

	protected override void Enter(){
		gameObject.SetActive(true);
		UIManager.Instance.FadeScreen(0.4f, 0.0f);
	}

	public override void Update(){
		if(Input.GetMouseButton(0)){
			GameManager.Instance.stateMachine.SetState<GamePlaying>();
		}
		UpdateBestScore();
	}

	/* Retrieve best game score overall */
	private void UpdateBestScore(){
		if(bestScoreText != null){
			bestScoreText.text = GameManager.Instance.BestScore.ToString();
		}
	}

	protected override void Exit(){
		gameObject.SetActive(false);
	}

	protected override void HandleStateChange(){
		DisplayBoard(GameManager.Instance.stateMachine.CurrentStateEquals<GameReady>());
	}

}
