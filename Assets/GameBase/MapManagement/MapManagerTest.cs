using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LaniakeaCode.MapManagement
{
    /// <summary>
    /// Test script to verify MapManagement system functionality.
    /// </summary>
    public class MapManagerTest : MonoBehaviour
    {
        [Header("Test Configuration")]
        [SerializeField]
        private bool runTests = false;
        
        [SerializeField]
        private SceneMapManager sceneMapManager;
        
        [SerializeField]
        private MapAsset testMapAsset;

        private void Start()
        {
            if (runTests && sceneMapManager != null)
            {
                RunSystemTests();
            }
        }

        private void RunSystemTests()
        {
            Debug.Log("=== Starting Map Management System Tests ===");
            
            // Test 1: Check if SceneMapManager instance exists
            if (SceneMapManager.Instance != null)
            {
                Debug.Log("✓ SceneMapManager singleton created successfully");
            }
            else
            {
                Debug.LogError("✗ SceneMapManager singleton not found");
            }
            
            // Test 2: Initialize map
            sceneMapManager.InitializeMap();
            Debug.Log("✓ Map initialized");
            
            // Test 3: Test coordinate conversion
            Vector3Int tilePos = new Vector3Int(5, 3, 0);
            Vector3 worldPos = sceneMapManager.GetWorldPosition(tilePos);
            Vector3Int returnedTilePos = sceneMapManager.GetTilePosition(worldPos);
            
            if (tilePos == returnedTilePos)
            {
                Debug.Log("✓ Coordinate conversion working correctly");
            }
            else
            {
                Debug.LogError("✗ Coordinate conversion failed");
            }
            
            // Test 4: Load test map if available
            if (testMapAsset != null)
            {
                Debug.Log($"✓ Test map asset loaded: {testMapAsset.mapName}");
                
                // Try to set up the map with tilemaps
                sceneMapManager.SetupMapWithTilemaps();
                Debug.Log("✓ Map setup completed");
            }
            
            Debug.Log("=== Map Management System Tests Completed ===");
        }

        /// <summary>
        /// Public method to manually trigger tests from inspector or other scripts.
        /// </summary>
        public void RunTests()
        {
            if (sceneMapManager != null)
            {
                RunSystemTests();
            }
            else
            {
                Debug.LogError("SceneMapManager reference not set!");
            }
        }
    }
}