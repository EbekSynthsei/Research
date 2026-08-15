using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LaniakeaCode.MapManagement
{
    /// <summary>
    /// Handles loading and managing maps.
    /// </summary>
    public class MapLoader : MonoBehaviour
    {
        [Header("Map Data")]
        [SerializeField]
        private MapData mapData;
        
        [Header("Scene Management")]
        [SerializeField]
        private SceneMapManager sceneMapManager;

        private void Awake()
        {
            if (sceneMapManager == null)
            {
                sceneMapManager = FindObjectOfType<SceneMapManager>();
            }
            
            if (sceneMapManager == null)
            {
                Debug.LogError("SceneMapManager not found in scene!");
            }
        }

        /// <summary>
        /// Loads a map using the provided MapData.
        /// </summary>
        /// <param name="data">Map data to load</param>
        public void LoadMap(MapData data)
        {
            if (data == null)
            {
                Debug.LogError("Map data is null!");
                return;
            }

            mapData = data;
            
            // Set up the scene manager with this map data
            if (sceneMapManager != null)
            {
                // Setup camera
                if (data.cameraPosition != Vector3.zero)
                {
                    sceneMapManager.transform.position = data.cameraPosition;
                }
                
                // Initialize the map
                sceneMapManager.InitializeMap();
                
                // Load associated scenes
                if (data.sceneNames != null && data.sceneNames.Length > 0)
                {
                    sceneMapManager.LoadScenes(data.sceneNames);
                }
            }
            
            Debug.Log($"Loaded map: {data.mapName}");
        }

        /// <summary>
        /// Loads a map from a ScriptableObject.
        /// </summary>
        /// <param name="mapAsset">Map data asset</param>
        public void LoadMapFromAsset(MapData mapAsset)
        {
            if (mapAsset != null)
            {
                LoadMap(mapAsset);
            }
        }

        /// <summary>
        /// Unloads the current map.
        /// </summary>
        public void UnloadMap()
        {
            if (mapData != null && mapData.sceneNames != null)
            {
                foreach (string sceneName in mapData.sceneNames)
                {
                    if (!string.IsNullOrEmpty(sceneName))
                    {
                        sceneMapManager.UnloadScene(sceneName);
                    }
                }
            }
            
            Debug.Log("Unloaded current map");
        }
    }
}