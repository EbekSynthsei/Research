using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LaniakeaCode.MapManagement
{
    /// <summary>
    /// Base map manager that handles tilemap-based scene management.
    /// </summary>
    public class MapManager : MonoBehaviour
    {
        [Header("Tilemap References")]
        [SerializeField]
        private Tilemap groundTilemap;
        
        [SerializeField]
        private Tilemap decorationTilemap;
        
        [SerializeField]
        private Tilemap foregroundTilemap;

        [Header("Map Settings")]
        [SerializeField]
        private Vector3Int mapSize = new Vector3Int(20, 15, 1);
        
        [SerializeField]
        private float tileSize = 1f;

        [Header("Scene Management")]
        [SerializeField]
        private bool useAdditiveLoading = true;
        
        // Singleton instance
        public static MapManager Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Initializes the map with the specified tilemap data.
        /// </summary>
        public virtual void InitializeMap()
        {
            // Base initialization logic
            Debug.Log("Initializing map with size: " + mapSize);
        }

        /// <summary>
        /// Loads a scene using tilemap-based approach.
        /// </summary>
        /// <param name="sceneName">Name of the scene to load</param>
        public virtual void LoadScene(string sceneName)
        {
            Debug.Log("Loading scene: " + sceneName);
            // Implementation would depend on how scenes are managed in this project
        }

        /// <summary>
        /// Gets the world position from tile coordinates.
        /// </summary>
        /// <param name="tilePosition">Tile coordinates</param>
        /// <returns>World position</returns>
        public Vector3 GetWorldPosition(Vector3Int tilePosition)
        {
            return new Vector3(
                tilePosition.x * tileSize,
                tilePosition.y * tileSize,
                0f
            );
        }

        /// <summary>
        /// Gets the tile coordinates from world position.
        /// </summary>
        /// <param name="worldPosition">World position</param>
        /// <returns>Tile coordinates</returns>
        public Vector3Int GetTilePosition(Vector3 worldPosition)
        {
            return new Vector3Int(
                Mathf.FloorToInt(worldPosition.x / tileSize),
                Mathf.FloorToInt(worldPosition.y / tileSize),
                0
            );
        }

        /// <summary>
        /// Gets the tile at the specified position.
        /// </summary>
        /// <param name="position">World position</param>
        /// <returns>TileData if found, null otherwise</returns>
        public virtual TileBase GetTileAtPosition(Vector3 position)
        {
            // Implementation would depend on how tiles are stored
            return null;
        }
    }
}