using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Makezi.ObjectPool;

public class SpawnManager : MonoBehaviour {

	public static SpawnManager Instance { get; private set; }

	public float startTime;					// Initial delay before obstacles spawn
	public float spawnGap;					// Gap in terms of distance between each spawn
	public float spawnPadding;				// Padding which limits the spawn points along the y axis
	public List<Spawner> spawners;			// List of references to spawners
	public List<GameObject> obstacles;		// Reference list of all obstacle objects
	public int poolAmount = 6;				// Amount of obstacles which will be pooled

	private float heightClamp;				// Minimum and maximum spawn point positions based on camera aspect
	private List<float> spawnPoints;		// List of all available points along the y axis
	private List<GameObject> obstacleList; 	// Current list of obstacles remaining, yet to be spawned

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
		heightClamp = Camera.main.orthographicSize;
		GenerateSpawnPoints();
		GenerateObstacles();
		AddObstaclesToPool();
	}

	/* Add possible spawn points to the spawn locations list based on camera aspect height clamps */
	private void GenerateSpawnPoints(){
		spawnPoints = new List<float>();
		for(float i = -heightClamp + spawnPadding; i <= heightClamp - spawnPadding; i += 1f){
			spawnPoints.Add(i);
		}
	}

	/* Generates list of obstacles to be spawned once emptied */
	private void GenerateObstacles(){
		obstacleList = new List<GameObject>();
		for(int i = 0; i < obstacles.Count; ++i){
			obstacleList.Add(obstacles[i]);
		}
	}

	/* Adds all obstacles to the object pool */
	private void AddObstaclesToPool(){
		for(int i = 0; i < obstacles.Count; ++i){
			PoolManager.Instance.AddToPool(obstacles[i].gameObject, poolAmount, true);
		}
	}

	/* Return a spawn point from the list. If the list of remaining spawn points is empty, refill */
	public float GetRandomSpawnPoint(){
		if(spawnPoints.Count <= 0){
			GenerateSpawnPoints();
		}
		float spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
		spawnPoints.Remove(spawnPoint);
		return spawnPoint;
	}

	/* Retrieves random obstacle from object pool */
	public GameObject GetRandomObstacle(){
		return PoolManager.Instance.SpawnRandom(obstacles[Random.Range(0, obstacles.Count)].name);
	}

	/* Retrieves random obstacle from object pool. Pseudo random leads to all shapes being retrieved
	before duplicates */
	public GameObject GetPseudoRandomObstacle(){
		if(obstacleList.Count <= 0){
			GenerateObstacles();
		}
		GameObject obj = obstacleList[Random.Range(0, obstacleList.Count)];
		obstacleList.Remove(obj);
		return PoolManager.Instance.SpawnRandom(obj.name);
	}

	/* Toggle spawn status */
	public void Spawn(bool status){
		if(status){
			for(int i = 0; i < spawners.Count; ++i){
				spawners[i].StartSpawning();
			}
		}else{
			for(int i = 0; i < spawners.Count; ++i){
				spawners[i].StopSpawning();
			}
		}
	}
	
}
