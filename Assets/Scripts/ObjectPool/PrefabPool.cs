using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Makezi.ObjectPool {

	[System.Serializable]
	public class PrefabPool {

		public GameObject prefab;			// The prefab managed by this class
		public int pooledAmount;			// The number of the prefab to pre-instantiate
		public bool canGrow = true;			// Allow instantiation of more prefabs if all are in use
		
		private string Name;				// Name of the prefab
		private List<GameObject> pool;		// The prefab pool

		public PrefabPool(){}

		public PrefabPool(GameObject prefab, int pooledAmount, bool canGrow){
			this.prefab = prefab;
			this.pooledAmount = pooledAmount;
			this.canGrow = canGrow;
		}
		
		public void Init(){
			pool = new List<GameObject>();
			Name = prefab.name;
			PreAllocate();
		}
		
		/* Adds specified pooledAmount of prefab instances to the pool */
		private void PreAllocate(){
			for(int i = 0; i < pooledAmount; ++i){
				GameObject obj = GameObject.Instantiate(prefab.gameObject) as GameObject;
				obj.SetActive(false);
				pool.Add(obj);
			}
		}
		
		/* Spawn the prefab by setting it active (if not already) */
		public GameObject Spawn(){
			for(int i = 0; i < pool.Count; ++i){
				if(!pool[i].activeInHierarchy){
					pool[i].SetActive(true);
					return pool[i];
				}
			}

			// If the spawn limit as been reached and canGrow is true, instantiate new GameObject
			if(canGrow){
				GameObject obj = GameObject.Instantiate(prefab.gameObject) as GameObject;
				pool.Add(obj);
				return obj;
			}
			return null;
		}
		
		public string PrefabName {
			get { return Name; }
		}

	}
}
