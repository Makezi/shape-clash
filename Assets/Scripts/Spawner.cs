using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

	public float direction;				// Direction in which the obstacles will go 

	private float spawnerXPos;			// Reference to current x position of the spawner
	private GameObject obstacle;		// Reference to previous spawned obstacle

	void Start(){
		spawnerXPos = transform.position.x;
	}

	/* Toggle spawning on */
	public void StartSpawning(){
		StartCoroutine("Spawn");
	}

	/* Toggle spawning off */
	public void StopSpawning(){
		StopCoroutine("Spawn");
	}

	/* Spawn an obstacle at a random spawn position from the remaining spawn points list. Obstacles are spawned
	with a specific gap between each obstacle (distance value from SpawnManager) */
	private IEnumerator Spawn(){
		yield return new WaitForSeconds(SpawnManager.Instance.startTime);
		while(true){
			// Obtain random obstacle from object pool and initialize it
			if(obstacle == null){
				obstacle = SpawnManager.Instance.GetPseudoRandomObstacle();
				obstacle.transform.position = new Vector3(transform.position.x, SpawnManager.Instance.GetRandomSpawnPoint(), 0);
				obstacle.transform.rotation = transform.rotation;
				ObstacleController controller = obstacle.GetComponent<ObstacleController>();
				controller.Shape.rotateSpeed *= direction;
				controller.direction = direction;
				yield return null;
			}

			// Remove reference to previously spawned obstacle if distance from obstacle to spawner position is too large
			if(Mathf.Abs(obstacle.transform.position.x - spawnerXPos) > SpawnManager.Instance.spawnGap){
				// Debug.Log(Mathf.Abs(obstacle.transform.position.x -  spawnerXPos));
				obstacle = null;
				yield return null;
			}

			yield return null;
		}
	}
}
