using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using LaniakeaCode.Utilities;
public class BarrelEntityEditor : EditorWindow {
    [MenuItem("LaniakeaCode/BarrelEntityEditor")]
    static void OpenWindow() => GetWindow<BarrelEntityEditor>("BarrelTool");

    #region Selections
    int entitySelected = 0;
    int stateSelected = 0;
    #endregion

    #region Section Rects
    Rect viewSection;
    Rect infoSection;
    Rect headerSection;
    //ButtonsAndStuff
    Rect actionSection;
    Rect buttonSection;
    #endregion

    #region Section colors and textures
    Color headerSectionColor;
    Color viewSectionColor;
    Color infoSectionColor;
    Color actionSectionColor;
    Color buttonSectionColor;

    Texture2D headerSectionTexture;
    Texture2D infoSectionTexture;
    Texture2D actionSectionTexture;
    Texture2D viewSectionTexture;
    Texture2D buttonSectionTexture;
    #endregion

    #region WORKING DATAS
    EntityData workEntity;
    List<EntityData> entityData;
    ScriptableStateData stateData;
    List<ScriptableStateData> possibleStates;

    public EntityData WorkEntity { get => workEntity; set => workEntity = value; }
    #endregion
    private void OnGUI()
    {
        SelectColors();
        DrawLayouts();
        DrawHeader();
        DrawViewSection();
        DrawActionSection();
        DrawInfoSection();
    }

    private void SelectColors()
    {
        headerSectionColor = new Color(54f / 255f, 6f / 255f, 45f / 255f, 1f);
        viewSectionColor = new Color(14f / 255f, 47f / 255f, 50f / 255f, 1f);
        infoSectionColor = new Color(24f / 255f, 57f / 255f, 50f / 255f, 1f);
        actionSectionColor = new Color(24f / 255f, 37f / 255f, 85f / 255f, 1f);
        buttonSectionColor = new Color(28f / 255f, 37f / 255f, 85f / 255f, 1f);
    }

    private void OnEnable()
    {
        AddAssets();
        SelectColors();
        InitTexture();
    }
    private void AddAssets()
    {
        WorkEntity = (EntityData)ScriptableObject.CreateInstance(typeof(EntityData));
        entityData = new List<EntityData>();
        var allObjectEntity = AssetDatabase.FindAssets("t:EntityData");
        foreach (var guid in allObjectEntity)
        {
            EntityData entity = AssetDatabase.LoadAssetAtPath<EntityData>(AssetDatabase.GUIDToAssetPath(guid));
            entityData.Add(entity);
        }
        entityData.ForEach(x => ScriptableObject.CreateInstance(typeof(EntityData)));

        stateData = (ScriptableStateData)ScriptableObject.CreateInstance(typeof(ScriptableStateData));
        possibleStates = new List<ScriptableStateData>();
        var allStatesToBeChosen = AssetDatabase.FindAssets("t:ScriptableStateData");
        foreach(var state in allStatesToBeChosen)
        {
            ScriptableStateData stateData = AssetDatabase.LoadAssetAtPath<ScriptableStateData>(AssetDatabase.GUIDToAssetPath(state));
            possibleStates.Add(stateData);
        }
        possibleStates.ForEach(x => ScriptableObject.CreateInstance(typeof(ScriptableStateData)));
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

        buttonSectionTexture = new Texture2D(1, 1);
        buttonSectionTexture.SetPixel(0, 0, buttonSectionColor);
        buttonSectionTexture.Apply();
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
        GUI.DrawTexture(buttonSection, buttonSectionTexture);
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

        buttonSection.x = viewSection.x;
        buttonSection.y = viewSection.y;
        buttonSection.width = Screen.width;
        buttonSection.height = Screen.height - actionSection.height;
    }

    void DrawHeader()
    {
        GUILayout.BeginArea(headerSection);
        //GUILayout.Label("|Headers");
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
        EditorGUILayout.BeginHorizontal();
        GUILayout.BeginVertical();
        GUILayout.Label("Prefab");
        if (WorkEntity.prefab == null)
        {
            EditorGUILayout.HelpBox("Missing |Prefab|", MessageType.Warning);
        }

        WorkEntity.prefab = (GameObject)EditorGUILayout.ObjectField(WorkEntity.prefab, typeof(GameObject), false);
        Debug.Log("WorkEntity : " + workEntity);
        GUILayout.Label("|Info");

        if (GUILayout.Button("Edit!"))
        {
            BarrelSettingsEditor.OpenWindow(this, WorkEntity);
        }
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
    void DrawActionSection()
    {
        GUILayout.BeginArea(actionSection);
        GUILayout.BeginHorizontal();
        GUILayout.Label("| EntityType : ");
        WorkEntity.entityType = (EntityType)EditorGUILayout.EnumPopup(WorkEntity.entityType);
        GUILayout.BeginVertical();
        GUIContent content = new GUIContent("| Entity Name : ");
        entitySelected = EditorGUILayout.Popup(content, entitySelected, entityData.ConvertAll(x => x.name).ToArray());
        WorkEntity = entityData[entitySelected];
        GUILayout.EndVertical();
        GUIContent newContent = new GUIContent("| Entity State : ");
        stateSelected = EditorGUILayout.Popup(newContent, stateSelected, possibleStates.ConvertAll(x => x.name).ToArray());
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        BarrelSettingsEditor.SelectDataToBeShown(WorkEntity, possibleStates[stateSelected]);
    }
}