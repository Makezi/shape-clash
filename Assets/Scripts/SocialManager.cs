using UnityEngine;
using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class SocialManager : MonoBehaviour {

	public static SocialManager Instance { get; private set; }

	public string iosID;
	public string androidID;

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

		#if UNITY_IPHONE
		SPShareUtility.FacebookShare("I just scored " + GameManager.Instance.LatestScore + " in a game of " + gameHashtag + " on iOS. Can you beat it?", tex);
		#elif UNITY_ANDROID
		SPShareUtility.FacebookShare("I just scored " + GameManager.Instance.LatestScore + " in a game of " + gameHashtag + " on Android. Can you beat it?", tex);
		#endif

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
		#elif UNITY_ANDROID
		SPShareUtility.TwitterShare("I just scored " + GameManager.Instance.LatestScore + " in a game of " + gameHashtag + " on Android. Can you beat it?", tex);
		#endif

		Destroy(tex);
	}

	/* Opens the app rating page dependant on platform */
	public void OpenAppRatingPage(){
		#if UNITY_IPHONE
		Application.OpenURL("itms-apps://itunes.apple.com/app/id" + iosID);
		#elif UNITY_ANDROID
		Application.OpenURL("market://details?id=" + androidID);
		#endif
	}
	
}
