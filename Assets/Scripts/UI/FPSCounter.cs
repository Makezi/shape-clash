using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour {

	public float updateInterval = 0.5f;		// Interval to retrieve frames
	
	private float accum = 0; 				// FPS accumulated over the interval
	private int frames = 0;					// Frames drawn over the interval
	private float timeLeft;					// Time left for current interval
	private Text fpsText;					// Reference to UI text
	
	void Awake(){
		fpsText = GetComponent<Text>();
	}

	// Use this for initialization
	void Start () {
		timeLeft = updateInterval;
	}
	
	// Update is called once per frame
	void Update () {
		timeLeft -= Time.deltaTime;
		accum += Time.timeScale / Time.deltaTime;
		frames++;
		
		// Interval ended - update text and start new interval
		if(timeLeft <= 0.0){
			// display two fractional digits
			float fps = accum / frames;
			string format = string.Format("FPS: {0:F2}", fps);
			fpsText.text = format;
			
			// FPS less than 30 is displayed as yellow 
			// FPS less than 10 is displayed as red
			// FPS greater or equal to 30 is displayed in green
			if(fps < 30){
				fpsText.color = Color.yellow;
			}else{
				if(fps < 10){
					fpsText.color = Color.red;
				}else{
					fpsText.color = Color.green;
				}
			}
			timeLeft = updateInterval;
			accum = 0.0f;
			frames = 0;
		}
	}
}
