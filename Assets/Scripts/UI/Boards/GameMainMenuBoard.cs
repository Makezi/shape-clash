using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameMainMenuBoard : UIBoard {

	public Text gameNameLabel;

	new void Start(){
		base.Start();
		if(gameNameLabel != null){
			gameNameLabel.text = GameManager.Instance.gameTitle;
		}	
	}

	protected override void Enter(){
		gameObject.SetActive(true);
	}

	public override void Update(){}

	protected override void Exit(){
		gameObject.SetActive(false);
	}

	protected override void HandleStateChange(){
		DisplayBoard(GameManager.Instance.stateMachine.CurrentStateEquals<GameMainMenu>());
	}

	public void PlayGame(){
		// On fade complete, change game state
		UIManager.Instance.FadeScreen(() => GameManager.Instance.stateMachine.SetState<GameReady>(), 0.4f, 1.0f);
	}

}