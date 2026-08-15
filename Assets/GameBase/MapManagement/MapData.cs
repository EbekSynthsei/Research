using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LaniakeaCode.MapManagement
{
    /// <summary>
    /// Data class for storing map information.
    /// </summary>
    [System.Serializable]
    public class MapData
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
        
        public MapData()
        {
            mapSize = new Vector3Int(20, 15, 1);
            tileSize = 1f;
            cameraPosition = Vector3.zero;
            cameraSize = 10f;
        }
    }

    /// <summary>
    /// Lighting settings for the map.
    /// </summary>
    [System.Serializable]
    public class LightingSettings
    {
        public bool useRealtimeGI = false;
        public float intensity = 1f;
        public Color lightColor = Color.white;
        public LightShadows shadows = LightShadows.Soft;
    }
}