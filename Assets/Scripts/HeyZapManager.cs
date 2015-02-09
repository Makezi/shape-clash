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
	}

	// Use this for initialization
	void Start () {
		// Initialize HeyZap
		HeyzapAds.start("b5add26258f6b768fb2a7a643be8c49f", HeyzapAds.FLAG_NO_OPTIONS);
		HeyzapAds.showMediationTestSuite();
	}

	/* Show interstitial ads on game over after set number of games or set score achieved */
	public void ShowInterstitialOnGameOver(){
		gamesCompleted++;
		if(gamesCompleted == showAfterNoGames || GameManager.Instance.LatestScore >= showAfterScore){
			gamesCompleted = 0;
			ShowInterstitial();
		}
	}

	/* Show interstitial ad (whichever ad was requested from mediation network) */
	private void ShowInterstitial(){
		HZInterstitialAd.show();
	}

	

	// public static HeyZapManager instance;

	// void Awake(){
	// 	instance = this;
	// }

	// // Use this for initialization
	// void Start () {
	// 	// Initialize HeyZap
	// 	HeyzapAds.start("b5add26258f6b768fb2a7a643be8c49f", HeyzapAds.FLAG_NO_OPTIONS);
	// }

	// /* Show interstitial ads on game over after set number of games or set score achieved */
	// public void ShowInterstitialOnGameOver(){
	// 	gamesCompleted++;
	// 	if(gamesCompleted == showAfterNoGames || GameManager.Instance.LatestScore >= showAfterScore){
	// 		gamesCompleted = 0;
	// 		ShowInterstitial();
	// 	}
	// }

	// private void ShowInterstitial(){
	// 	HZInterstitialAd.show();
	// }
	
}
