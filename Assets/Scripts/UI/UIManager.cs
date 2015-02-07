using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {

	public Image overlayPanel;								// Reference to overlay panel
	public List<UIBoard> uiBoards = new List<UIBoard>();	// Reference to all UIBoards

	private static UIManager instance;						// Singleton instance

	public static UIManager Instance {
		get {
			if(instance == null){
				instance = GameObject.FindObjectOfType<UIManager>();
				DontDestroyOnLoad(instance.gameObject);
			}
			return instance;
		}
	}

	void Awake(){
		if(instance == null){
			// If first instance, make it Singleton
			instance = this;
			DontDestroyOnLoad(this);
		}else{
			// If Singleton already exists, destroy it
			if(this != instance){
				Destroy(this.gameObject);
			}
		}
		SetActiveStatus();
		InitBoards();
	}

	/* Initialises UIBoards by setting them active. Refer to individual UIBoards for
	how they become active/inactive */
	private void InitBoards(){
		for(int i = 0; i < uiBoards.Count; ++i){
			uiBoards[i].gameObject.SetActive(true);
		}
	}

	public void PulseText(Text text){
		StartCoroutine("PulseTextRoutine", text);
	}

	private IEnumerator PulseTextRoutine(Text text){
		bool fadeOut = true;
		while(true){
			Color old = text.color;
			if(fadeOut){
				text.color = new Color(old.r, old.g, old.b, text.color.a - 1f * Time.deltaTime);
				if(text.color.a <= 0.0f){
					fadeOut = false;
				}
			}else{
				text.color = new Color(old.r, old.g, old.b, text.color.a + 1f * Time.deltaTime);
				if(text.color.a >= 1.0f){
					fadeOut = true;
				}
			}
			yield return null;
		}
	}

	public void FadeText(Text text, float time, float toAlpha){
		StartCoroutine(FadeTextRoutine(text, time, toAlpha));
	}

	private IEnumerator FadeTextRoutine(Text text, float time, float toAlpha){
		float i = 0.0f;
		float rate = 1.0f / time;
		Color start = text.color;
		Color end = new Color(start.r, start.g, start.b, toAlpha);
		while(i < 1.0f){
			i += Time.deltaTime / Time.timeScale * rate;
			text.color = Color.Lerp(start, end, i);
			yield return null;
		}
	}

	public void FadeScreen(float time, float toAlpha){
		overlayPanel.gameObject.SetActive(true);
		StartCoroutine(FadeScreenRoutine(time, toAlpha));
	}

	public void FadeScreen(Action method, float time, float toAlpha){
		overlayPanel.gameObject.SetActive(true);
		StartCoroutine( FadeScreenRoutine(() => method(), time, toAlpha));
	}

	private IEnumerator FadeScreenRoutine(Action method, float time, float toAlpha){
		float i = 0.0f;
		float rate = 1.0f / time;
		Color start = overlayPanel.color;
		Color end = new Color(start.r, start.g, start.b, toAlpha);
		while(i < 1.0f){
			i += Time.deltaTime / Time.timeScale * rate;
			overlayPanel.color = Color.Lerp(start, end, i);
			yield return null;
		}
		SetActiveStatus();
		method();
	}

	private IEnumerator  FadeScreenRoutine(float time, float toAlpha){
		float i = 0.0f;
		float rate = 1.0f / time;
		Color start = overlayPanel.color;
		Color end = new Color(start.r, start.g, start.b, toAlpha);
		while(i < 1.0f){
			i += Time.deltaTime / Time.timeScale * rate;
			overlayPanel.color = Color.Lerp(start, end, i);
			yield return null;
		}
		SetActiveStatus();
	}

	private void SetActiveStatus(){
		bool activeStatus = overlayPanel.color.a <= 0.0f ? false : true;
		overlayPanel.gameObject.SetActive(activeStatus);
	}
}
