using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using LaniakeaCode.Utilities;
public class BarrelEntityEditor : EditorWindow {
    [MenuItem("LaniakeaCode/BarrelEntityEditor")]
    static void OpenWindow() => GetWindow<BarrelEntityEditor>("BarrelTool");

    Rect viewSection;
    Rect infoSection;
    Rect headerSection;
    //ButtonsAndStuff
    Rect actionSection;

    Rect playerSection;
    Rect enemySection;
    Rect barrelSection;

    Color headerSectionColor;
    Color viewSectionColor;
    Color infoSectionColor;
    Color actionSectionColor;

    Texture2D headerSectionTexture;
    Texture2D infoSectionTexture;
    Texture2D actionSectionTexture;
    Texture2D viewSectionTexture;
    Texture2D playerSectionTexture;

    EntityData workEntity;
    List<EntityData> entityData;

    private void OnGUI()
    {
        SelectColors();
        DrawLayouts();
        DrawHeader();
        DrawViewSection();
        DrawInfoSection();
        DrawActionSection();
    }

    private void SelectColors()
    {
        headerSectionColor = new Color(54f / 255f, 6f / 255f, 45f / 255f, 1f);
        viewSectionColor = new Color(14f / 255f, 47f / 255f, 50f / 255f, 1f);
        infoSectionColor = new Color(24f / 255f, 57f / 255f, 50f / 255f, 1f);
        actionSectionColor = new Color(24f / 255f, 37f / 255f, 85f / 255f, 1f);
    }

    private void OnEnable()
    {
        AddAssets();
        SelectColors();
        InitTexture();
    }
    private void AddAssets()
    {
        workEntity = (EntityData)ScriptableObject.CreateInstance(typeof(EntityData));
        entityData = new List<EntityData>();
        var allObjectEntity = AssetDatabase.FindAssets("t:EntityData");
        foreach (var guid in allObjectEntity)
        {
            EntityData entity = AssetDatabase.LoadAssetAtPath<EntityData>(AssetDatabase.GUIDToAssetPath(guid));
            entityData.Add(entity);
        }
        entityData.ForEach(x => ScriptableObject.CreateInstance(typeof(EntityData)));
    }
    void InitTexture()
    {
        headerSectionTexture = new Texture2D(1, 1);
        headerSectionTexture.SetPixel(0, 0, headerSectionColor);
        headerSectionTexture.Apply();
        
        viewSectionTexture = new Texture2D(1, 1);
        viewSectionTexture.SetPixel(0, 0, viewSectionColor);
        viewSectionTexture.Apply();

        infoSectionTexture = new Texture2D(1, 1);
        infoSectionTexture.SetPixel(0, 0, infoSectionColor);
        infoSectionTexture.Apply();

        actionSectionTexture = new Texture2D(1, 1);
        actionSectionTexture.SetPixel(0, 0, actionSectionColor);
        actionSectionTexture.Apply();

        playerSectionTexture = new Texture2D(1, 1);
        playerSectionTexture.SetPixel(0, 0, actionSectionColor);
        playerSectionTexture.Apply();
    }
    void DrawLayouts()
    {
        DeclareSections();

        DrawSections();
    }

    private void DrawSections()
    {
        GUI.DrawTexture(headerSection, headerSectionTexture);
        GUI.DrawTexture(viewSection, viewSectionTexture);
        GUI.DrawTexture(infoSection, infoSectionTexture);
        GUI.DrawTexture(actionSection, actionSectionTexture);
    }

    private void DeclareSections()
    {
        headerSection.x = 0;
        headerSection.y = 0;
        headerSection.width = Screen.width;
        headerSection.height = 50;

        viewSection.x = 0;
        viewSection.y = 50;
        viewSection.width = Screen.width / 3f;
        viewSection.height = Screen.height;

        infoSection.x = Screen.width / 3f;
        infoSection.y = 50;
        infoSection.width = (Screen.width / 3f) * 2;
        infoSection.height = Screen.height - 50;

        actionSection.x = 0;
        actionSection.y = Screen.height - 50;
        actionSection.width = Screen.width;
        actionSection.height = 50;

        playerSection.x = viewSection.x;
        playerSection.y = viewSection.y;
        playerSection.width = Screen.width;
        playerSection.height = Screen.height - actionSection.height;

        playerSection.x = viewSection.x;
        playerSection.y = viewSection.y;
        playerSection.width = Screen.width;
        playerSection.height = Screen.height - actionSection.height;
    }

    void DrawHeader()
    {
        GUILayout.BeginArea(headerSection);
        GUILayout.Label("|Headers");
        GUILayout.EndArea();
    }
    void DrawViewSection()
    {
        GUILayout.BeginArea(viewSection);
        GUILayout.Label("|Preview");
        GUILayout.EndArea();
    }
    void DrawInfoSection()
    {
        GUILayout.BeginArea(infoSection);
        GUILayout.Label("|Info");
        GUILayout.BeginHorizontal();
        GUILayout.Label("|Type : ");
        GUILayout.TextArea(workEntity.entityType.ToString());
        GUILayout.Label("|Max Health : ");
        GUILayout.TextArea(workEntity.maxHealth.ToString());
        GUILayout.Label("|Target LayerMask");
        GUILayout.TextArea(workEntity.whatIsTarget.value.ToString());
        GUILayout.Label("|Ground LayerMask");
        GUILayout.TextArea(workEntity.whatIsGround.value.ToString());
        GUILayout.EndArea();
    }
    void DrawActionSection()
    {
        GUILayout.BeginArea(actionSection);
        GUILayout.BeginHorizontal();
        GUILayout.Label("| EntityType");
        workEntity.entityType = (EntityType)EditorGUILayout.EnumPopup(workEntity.entityType);
        SelectDataToBeShown(workEntity.entityType);
        GUILayout.BeginVertical();
        GUILayout.Label("|");
        ShowList();
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    private void ShowList()
    {
        
    }

    private void SelectDataToBeShown(EntityType entityType)
    {
        switch (entityType)
        {
            case EntityType.Base:
                break;
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
            default:
                break;
        }

        void DrawPlayer()
    {
        GUILayout.BeginArea(playerSection);
        if(GUILayout.Button("Create!", GUILayout.Height(40)))
            {
                BarrelSettingsEditor.OpenWindow(this, workEntity.entityType);
            }
        GUILayout.EndArea();
    }
    void DrawEnemy()
    {
        GUILayout.BeginArea(enemySection);

        GUILayout.EndArea();
    }
    void DrawBarrel()
    {
        GUILayout.BeginArea(barrelSection);

        GUILayout.EndArea();
    }
    }
}