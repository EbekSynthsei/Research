using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LaniakeaCode.MapManagement
{
    /// <summary>
    /// ScriptableObject asset for storing map data.
    /// </summary>
    [CreateAssetMenu(fileName = "NewMap", menuName = "Map Management/Map Asset")]
    public class MapAsset : ScriptableObject
    {
        [Header("Map Information")]
        public string mapName;
        public Vector3Int mapSize;
        public float tileSize = 1f;
        
        [Header("Tilemap References")]
        public Tilemap groundTilemap;
        public Tilemap decorationTilemap;
        public Tilemap foregroundTilemap;
        
        [Header("Scene References")]
        public string[] sceneNames;
        
        [Header("Camera Settings")]
        public Vector3 cameraPosition;
        public float cameraSize = 10f;
        
        [Header("Environment Settings")]
        public Color backgroundColor = Color.black;
        public LightingSettings lightingSettings;
        
        [Header("Map Description")]
        public string description;
        
        public MapAsset()
        {
            mapSize = new Vector3Int(20, 15, 1);
            tileSize = 1f;
            cameraPosition = Vector3.zero;
            cameraSize = 10f;
        }
    }
}