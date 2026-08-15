# Using LaniakeaCode.MapManagement

This document explains how to use the new MapManagement system with practical examples.

## Overview

The MapManagement system provides a tilemap-based approach to scene management that integrates seamlessly with existing Unity systems. It allows for:
- Additive scene loading
- Integration with 3D perspective camera setups
- Multi-layer tilemap support (ground, decoration, foreground)
- Coordinate conversion between world and tile positions

## Getting Started

### 1. Create a Map Asset

1. Right-click in the Project window
2. Create → Map Management → Map Asset
3. Configure the asset with:
   - Map name
   - Tilemap references (ground, decoration, foreground)
   - Scene names to load
   - Camera settings

### 2. Set Up Your Scene

1. Create a GameObject in your scene
2. Add the `SceneMapManager` component
3. Assign the `SceneMapManager` to your `SceneInstantiate` script if using that system

## Example Usage

### Basic Map Loading

```csharp
// Load a map using SceneMapManager directly
SceneMapManager mapManager = FindObjectOfType<SceneMapManager>();
if (mapManager != null)
{
    mapManager.LoadScene("Level1");
}
```

### Using MapLoader

```csharp
// Create a MapLoader component and load from an asset
public class LevelLoader : MonoBehaviour
{
    public MapAsset levelMap;
    private MapLoader mapLoader;
    
    void Start()
    {
        mapLoader = GetComponent<MapLoader>();
        if (mapLoader != null && levelMap != null)
        {
            mapLoader.LoadMapFromAsset(levelMap);
        }
    }
}
```

### Creating a Simple Level

```csharp
// Create a basic map with tilemaps
public class SimpleLevelSetup : MonoBehaviour
{
    public SceneMapManager sceneMapManager;
    public Tilemap groundTilemap;
    public Tilemap decorationTilemap;
    
    void Awake()
    {
        // Set up the tilemaps
        if (sceneMapManager != null)
        {
            sceneMapManager.groundTilemap = groundTilemap;
            sceneMapManager.decorationTilemap = decorationTilemap;
            
            // Initialize the map
            sceneMapManager.InitializeMap();
            sceneMapManager.SetupMapWithTilemaps();
        }
    }
}
```

## Integration with Existing Systems

### With FSM System

```csharp
public class PlayerController : Entity
{
    void Start()
    {
        // Your existing FSM initialization
        stateMachine = new FSM(this);
        
        // Initialize map manager if needed
        SceneMapManager mapManager = SceneMapManager.Instance;
        if (mapManager != null)
        {
            // Use map data for positioning or other logic
            mapManager.InitializeMap();
        }
    }
}
```

### With Events System

```csharp
public class MapTransitionEvent : MonoBehaviour
{
    public BaseGameEvent<string> onMapLoaded;
    
    void OnMapLoaded(string mapName)
    {
        // Trigger event when a map is loaded
        if (onMapLoaded != null)
        {
            onMapLoaded.Raise(mapName);
        }
    }
}
```

## Best Practices

1. **Always use SceneMapManager as the main entry point** for all map operations
2. **Create MapAssets** for each level or environment to maintain consistency
3. **Keep tilemap layers organized** and named appropriately
4. **Use the MapLoader** for complex loading scenarios involving multiple assets
5. **Test integration with existing systems** before deploying to production

## Troubleshooting

### Common Issues

1. **SceneMapManager not found**: Make sure you have a GameObject with SceneMapManager component in your scene
2. **Tilemaps not showing**: Ensure tilemaps are assigned to the SceneMapManager and properly configured
3. **Camera positioning issues**: Verify camera settings in MapAsset or SceneMapManager

### Debugging Tips

1. Use the `MapManagerTest` component to run system diagnostics
2. Check the Unity Console for error messages
3. Verify all references are correctly assigned in the Inspector
4. Ensure proper ordering of initialization (SceneMapManager should be initialized before loading scenes)
```