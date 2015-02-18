using UnityEngine;
using System;
using System.Collections;
using System.Text.RegularExpressions;
using Soomla;
using Soomla.Profile;
using UnityEngine.UI;

public class SocialManager : MonoBehaviour {

	public static SocialManager Instance { get; private set; }

	private string gameHashtag;					// Hash tag format of game name
	private Provider provider;					// Reference to latest provider logged in
	private bool sharedOnTwitter = false;		// Flag checking status of sharing on twitter after game session
	private bool sharedOnFacebook = false;		// Flag checking status of sharing on facebook after game session

	void Awake(){
		// Check if there are instance conflicts, if so, destroy other instances
		if(Instance != null && Instance != this){
			Destroy(gameObject);
		}
		// Save singleton instance
		Instance = this;
		// Don't destroy between scenes
		DontDestroyOnLoad(gameObject);
	}

	void Start(){
		// Register profile events
		ProfileEvents.OnLoginStarted += LoginStarted;
		ProfileEvents.OnLoginFinished += LoginFinished;
		ProfileEvents.OnSocialActionFinished += SocialActionFinished;
		// Register game state events
		GameManager.Instance.stateMachine.onStateChanged += HandleStateChange;
		// Initialise Soomla profile module
		SoomlaProfile.Initialize();
		// Compose hashtag of game name for social sharing
		gameHashtag = "#" + Regex.Replace(GameManager.Instance.gameTitle, @"\s+", ""); 
	}

	private void LoginStarted(Provider provider, string payload){
		this.provider = provider;
	}

	private void LoginFinished(UserProfile userProfileJson, string payload){
		// Share when the relevant provider has logged in
		if(provider == Provider.FACEBOOK){
			FacebookShare();
		}else if(provider == Provider.TWITTER){
			TwitterShare();
		}
	}

	private void SocialActionFinished(Provider provider, SocialActionType action, string payload){
		if(provider == Provider.FACEBOOK){
			sharedOnFacebook = true;
			Debug.Log("FACEBOOK ACTION COMPLETED");
		}
		if(provider == Provider.TWITTER){
			sharedOnTwitter = true;
			Debug.Log("TWITTER ACTION COMPLETED");
		}
	}

	/* Posts a story on Facebook */
	public void FacebookShare(){
		// Log the user in if not already
		if(!SoomlaProfile.IsLoggedIn(Provider.FACEBOOK)){
			SoomlaProfile.Login(Provider.FACEBOOK);
		}
		
		// Post story on Facebook
		if(SoomlaProfile.IsLoggedIn(Provider.FACEBOOK)){
			SoomlaProfile.UpdateStory(
			Provider.FACEBOOK,
			"Available now on the Apple App Store!",
			"I just achieved a score of " + GameManager.Instance.LatestScore + " in " + gameHashtag + ". Can you beat it?",
			GameManager.Instance.gameTitle,
			"",
			"https://twitter.com/MakeziApps",
			"http://i.imgur.com/BWvNZPG.png",
			"",
			null);
		}
	}

 	/* Posts a tweet on Twitter */
	public void TwitterShare(){
		// Log the user in if not already
		if(!SoomlaProfile.IsLoggedIn(Provider.TWITTER)){
			SoomlaProfile.Login(Provider.TWITTER);
			Debug.Log("LOGGING IN");
		}
		// Post tweet on Twitter (message dependant on platform)
		if(SoomlaProfile.IsLoggedIn(Provider.TWITTER)){
			Debug.Log("POSTING");
			#if UNITY_IPHONE
			SoomlaProfile.UpdateStatus(
				Provider.TWITTER,
				"I just scored " + GameManager.Instance.LatestScore + " in a game of " + gameHashtag + " on iOS. Can you beat it?",
				"",
				null);
			#elif UNITY_ANDROID
			SoomlaProfile.UpdateStatus(
				Provider.TWITTER,
				"I just scored " + GameManager.Instance.LatestScore + " in a game of " + gameHashtag + " on Android. Can you beat it?",
				"",
				null);
			#endif
		}
	}

	/* Opens the app rating page dependant on platform */
	public void OpenAppRatingPage(){
		SoomlaProfile.OpenAppRatingPage();
	}

	private void HandleStateChange(){
		// Reset sharing status of social media
		if(GameManager.Instance.stateMachine.CurrentStateEquals<GameOver>()){
			sharedOnFacebook = false;
			sharedOnTwitter = false;
		}
	}

	public bool SharedOnFacebook {
		get { return sharedOnFacebook; }
	}

	public bool SharedOnTwitter {
		get { return sharedOnTwitter; }
	}
	
}
