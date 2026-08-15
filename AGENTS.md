# Unity 2D Platformer Project - AI Agent Instructions

This document provides guidance for AI coding agents working with this Unity 2D platformer project. The project is built on Unity 6 (6.5 LTS) with a focus on narrative-driven gameplay inspired by "The Last Night" and "Detroit: Become Human".

## Project Architecture Overview

### Core Systems
- **FSM Player Controller**: Implements a complex state machine pattern following Bardent's "2D Platformer Player Controller" tutorial
- **ScriptableObject Events**: Uses Ryan Hipple's architecture pattern from Unite Austin 2017
- **Dialogue System**: Based on Kasper Game Dev's "Dialogue Editor Tutorial" with CSV-based localization
- **Camera System**: Combines Cinemachine with custom CameraManager

### Key Components and Patterns

#### FSM Architecture
- `Entity` → `CORE/Core` (collision senses, movement) → `FSM` → `State` → `O_State` (player-specific) → concrete states
- Input is centralized in `InputManager` Singleton using Unity Input System
- States follow the hierarchy: `State` → `O_State` → specific state implementations

#### ScriptableObject Events
- Events are implemented as ScriptableObjects inheriting from `BaseGameEvent<T>`
- Used for communication between game systems without direct references
- Examples: `GraphEvent`, `InteractableData`

#### Dialogue System
- `GraphTree` (ScriptableObject) with `NodeData`/`NodeLinkData`/`DialogueNodePort`
- Editor based on `UnityEditor.Experimental.GraphView`
- CSV-based saving/loading for multilingual support

#### Interactable System
- `IInteractable` interface defines the contract for interactive objects
- `InteractableBase` class implements core interaction logic with:
  - Collider-based detection and focus management
  - Visual tooltip display
  - Dialogue integration via `GraphTree`
  - Scriptable Actions for complex interactions
  - Event triggering through `ScriptableEvent`
  - State management for multiple use and hold interactions

### Directory Structure and Naming Conventions

```
Assets/
├── GameBase/                 # Core gameplay systems
│   ├── FSM/                  # Finite State Machine components
│   ├── Player/               # Player-specific components
│   ├── CORE/                 # Core entity components
│   ├── MNG/                  # Managers (InputManager, etc.)
│   ├── Weapon/               # Weapon system
│   ├── Interfaces/           # Interaction interfaces and base classes
│   ├── MapManagement/        # Map management system with tilemap integration
│   └── Utilities/            # Utility classes
├── EVS/                      # Events and ScriptableObjects
└── Scenes/                   # Unity scenes
```

### Key Files to Understand

- `Assets/GameBase/FSM/Entity.cs` - Base entity with core components
- `Assets/GameBase/Player/Player.cs` - Player-specific implementation
- `Assets/GameBase/FSM/FSM.cs` - Main state machine logic
- `Assets/GameBase/MNG/InputManager.cs` - Input handling system
- `Assets/GameBase/MapManagement/SceneMapManager.cs` - Scene management with tilemap integration
- `Assets/EVS/GameEvents/Events/GraphEvent.cs` - Dialogue event system
- `Assets/GameBase/Interfaces/IInteractable.cs` - Interface for interactive objects
- `Assets/GameBase/Interfaces/InteractableBase.cs` - Base class implementing interaction logic
- `Assets/GameBase/ScriptableObjects/InteractionData/InteractableData.cs` - ScriptableObject data for interactions

### Development Guidelines

1. **Follow existing patterns**: All new systems should use the established FSM, ScriptableObject events, dialogue system, and interaction system patterns
2. **Maintain consistency**: Use PascalCase for public classes/methods, camelCase for private fields
3. **Respect architectural boundaries**: Don't bypass core components in state transitions
4. **Use enum values**: For forcing valid states and configurations
5. **Centralize data**: Entity data should be centralized behind abstractions
6. **Refactor with care**: The existing FSM implementation is almost complete, focus on minor fixes
7. **Integrate properly with interactable system**: When creating new interactive objects, inherit from `InteractableBase` and configure through `InteractableData` ScriptableObject
8. **Use MapManagement namespace**: For scene/map management, use the new `LaniakeaCode.MapManagement` namespace that integrates tilemap systems with 3D perspective camera setups

### Unity 6 Specific Considerations

- This project targets Unity 6.5 LTS
- Use `context7` MCP server to verify API changes and breaking changes
- When making structural changes, always verify compatibility with Unity 6 APIs
- Be aware of any Input System or GraphView API differences in Unity 6
- The interaction system relies heavily on Collider2D and Trigger events which work consistently in Unity 6
- Tilemap integration is now supported through the new `LaniakeaCode.MapManagement` namespace that allows combining 2D tilemaps with 3D perspective camera systems

### Testing Commands

To build/test the project:
1. Open Unity Editor
2. Build using standard Unity build pipeline (no specific command required)
3. Run tests via Unity's Test Runner

### MCP Integration

The following MCP tools are available for development:
- `unity-mcp`: For modifying assets, scenes, and components in Unity Editor
- `pixel-mcp`: For programmatic pixel art generation/editing
- `aseprite-mcp`: For direct .aseprite file editing
- `context7`: For checking official Unity documentation and API changes
- `duckduckgo`: For web research on breaking changes or external references

When applying code changes, use `unity-mcp` server for any modifications that would normally require the Unity Editor.