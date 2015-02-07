using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Makezi.ObjectPool {

	[CustomEditor (typeof(PoolManager))]
	public class PoolManagerEditor : Editor {

		private List<bool> prefabFoldouts;			// True for indices corresponding to individual prefab pool foldouts
		private PoolManager targetObject;			// Selected PoolManager object
		
		private List<PrefabPool> poolCollection;	// PrefabPool collection belonging to the PoolManager

		public void OnEnable(){
			targetObject = target as PoolManager;
			this.poolCollection = targetObject.poolCollection;
			prefabFoldouts = new List<bool>();
			if(poolCollection == null){ return; }
			if(poolCollection.Count <= 0){ return; }
			for(int i = 0; i < poolCollection.Count; ++i){
				prefabFoldouts.Add(true);
			}
			ClearNullReferences();
		}
		
		/* Checks all PrefabPools in the collection for null reference objects. These can occur
		sometimes when a created PrefabPool may have been deleted recently */
		private void ClearNullReferences(){
			if(poolCollection == null){
				return;
			}
			int i = 0;
			while(i < poolCollection.Count){
				if(poolCollection[i].prefab == null){
					RemovePoolAtIndex(i);
				}else{
					i++;
				}
			}
		}
		
		/* Remove the pool at specified index */
		private void RemovePoolAtIndex(int index){
			for(int i = index; i < poolCollection.Count - 1; ++i){
				poolCollection[i] = poolCollection[i + 1];
			}
			poolCollection.RemoveAt(poolCollection.Count - 1);
		}
		
		/* Add a pool to the specified GameObject (if one doesn't exist) */
		private void AddPool(GameObject obj){
			PrefabPool prefabPool = new PrefabPool();
			if(poolCollection == null){
				poolCollection = new List<PrefabPool>();
			}
			
			// Make sure duplicate objects are not pooled
			if(poolCollection != null){
				foreach(PrefabPool pp in poolCollection){
					if(pp.PrefabName == obj.name){
						EditorUtility.DisplayDialog("Pool Manager", "Pool Manager already manages a GameObject " +
							"with the name " + obj.name + ".\n\n Please give them unique names", "OK");
						return ;
					}
				}
			}
			
			prefabPool.prefab = obj;
			poolCollection.Add(prefabPool);
			while(poolCollection.Count > prefabFoldouts.Count){
				prefabFoldouts.Add(false);
			}
		}
		
		/* Draw custom inspector */
		public override void OnInspectorGUI(){
			GUILayout.Space(15f);
			DropPrefabGUI();
			
			if(poolCollection == null){ return; }
			
			GUILayout.Space(5f);
			GUILayout.Label("Pooled Objects", EditorStyles.boldLabel);
			
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(10f);
			EditorGUILayout.BeginVertical();
			
			for(int i = 0; i < poolCollection.Count; ++i){
				PrefabPool pp = poolCollection[i];
				string name;
				if(pp.prefab != null){
					name = pp.prefab.name;
				}else{
					name = "EMPTY";
				}
				
				// PrefabPool DropDown
				EditorGUILayout.BeginHorizontal();
				prefabFoldouts[i] = EditorGUILayout.Foldout(prefabFoldouts[i], name, EditorStyles.foldout);
				if(GUILayout.Button("-", GUILayout.Width(20f))){
					RemovePoolAtIndex(i);
				}
				EditorGUILayout.EndHorizontal();
				
				if(prefabFoldouts[i]){
					EditorGUILayout.BeginHorizontal();
					GUILayout.Space(10f);
					EditorGUILayout.BeginVertical();
					
					// Pooled Amount
					EditorGUILayout.BeginHorizontal();
					GUILayout.Label("Pooled Amount", EditorStyles.label, GUILayout.Width(115f));
					pp.pooledAmount = EditorGUILayout.IntField(pp.pooledAmount);
					if(pp.pooledAmount < 0){ pp.pooledAmount = 0; }
					EditorGUILayout.EndHorizontal();
					
					// Can Grow
					EditorGUILayout.BeginHorizontal();
					GUILayout.Label("Can Grow", EditorStyles.label, GUILayout.Width(115f));
					pp.canGrow = EditorGUILayout.Toggle(pp.canGrow);
					EditorGUILayout.EndHorizontal();
					
					EditorGUILayout.EndVertical();
					EditorGUILayout.EndHorizontal();
				}
				EditorGUILayout.Space();
			}
			EditorGUILayout.EndVertical();
			EditorGUILayout.EndHorizontal();
		}
		
		/* GUI for dropping a prefab to the pool */
		private void DropPrefabGUI(){
			var evt = Event.current;
			var dropArea = GUILayoutUtility.GetRect(0f, 50f, GUILayout.ExpandWidth(true));
			GUI.Box(dropArea, "Drop a Prefab or GameObject here to add it to PoolManager");
			
			switch(evt.type){
				case EventType.DragUpdated:
				case EventType.DragPerform:
					if(!dropArea.Contains(evt.mousePosition)){
						break;
					}
					
					DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
					
					if(evt.type == EventType.DragPerform){
						DragAndDrop.AcceptDrag();
						foreach(var draggedObject in DragAndDrop.objectReferences){
							var go = draggedObject as GameObject;
							if(!go){
								continue;
							}
							AddPool(go);
						}
					}
					Event.current.Use();
					break;
			}
		}
	}
}
