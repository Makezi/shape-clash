using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {

	public static AudioManager Instance { get; private set; }

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
	
	/* Plays audio clip */
	public void PlayClip(AudioClip clip){
		audio.PlayOneShot(clip);
	}

	/* Toggle audio mute */
	public void Mute(bool status){
		audio.mute = status;
	}
}
