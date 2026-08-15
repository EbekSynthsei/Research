using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace LaniakeaCode.MapManagement
{
    [DefaultExecutionOrder(-1)]
    public class SceneInstantiate : MonoBehaviour
    {
        private Object persistentScene;

        [Header("Available Scenes")]
        [SerializeField] private List<string> availableScenes = new List<string>();
        
        // Reference to the map manager for integration with tilemap system
        [SerializeField] private SceneMapManager sceneMapManager;
        
        private void Awake()
        {
            // Initialize our map manager if not already done
            if (sceneMapManager == null)
            {
                sceneMapManager = FindObjectOfType<SceneMapManager>();
                
                // If no SceneMapManager found, create one
                if (sceneMapManager == null)
                {
                    GameObject mapManagerGO = new GameObject("SceneMapManager");
                    sceneMapManager = mapManagerGO.AddComponent<SceneMapManager>();
                }
            }
            
            // Load the managers scene additively
            SceneManager.LoadSceneAsync("Managers", LoadSceneMode.Additive);
        }

        #region Runtime Operations
        [ContextMenu("ChangeScene")]
        public void NextScene()
        {
            // This should be more dynamic - we could load the next scene in the list
            if (availableScenes.Count > 0)
            {
                // Get the currently loaded scene name
                string currentSceneName = SceneManager.GetActiveScene().name;
                
                // Find the index of the current scene in our available scenes list
                int currentIndex = availableScenes.IndexOf(currentSceneName);
                
                // If current scene is found in our list, load the next one
                if (currentIndex != -1)
                {
                    int nextIndex = (currentIndex + 1) % availableScenes.Count;
                    string nextSceneName = availableScenes[nextIndex];
                    
                    // Use our map manager to handle scene loading for tilemap integration
                    if (sceneMapManager != null)
                    {
                        sceneMapManager.LoadScene(nextSceneName);
                    }
                    else
                    {
                        // Fallback to standard scene loading
                        SceneManager.UnloadSceneAsync(currentSceneName);
                        SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive);
                    }
                }
            }
        }
        
        // Public method to switch to any scene using our map manager system
        public void SwitchToScene(string sceneName)
        {
            if (!string.IsNullOrEmpty(sceneName) && availableScenes.Contains(sceneName))
            {
                if (sceneMapManager != null)
                {
                    sceneMapManager.LoadScene(sceneName);
                }
                else
                {
                    // Fallback to standard scene loading
                    SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                }
            }
            else
            {
                Debug.LogWarning($"Scene '{sceneName}' not found in available scenes list");
            }
        }
        
        // Public method to switch to scene by index with validation using our map manager system
        public void SwitchToSceneByIndex(int index)
        {
            if (index >= 0 && index < availableScenes.Count)
            {
                if (sceneMapManager != null)
                {
                    sceneMapManager.LoadScene(availableScenes[index]);
                }
                else
                {
                    // Fallback to standard scene loading
                    SceneManager.LoadSceneAsync(availableScenes[index], LoadSceneMode.Additive);
                }
            }
            else
            {
                Debug.LogWarning($"Invalid scene index: {index}. Available scenes count: {availableScenes.Count}");
            }
        }
        #endregion

        #region Editor Operations
        #if UNITY_EDITOR
        [ContextMenu("Load First Available Scene")]
        public void LoadFirstAvailableScene()
        {
            if (availableScenes.Count > 0)
            {
                if (sceneMapManager != null)
                {
                    sceneMapManager.LoadScene(availableScenes[0]);
                }
                else
                {
                    // Fallback to standard scene loading
                    SceneManager.LoadSceneAsync(availableScenes[0], LoadSceneMode.Additive);
                }
            }
        }
        
        [ContextMenu("Load Last Available Scene")]
        public void LoadLastAvailableScene()
        {
            if (availableScenes.Count > 0)
            {
                if (sceneMapManager != null)
                {
                    sceneMapManager.LoadScene(availableScenes[availableScenes.Count - 1]);
                }
                else
                {
                    // Fallback to standard scene loading
                    SceneManager.LoadSceneAsync(availableScenes[availableScenes.Count - 1], LoadSceneMode.Additive);
                }
            }
        }
        
        [ContextMenu("Load All Available Scenes")]
        public void LoadAllAvailableScenes()
        {
            foreach (string scene in availableScenes)
            {
                if (sceneMapManager != null)
                {
                    sceneMapManager.LoadScene(scene);
                }
                else
                {
                    // Fallback to standard scene loading
                    SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
                }
            }
        }
        
        // Auto populate scenes - this will be empty at runtime but can be called from editor
        [ContextMenu("Auto Populate Scenes")]
        public void AutoPopulateScenes()
        {
            // This implementation works only in the Unity Editor
            var scenePaths = AssetDatabase.FindAssets("t:Scene");
            availableScenes.Clear();
            
            foreach (string scenePath in scenePaths)
            {
                string fullPath = AssetDatabase.GUIDToAssetPath(scenePath);
                
                // Only include scenes under Assets/Scenes path and exclude "Managers" scene
                if (fullPath.StartsWith("Assets/Scenes/") && !fullPath.Contains("/Managers"))
                {
                    string sceneName = System.IO.Path.GetFileNameWithoutExtension(fullPath);
                    availableScenes.Add(sceneName);
                }
            }
            
            Debug.Log($"Auto-populated {availableScenes.Count} scenes");
        }
        #endif
        #endregion
    }
}
