using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Makezi.StateMachine;
using Soomla;
using Soomla.Levelup;

public enum Medal { NONE, BRONZE, SILVER, GOLD, PLATINUM, DIAMOND };

public class GameManager : MonoBehaviour {

	public static GameManager Instance { get; private set; }

	public string gameTitle;							// Game title
	public int targetFrameRate = 60;					// Target frame rate cap
	public PlayerController player;						// Reference to PlayerController
	public StateMachine<GameManager> stateMachine;		// Game state machine				
	public int[] medalBreakpoints;						// Score breakpoints which reward medals

	public delegate void GameHandler();
	public event GameHandler onScore;					// Event triggered when player has scored
	
	private Score score;								// Current session score
	private World mainWorld = new World("main_world");	// Game world
	private Medal medal;								// Medal earned in current session

	void Awake(){
		// Check if there are instance conflicts, if so, destroy other instances
		if(Instance != null && Instance != this){
			Destroy(gameObject);
		}
		// Save singleton instance
		Instance = this;
		// Don't destroy between scenes
		DontDestroyOnLoad(gameObject);

		InitGameStates();
		Application.targetFrameRate = 60;
		// PlayerPrefs.DeleteAll(); // USEFUL LATER ON TO PROVIDE PLAYERS AN OPTION TO RESET SCORES
	}

	/* Adds all states the game state machine. MainMenu state is set as the initial state */
	void InitGameStates(){
		stateMachine = new StateMachine<GameManager>(this, new GameMainMenu());
		stateMachine.AddState(new GameReady());
		stateMachine.AddState(new GamePlaying());
		stateMachine.AddState(new GameOver());
	}

	void Start(){
		score = new Score("score");
		mainWorld.AddScore(score);
		SoomlaLevelUp.Initialize(mainWorld);
	}

	void Update(){
		stateMachine.Update();
	}

	void FixedUpdate(){
		stateMachine.FixedUpdate();
	}

	void OnEnable(){
		PlayerController.onPlayerDeath += GameOver;
	}

	/* Resets latest score obtained, sets current state to GameSummary and show medal earned */
	private void GameOver(){
		MedalEarned();
		// Reset latest score obtained - true means save the score to PlayerPrefs (to determine best scores)
		mainWorld.GetSingleScore().Reset(true);
		GameManager.Instance.stateMachine.SetState<GameOver>();
	}

	/* Determine which medal was earned from score achieved in current game session */
	private void MedalEarned(){
		if(CurrentScore >= medalBreakpoints[0] && CurrentScore < medalBreakpoints[1]){
			medal = Medal.BRONZE;
		}else if(CurrentScore >= medalBreakpoints[1] && CurrentScore < medalBreakpoints[2]){
			medal = Medal.SILVER;
		}else if(CurrentScore >= medalBreakpoints[2] && CurrentScore < medalBreakpoints[3]){
			medal = Medal.GOLD;
		}else if(CurrentScore >= medalBreakpoints[3] && CurrentScore < medalBreakpoints[4]){
			medal = Medal.PLATINUM;
		}else if(CurrentScore >= medalBreakpoints[4]){
			medal = Medal.DIAMOND;
		}else{
			medal = Medal.NONE;
		}
	}

	/* Retrieve current score in session */
	public int CurrentScore {
		get { return (int)mainWorld.GetSingleScore().GetTempScore(); }
		// get { return 0; }
	}

	/* Retrieve latest score achieved in previous completed session */
	public int LatestScore {
		get { return (int)mainWorld.GetSingleScore().Latest; }
		// get { return 0; }
	}

	/* Retrieve best score achieved in all sessions */
	public int BestScore {
		get { 
			return (int)mainWorld.GetSingleScore().Record > 0 ? (int)mainWorld.GetSingleScore().Record : 0;
		}
		// get { return 0; }
	}

	/* Retrieve medal earned */
	public Medal Medal {
		get { return medal; }
	}

	/* Increment current session score by 1 */
	public void IncrementScore(){
		mainWorld.IncSingleScore(1);
		if(onScore != null){ onScore(); }
	}

}
