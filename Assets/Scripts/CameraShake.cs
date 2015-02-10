using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	public float ShakeAmount = 0.1f;		// The intensity of the screen shake. Higher is more intense
   	public float DecreaseFactor = 1.5f;		// How quickly the shaking stops. Higher means shorter shakes

   	private new Camera camera;				// Reference to main camera
   	private Vector3 cameraPos;				// Reference to camera position
   	private float shake = 0.0f;				// Current shake value

	void Awake() {
   		this.camera = Camera.main;
   	}

   	void Update() {
	   	// Check if the screen should be shaking
		if (this.shake > 0.0f) {
	    	// Shake the camera
		    this.camera.transform.localPosition = Random.insideUnitSphere * this.ShakeAmount * this.shake;
		    // Reduce the amount of shaking for next tick
		    this.shake-= Time.deltaTime * this.DecreaseFactor;
		    // Check to see if we've stopped shaking
		    if (this.shake <= 0.0f) {
		    	// Clamp the shake amount back to zero, and reset the camera position to our cached value
	        	this.shake = 0.0f;
	         	this.camera.transform.localPosition = this.cameraPos;
	      	}
	   	}
   	}

	void LateUpdate(){
		// Keep the camera at set z axis position
   		this.camera.transform.position = new Vector3(this.camera.transform.position.x, this.camera.transform.position.y, -10);
   	}

   	/* Shake the camera. The amount refers to the wobble until rest */
   	public void Shake(float amount) {
   	 	// Check if we're already shaking.
   		if (this.shake <= 0.0f) {
      		// If we aren't, cache the camera position.
      		this.cameraPos = this.camera.transform.position;
   		}

   		// Set the 'shake' value.
   		this.shake = amount;
  	}
}
