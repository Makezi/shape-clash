using UnityEngine;
using System.Collections;

public class HeyZapManager : MonoBehaviour {

	public static HeyZapManager Instance { get; private set; }

	public int showAfterNoGames;			// Show ad after set number of games played
	public int showAfterScore;				// Show ad after set score has been reached

	private int gamesCompleted = 0;			// Counter to the amount of games played so far which is reset after an ad shown

	void Awake(){
		// Check if there are instance conflicts, if so, destroy other instances
		if(Instance != null && Instance != this){
			Destroy(gameObject);
		}
		// Save singleton instance
		Instance = this;
		// Don't destroy between scenes
		DontDestroyOnLoad(gameObject);
		// Initialize HeyZap
		HeyzapAds.start("b5add26258f6b768fb2a7a643be8c49f", HeyzapAds.FLAG_NO_OPTIONS);
		// HeyzapAds.start("b5add26258f6b768fb2a7a643be8c49f", HeyzapAds.FLAG_DISABLE_AUTOMATIC_FETCHING);
		// FetchInterstitial("gameover");
	}

	/* Show ads on awake - currently not working well, too slow to fetch */
	// private void ShowInterstitialOnAwake(){
	// 	if(!PlayerPrefs.HasKey("firstload")){
	// 		PlayerPrefs.SetString("firstload", "true");
	// 	}else{
	// 		ShowInterstitial();
	// 	}
	// }

	/* Show interstitial ads on game over after set number of games or set score achieved */
	public void ShowInterstitialOnGameOver(){
		gamesCompleted++;
		if(gamesCompleted == showAfterNoGames || GameManager.Instance.LatestScore >= showAfterScore){
			HZInterstitialAd.show();
			gamesCompleted = 0;
			// if(HZInterstitialAd.isAvailable("gameover")){
			// 	gamesCompleted = 0;
			// 	ShowInterstitial("gameover");
			// 	FetchInterstitial("gameover");
			// }
		}
	}

	/* Show interstitial ad (whichever ad was requested from mediation network) using tag */
	private void ShowInterstitial(string tag){
		HZInterstitialAd.show(tag);
	}

	/* Fetch ad with specific tag */
	public void FetchInterstitial(string tag){
		HZInterstitialAd.fetch(tag);
	}
	
}
