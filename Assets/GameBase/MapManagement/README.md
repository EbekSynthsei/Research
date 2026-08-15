# LaniakeaCode.MapManagement

This namespace provides a comprehensive map management system that integrates Unity's Tilemap system with the existing 3D perspective camera setups.

## Overview

The MapManagement system allows for:
- Tilemap-based scene management
- Integration with existing 3D perspective camera systems
- Additive scene loading
- Camera positioning based on tilemaps
- Support for multiple tilemap layers (ground, decoration, foreground)

## Key Components

### MapManager.cs
Base class for map management with core functionality:
- Map initialization
- Scene loading methods
- World/tile coordinate conversion
- Tile retrieval from positions

### SceneMapManager.cs
Extends MapManager with scene-specific functionality:
- Integration with Unity's SceneManager
- Additive loading support
- Camera positioning for 3D perspective
- Multi-tilemap support
- Scene unload functionality

### MapData.cs
Data class for storing map information:
- Tilemap references
- Scene names
- Camera settings
- Lighting settings

### MapLoader.cs
Handles loading and managing maps:
- Loading from MapData or ScriptableObject assets
- Unloading maps
- Integration with SceneMapManager

### MapAsset.cs
ScriptableObject asset for defining map data:
- Create new map assets via the Unity Editor
- Store all relevant map information in a single asset

## Usage

1. **Create a Map Asset**: Right-click in Project window → Create → Map Management → Map Asset
2. **Configure Tilemaps**: Assign your ground, decoration, and foreground tilemaps to the MapAsset
3. **Set up SceneMapManager**: Attach SceneMapManager to a GameObject in your scene
4. **Load the Map**: Use MapLoader to load maps or directly call SceneMapManager methods

## Integration with Existing Systems

The MapManagement system integrates seamlessly with:
- Existing FSM architecture (Entity, FSM, State management)
- ScriptableObject events system
- Dialogue system
- InputManager and camera systems
- Interactable system

## Best Practices

1. **Use SceneMapManager as the main entry point** for map operations
2. **Create MapAssets** for each level or environment
3. **Keep tilemap layers organized** (ground, decoration, foreground)
4. **Use the MapLoader** to handle complex loading/unloading scenarios
5. **Integrate with existing events** for map transition notifications