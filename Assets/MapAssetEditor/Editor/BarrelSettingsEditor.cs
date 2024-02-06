using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using LaniakeaCode.Utilities;
using System;

/// <summary>
/// Should actually save changes to dirty objects edited
/// </summary>
public class BarrelSettingsEditor : EditorWindow
{
    static EntityData entityTypeSettings;
    static BarrelSettingsEditor window;
    static BarrelEntityEditor editor;
    public static void OpenWindow(BarrelEntityEditor editor, EntityData setting)
    {
        entityTypeSettings = setting;
        window = (BarrelSettingsEditor)GetWindow(typeof(BarrelSettingsEditor));
        window.minSize = new Vector2(480, 270);
        window.Show();
    }
    public static void SelectDataToBeShown(EntityData workEntity, ScriptableStateData stateData)
    {
        LoadAssets(workEntity, stateData);
        
    }

    private static void LoadAssets(EntityData workEntity, ScriptableStateData stateData)
    {
        entityTypeSettings = workEntity;
        string baseDataPath = "Assets/MapAssetEditor/Resources/barrelData/";
        string basePrefabFolder = "Assets/MapAssetEditor/Resources/prefab/barrelPrefab/";
        string dataPath = "base/";
        string prefabPath;
        string newPrefabPath;
        switch (entityTypeSettings.entityType)
        {

            case EntityType.Player:
                DrawPlayer();
                break;
            case EntityType.Enemy:
                DrawEnemy();
                break;
            case EntityType.NPC:
                break;
            case EntityType.Barrel:
                DrawBarrel();
                break;
            case EntityType.Ground:
                break;
            case EntityType.Projectile:
                break;
            case EntityType.Base:
            default:
                break;
        }
    }

    private void OnEnable()
    {
    }
    private void OnGUI()
    {
        editor = GetWindow<BarrelEntityEditor>();
        GUILayout.BeginHorizontal();
        GUILayout.Label("|Type : ");
        GUILayout.TextArea(editor.WorkEntity.entityType.ToString());
        GUILayout.Label("|Max Health : ");
        GUILayout.TextArea(editor.WorkEntity.maxHealth.ToString());
        GUILayout.Label("|Target LayerMask");
        GUILayout.TextArea(editor.WorkEntity.whatIsTarget.value.ToString());
        GUILayout.Label("|Ground LayerMask");
        GUILayout.TextArea(editor.WorkEntity.whatIsGround.value.ToString());
        GUILayout.EndHorizontal();
    }
    public static void DrawPlayer()
    {
    }
    static void DrawEnemy()
    {
        GUILayout.Label("Enemy");
    }
    static void DrawBarrel()
    {
        GUILayout.Label("Barrel");
    }
    
}
