using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public abstract class UIBoard : MonoBehaviour {

		protected bool isActive = false;		// Display status

		protected void Start(){
			gameObject.SetActive(false);
			GameManager.Instance.stateMachine.onStateChanged += HandleStateChange;
			HandleStateChange();
		}

		/* Set the UI board's active state */
		protected void DisplayBoard(bool status){
			if(status){
				Enter();
				isActive = true;
			}else if(!status && isActive){
				Exit();
				isActive = false;
			}
		}

		/* Initialises UI board with settings values etc. */
		protected abstract void Enter();

		/* UI which is update on the board */
		public abstract void Update();

		/* Deactivates board and resets settings */
		protected abstract void Exit();

		/* Actions to be performed on state transition */
		protected abstract void HandleStateChange();
		
}

