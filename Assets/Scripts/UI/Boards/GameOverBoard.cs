using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameOverBoard : UIBoard {

	public Text latestScoreText;		// Reference to latest score text
	public Text bestScoreText;			// Reference to best score text
	public Image twitterTick;			// Reference to tick icon for twitter share status
	public Image facebookTick;			// Reference to tick icon for facebook share status
	public Image medalIcon;				// Reference to medal earned icon
	public Sprite noMedalSprite;		// Reference to the no medal sprite
	public List<Sprite> medalSprites;	// Reference to list of medal sprites
	public AudioClip medalEarnedClip;	// Reference to the medal earned audio clipped played on earning of medal
	
	private Animator anim;				// Reference to UI animator

	void Awake () {
		anim = GetComponent<Animator>();
	}

	protected override void Enter(){
		// Play transition in animation and fade screen
		UIManager.Instance.overlayPanel.transform.SetAsFirstSibling();
		UIManager.Instance.FadeScreen(0.4f, 0.5f);
		gameObject.SetActive(true);
		anim.Play("GameOverTransition");
		anim.enabled = true;
		HeyZapManager.Instance.ShowInterstitialOnGameOver();
		// Update medal icon
		medalIcon.sprite = GetMedalSprite(GameManager.Instance.Medal.ToString());
	}

	/* Retrieves medal sprite from list of available medals when comparison is made to medal earned */
	private Sprite GetMedalSprite(string name){
		for(int i = 0; i < medalSprites.Count; ++i){
			string lower = medalSprites[i].name.ToLower();
			if(lower.Contains(name.ToLower())){
				// Play clip if medal found
				AudioManager.Instance.PlayClip(medalEarnedClip);
				return medalSprites[i];
			}
		}
		return noMedalSprite;
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
		if(twitterTick != null){
			twitterTick.enabled = SocialManager.Instance.SharedOnTwitter;
		}
		if(facebookTick != null){
			facebookTick.enabled = SocialManager.Instance.SharedOnFacebook;
		}



		// if(medalText != null){
		// 	medalText.text = GameManager.Instance.Medal.ToString();
		// }
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
