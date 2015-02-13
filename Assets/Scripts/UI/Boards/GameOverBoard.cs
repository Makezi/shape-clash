using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverBoard : UIBoard {

	public Animator gameOverLabelAnim;
	public Text latestScoreText;		// Reference to latest score text
	public Text bestScoreText;			// Reference to best score text
	public Text twitterText;
	public Text facebookText;
	public Text medalText;				// Temp solution
	public AudioClip gameOverClip;
	// public SocialManager socialManager;
	
	// private Animator anim;				// Reference to UI animator

	void Awake () {
		// anim = GetComponent<Animator>();
	}

	protected override void Enter(){
		// Play transition in animation and fade screen
		UIManager.Instance.overlayPanel.transform.SetAsFirstSibling();
		UIManager.Instance.FadeScreen(0.4f, 0.5f);
		gameObject.SetActive(true);
		// anim.Play("GameSummaryTransitionIn");
		// anim.enabled = true;
		HeyZapManager.Instance.ShowInterstitialOnGameOver();
		AudioManager.Instance.PlayClip(gameOverClip);

		// Invoke("gameOverLabel.Play", "GameOverLabelTransition");
		// gameOverLabelAnim.Play("GameOverLabelTransition");
	}
	
	public override void Update(){
		UpdateScores();
	}

	/* Retrieve latest obtained score and best score and update relevant UI */
	private void UpdateScores(){
		if(latestScoreText != null){ 
			latestScoreText.text = GameManager.Instance.LatestScore.ToString(); 
		}
		if(bestScoreText != null){
			bestScoreText.text = GameManager.Instance.BestScore.ToString();
		}
		// if(twitterText != null){
		// 	if(socialManager.TwitterShareStatus){
		// 		twitterText.text = "TW:S";
		// 	}else{
		// 		twitterText.text = "TW";
		// 	}
		// }
		// if(facebookText != null){
		// 	if(socialManager.FacebookShareStatus){
		// 		facebookText.text = "FB:S";
		// 	}else{
		// 		facebookText.text = "FB";
		// 	}
		// }
		if(medalText != null){
			medalText.text = GameManager.Instance.Medal.ToString();
		}
	}

	protected override void Exit(){
		// anim.Play("GameSummaryTransitionOut");
		gameObject.SetActive(false);
	}

	protected override void HandleStateChange(){
		DisplayBoard(GameManager.Instance.stateMachine.CurrentStateEquals<GameOver>());
	}

	public void ReplayGame(){
		// Make sure overlay panel is infront of all UI elements before fading the screen
		UIManager.Instance.overlayPanel.transform.SetAsLastSibling();
		UIManager.Instance.FadeScreen(() => GameManager.Instance.stateMachine.SetState<GameReady>(), 0.4f, 1.0f);
	}

	public void MainMenu(){
		UIManager.Instance.overlayPanel.transform.SetAsLastSibling();
		UIManager.Instance.FadeScreen(() => GameManager.Instance.stateMachine.SetState<GameMainMenu>(), 0.4f, 1.0f);
	}
}
