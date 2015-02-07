using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Makezi.ObjectPool {

	public class PoolManager : MonoBehaviour {

		// public static PoolManager instance;			// Static instance
		public static PoolManager Instance { get; private set; }

		public List<PrefabPool> poolCollection;		// List containing all pooled prefabs
		
		// Collection of pools. Used for fast lookups at run-time
		private Dictionary<string, PrefabPool> pools = new Dictionary<string, PrefabPool>();

		// Use this for initialization
		void Awake(){
			// instance = this;
			if(Instance != null && Instance != this){
				Destroy(gameObject);
			}
			// Save singleton instance
			Instance = this;
			// Don't destroy between scenes
			DontDestroyOnLoad(gameObject);
			InitPrefabPools();
		}
		
		/* Initialises all prefab pools in the collection and adds them to the Dictionary */
		private void InitPrefabPools(){
			PrefabPool pp;
			for(int i = 0; i < poolCollection.Count; ++i){
				pp = poolCollection[i];
				pp.Init();
				pools.Add(pp.PrefabName, pp);
			}
		}
		
		/* Spawn a GameObject from the specified pool using name */
		public GameObject Spawn(string name){
			PrefabPool pp;
			if(pools.TryGetValue(name, out pp)){
				return pp.Spawn();
			}else{
				return null;
			}
		}
		
		/* Spawn GameObject from the object pool using passed obj for comparison */
		public GameObject Spawn(GameObject obj){
			if(pools.ContainsKey(obj.name)){
				return pools[obj.name].Spawn();
			}else{
				return GameObject.Instantiate(obj) as GameObject;
			}
		}

		/* Spawn random Gameobject from the object pool consisting of substring name */
		public GameObject SpawnRandom(string name){
			IList<string> objs = pools.Keys.Where(x => x.Contains(name)).ToList();
			if(objs.Count <= 0){ return null; }
			return Spawn(objs.ElementAt(Random.Range(0, objs.Count)));
		}

		/* Add an object to the pool at runtime */
		public void AddToPool(GameObject obj, int pooledAmount, bool canGrow){
			PrefabPool pp = new PrefabPool(obj, pooledAmount, canGrow);
			pp.Init();
			pools.Add(pp.PrefabName, pp);
		}
		
		private void OnDisable(){
			pools.Clear();
		}
		
		private void OnDestroy(){
			pools.Clear();
		}
		
	}
}

