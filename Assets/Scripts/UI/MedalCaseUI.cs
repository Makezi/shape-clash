using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MedalCaseUI : MonoBehaviour {

	public Sprite noMedalSprite;		// Reference to the no medal sprite
	public List<Sprite> medalSprites;	// Reference to list of medal sprites
	public Image bronzeMedal;
	public Image silverMedal;
	public Image goldMedal;
	public Image platinumMedal;
	public Image diamondMedal;

	private bool opened = false;

	// Use this for initialization
	void Start () {
		GameManager.Instance.stateMachine.onStateChanged += HandleStateChange;
				gameObject.SetActive(opened);
		UpdateMedals();
	}

	public void OpenCase(){
		opened = true;
		gameObject.SetActive(opened);
	}

	public void CloseCase(){
		opened = false;
		gameObject.SetActive(opened);
	}

	public void ToggleCase(){
		opened = !opened;
		gameObject.SetActive(opened);
	}

	private void UpdateMedals(){
		bronzeMedal.sprite = PlayerPrefs.GetInt("BronzeMedal") == 1 ? GetMedalSprite("Bronze") : noMedalSprite;
		silverMedal.sprite = PlayerPrefs.GetInt("SilverMedal") == 1 ? GetMedalSprite("Silver") : noMedalSprite;
		goldMedal.sprite = PlayerPrefs.GetInt("GoldMedal") == 1 ? GetMedalSprite("Gold") : noMedalSprite;
		platinumMedal.sprite = PlayerPrefs.GetInt("PlatinumMedal") == 1 ? GetMedalSprite("Platinum") : noMedalSprite;
		diamondMedal.sprite = PlayerPrefs.GetInt("DiamondMedal") == 1 ? GetMedalSprite("Diamond") : noMedalSprite;
	}

	/* Retrieves medal sprite from list of available medals when comparison is made to medal earned */
	private Sprite GetMedalSprite(string name){
		for(int i = 0; i < medalSprites.Count; ++i){
			string lower = medalSprites[i].name.ToLower();
			if(lower.Contains(name.ToLower())){
				return medalSprites[i];
			}
		}
		return noMedalSprite;
	}
	

	private void HandleStateChange(){
		if(GameManager.Instance.stateMachine.CurrentStateEquals<GameOver>()){
			UpdateMedals();
		}
	}
}
