using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

namespace LaniakeaCode.MapManagement
{
    /// <summary>
    /// Scene-based map manager that integrates with Unity's SceneManager.
    /// </summary>
    public class SceneMapManager : MapManager
    {
        [Header("Scene Management")]
        [SerializeField]
        private string[] sceneNames;
        
        [SerializeField]
        private bool loadScenesAdditively = true;

        [Header("Camera Integration")]
        [SerializeField]
        private Camera mainCamera;
        
        [SerializeField]
        private Vector3 cameraOffset = new Vector3(0, 0, -10);

        protected override void Awake()
        {
            base.Awake();
            
            // Initialize camera if not set
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }
        }

        /// <summary>
        /// Initializes the scene-based map.
        /// </summary>
        public override void InitializeMap()
        {
            base.InitializeMap();
            
            // Set up the camera for 3D perspective with tilemap
            if (mainCamera != null)
            {
                mainCamera.orthographic = false; // Enable 3D perspective
                mainCamera.transform.position = new Vector3(0, 0, -10);
            }
        }

        /// <summary>
        /// Loads a scene using additive loading.
        /// </summary>
        /// <param name="sceneName">Name of the scene to load</param>
        public override void LoadScene(string sceneName)
        {
            if (string.IsNullOrEmpty(sceneName))
            {
                Debug.LogError("Scene name is null or empty!");
                return;
            }

            if (loadScenesAdditively)
            {
                SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                Debug.Log($"Loading scene additively: {sceneName}");
            }
            else
            {
                SceneManager.LoadScene(sceneName);
                Debug.Log($"Loading scene: {sceneName}");
            }
        }

        /// <summary>
        /// Loads multiple scenes additively.
        /// </summary>
        /// <param name="sceneNames">Array of scene names to load</param>
        public void LoadScenes(string[] sceneNames)
        {
            foreach (string sceneName in sceneNames)
            {
                LoadScene(sceneName);
            }
        }

        /// <summary>
        /// Unloads a scene.
        /// </summary>
        /// <param name="sceneName">Name of the scene to unload</param>
        public void UnloadScene(string sceneName)
        {
            SceneManager.UnloadSceneAsync(sceneName);
            Debug.Log($"Unloading scene: {sceneName}");
        }

        /// <summary>
        /// Sets up camera position for a specific tilemap.
        /// </summary>
        /// <param name="tilemap">The tilemap to center the camera on</param>
        public void CenterCameraOnTilemap(Tilemap tilemap)
        {
            if (mainCamera == null || tilemap == null) return;

            BoundsInt bounds = tilemap.cellBounds;
            Vector3 center = tilemap.LocalToWorld(bounds.center);
            
            // Adjust for 3D perspective
            mainCamera.transform.position = new Vector3(
                center.x + cameraOffset.x,
                center.y + cameraOffset.y,
                cameraOffset.z
            );
        }

        /// <summary>
        /// Gets the tile at the specified world position.
        /// </summary>
        /// <param name="position">World position</param>
        /// <returns>TileBase if found, null otherwise</returns>
        public override TileBase GetTileAtPosition(Vector3 position)
        {
            // Try to get tile from each tilemap
            if (groundTilemap != null)
            {
                TileBase tile = groundTilemap.GetTile(GetTilePosition(position));
                if (tile != null) return tile;
            }
            
            if (decorationTilemap != null)
            {
                TileBase tile = decorationTilemap.GetTile(GetTilePosition(position));
                if (tile != null) return tile;
            }
            
            if (foregroundTilemap != null)
            {
                TileBase tile = foregroundTilemap.GetTile(GetTilePosition(position));
                if (tile != null) return tile;
            }
            
            return null;
        }

        /// <summary>
        /// Sets up the map with tilemaps and initializes camera.
        /// </summary>
        public void SetupMapWithTilemaps()
        {
            // Initialize all tilemaps
            if (groundTilemap != null)
            {
                groundTilemap.CompressBounds();
            }
            
            if (decorationTilemap != null)
            {
                decorationTilemap.CompressBounds();
            }
            
            if (foregroundTilemap != null)
            {
                foregroundTilemap.CompressBounds();
            }

            // Set up camera
            InitializeMap();
        }
    }
}