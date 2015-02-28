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
		// Initialise Soomla profile module
		SoomlaProfile.Initialize();
		// Compose hashtag of game name for social sharing
		gameHashtag = "#" + Regex.Replace(GameManager.Instance.gameTitle, @"\s+", ""); 
	}

	/* Posts image on Facebook */
	public void PostFacebookScreenshot() {
		StartCoroutine("FacebookScreenshot");
	}
	
	private IEnumerator FacebookScreenshot() {
		yield return new WaitForEndOfFrame();
		// Create a texture the size of the screen, RGB24 format
		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D( width, height, TextureFormat.RGB24, false );
		// Read screen contents into the texture
		tex.ReadPixels( new Rect(0, 0, width, height), 0, 0 );
		tex.Apply();

		SPShareUtility.FacebookShare("I just achieved a score of " + GameManager.Instance.LatestScore + " in " + gameHashtag + ". Can you beat it?", tex);

		Destroy(tex);
	}

	 /* Posts a tweet on Twitter */
	public void PostTwitterScreenshot(){
		StartCoroutine("TwitterScreenshot");
	}

	private IEnumerator TwitterScreenshot() {
		yield return new WaitForEndOfFrame();
		// Create a texture the size of the screen, RGB24 format
		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D( width, height, TextureFormat.RGB24, false );
		// Read screen contents into the texture
		tex.ReadPixels( new Rect(0, 0, width, height), 0, 0 );
		tex.Apply();

		#if UNITY_IPHONE
		SPShareUtility.TwitterShare("I just scored " + GameManager.Instance.LatestScore + " in a game of " + gameHashtag + " on iOS. Can you beat it?", tex);
		#elif
		SPShareUtility.TwitterShare("I just scored " + GameManager.Instance.LatestScore + " in a game of " + gameHashtag + " on Android. Can you beat it?", tex);
		#endif

		Destroy(tex);
	}

	/* Opens the app rating page dependant on platform */
	public void OpenAppRatingPage(){
		SoomlaProfile.OpenAppRatingPage();
	}
	
}
