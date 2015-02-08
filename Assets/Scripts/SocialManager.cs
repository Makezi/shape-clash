using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using Soomla;
using Soomla.Profile;
using UnityEngine.UI;

public class SocialManager : MonoBehaviour {

	private string gameHashtag;

	void Start(){
		SoomlaProfile.Initialize();
		gameHashtag = "#" + Regex.Replace(GameManager.Instance.gameTitle, @"\s+", ""); 
	}

	public void FacebookShare(){
		if(!SoomlaProfile.IsLoggedIn(Provider.FACEBOOK)){
			SoomlaProfile.Login(Provider.FACEBOOK);
		}

		if(SoomlaProfile.IsLoggedIn(Provider.FACEBOOK)){
			SoomlaProfile.UpdateStory(
			Provider.FACEBOOK,
			"I achieved a score of " + GameManager.Instance.LatestScore + " in " + gameHashtag + ". Can you beat it?",
			GameManager.Instance.gameTitle,
			"GET IT ON GOOGLE PLAY OR APPLE APP STORE",
			"",
			"www.google.com",
			"http://cdn3.howtogeek.com/wp-content/uploads/gg/up/sshot4f9824f0ea668.jpg",
			"",
			null);
		}
	}



	// private bool sharedOnFacebook;		// Shared on facebook flags during single game session
	// private bool sharedOnTwitter;		// Shared on twitter flag during single game session
	// private string gameTag;				// Game title formated as Twitter hash tag

	// // Use this for initialization
	// void Start () {
	// 	ProfileEvents.OnSocialActionFinished += SocialActionFinished;
	// 	SoomlaProfile.Initialize();
	// 	GameManager.Instance.stateMachine.onStateChanged += HandleStateChange;
	// 	gameTag = "#" + Regex.Replace(GameManager.Instance.gameTitle, @"\s+", "");

	// 	SoomlaProfile.Logout(Provider.TWITTER);
	// }

	// private void SocialActionFinished(Provider provider, SocialActionType action, string payload){
	// 	if(provider == Provider.TWITTER){
	// 		Debug.Log("SHARED ON TWITTER");
	// 		sharedOnTwitter = true;
	// 	}
	// 	if(provider == Provider.FACEBOOK){
	// 		sharedOnFacebook = true;
	// 	}
	// }

	// /* Posts a story on Facebook */ 
	// public void FacebookShare(){
	// 	StopCoroutine("FacebookShareRoutine");
	// 	StartCoroutine("FacebookShareRoutine");
	// }

	// private IEnumerator FacebookShareRoutine(){
	// 	// Return if already shared
	// 	if(sharedOnFacebook){
	// 		yield return null;
	// 	}

	// 	// Attempt to log in if not already logged in
	// 	if(!SoomlaProfile.IsLoggedIn(Provider.FACEBOOK)){
	// 		SoomlaProfile.Login(Provider.FACEBOOK);
	// 	}

	// 	// Give 5 seconds to attempt to log in
	// 	int waitTime = 5;
	// 	while(!SoomlaProfile.IsLoggedIn(Provider.FACEBOOK) && waitTime > 0){
	// 		yield return new WaitForSeconds(1);
	// 		waitTime--;
	// 	}

	// 	// If login failed, return
	// 	if(!SoomlaProfile.IsLoggedIn(Provider.FACEBOOK)){
	// 		yield return null;
	// 	}

	// 	// Post story on users facebook page
	// 	SoomlaProfile.UpdateStory(
	// 		Provider.FACEBOOK,
	// 		"I achieved a score of " + GameManager.Instance.LatestScore + " in " + gameTag + ". Can you beat it?",
	// 		GameManager.Instance.gameTitle,
	// 		"GET IT ON GOOGLE PLAY OR APPLE APP STORE",
	// 		"",
	// 		"www.google.com",
	// 		"http://cdn3.howtogeek.com/wp-content/uploads/gg/up/sshot4f9824f0ea668.jpg",
	// 		"",
	// 		null);
	// }

	// public void TwitterShareTest(){
	// 	if(sharedOnTwitter){
	// 		return;
	// 	}

	// 	if(!SoomlaProfile.IsLoggedIn(Provider.TWITTER)){
	// 		SoomlaProfile.Login(Provider.TWITTER);
	// 	}

	// 	if(!SoomlaProfile.IsLoggedIn(Provider.TWITTER)){
	// 		return;
	// 	}

	// 	#if UNITY_IPHONE
	// 	SoomlaProfile.UpdateStatus(
	// 		Provider.TWITTER,
	// 		"I just scored " + GameManager.Instance.LatestScore + " in a game of " + gameTag + " on iOS. Can you beat it?",
	// 		"",
	// 		null);
	// 	#elif UNITY_ANDROID
	// 	SoomlaProfile.UpdateStatus(
	// 		Provider.TWITTER,
	// 		"I just scored " + GameManager.Instance.LatestScore + " in a game of " + gameTag + " on Android. Can you beat it?",
	// 		"",
	// 		null);
	// 	#endif
	// } 

	// /* Posts a tweet on Twitter */
	// public void TwitterShare(){
	// 	StopCoroutine("TwitterShareRoutine");
	// 	StartCoroutine("TwitterShareRoutine");
	// }

	// private IEnumerator TwitterShareRoutine(){
	// 	// Return if already shared
	// 	if(sharedOnTwitter){
	// 		yield return null;
	// 	}

	// 	// Attempt to log in if not already logged in
	// 	if(!SoomlaProfile.IsLoggedIn(Provider.TWITTER)){
	// 		SoomlaProfile.Login(Provider.TWITTER);
	// 	}

	// 	// Give 5 seconds to attempt to log in
	// 	int waitTime = 5;
	// 	while(!SoomlaProfile.IsLoggedIn(Provider.TWITTER) && waitTime > 0){
	// 		yield return new WaitForSeconds(1);
	// 		waitTime--;
	// 	}

	// 	// If login failed, return
	// 	if(!SoomlaProfile.IsLoggedIn(Provider.TWITTER)){
	// 		yield return null;
	// 	}

	// 	// Post tweet. Message dependant on platform
	// 	#if UNITY_IPHONE
	// 	SoomlaProfile.UpdateStatus(
	// 		Provider.TWITTER,
	// 		"I just scored " + GameManager.Instance.LatestScore + " in a game of " + gameTag + " on iOS. Can you beat it?",
	// 		"",
	// 		null);
	// 	#elif UNITY_ANDROID
	// 	SoomlaProfile.UpdateStatus(
	// 		Provider.TWITTER,
	// 		"I just scored " + GameManager.Instance.LatestScore + " in a game of " + gameTag + " on Android. Can you beat it?",
	// 		"",
	// 		null);
	// 	#endif
	// }

	// /* Opens apps page on the platform store for simple rating */
	// public void OpenAppRatingPage(){
	// 	SoomlaProfile.OpenAppRatingPage();
	// }

	// private void HandleStateChange(){
	// 	/* Reset sharing flags at the end of each game session */
	// 	if(GameManager.Instance.stateMachine.CurrentStateEquals<GameOver>()){
	// 		sharedOnFacebook = false;
	// 		sharedOnTwitter = false;
	// 	}
	// }

	// public bool FacebookShareStatus {
	// 	get { return sharedOnFacebook; }
	// }

	// public bool TwitterShareStatus {
	// 	get { return sharedOnTwitter; }
	// }
	
}
