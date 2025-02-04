using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;

namespace LaniakeaCode.GraphSystem
{
    /// <summary>
    /// Graph Editor Window That Manages The Display and Edit Of The Graph Tree
    /// </summary>
    /// 
    public class GraphEditorWindow : EditorWindow
    {
        #region Private
        //Label For the Graph
        private Label graphTreeName;

        //Actual View
        private GraphTreeView graphTreeView;

        //Reference To SavenLoad Utility
        private GraphSaveUtility saveUtility;
        private ToolbarButton saveButton;
        private ToolbarButton loadButton;

        //Dropdown for the Language
        private ToolbarMenu languageToolbar;

        //Language Default Init
        private LanguageType selectedLanguage = LanguageType.English;

        //Style Sheet Name Reference
        private string styleSheetName = "Graph Tree";
        private LoadCSV loadCSV;
        private List<string> csvFiles;
        private string selectedFile;
        #endregion

        //Selected Graph
        public GraphTree currentGraphTree { get; set; }

        /// <summary>
        /// Current Language in the Editor Window
        /// </summary>
        public LanguageType SelectedLanguage { get => selectedLanguage; set => selectedLanguage = value; }
        

        private void OnEnable()
        {
            GenerateGraphTreeView();
            GenerateToolbar();
            Load();
        }
        private void OnDisable()
        {
            rootVisualElement.Remove(graphTreeView);
        }

        /// <summary>
        /// Show Graph Tree Editor Window On Editor Callback
        /// </summary>
        /// <param name="instanceId">The Instance Id Of The Asset Opened </param>
        /// <param name="line">Required but not openly used</param>
        /// <returns>A bool if Graph Editor Window is shown</returns>
        /// 
        [OnOpenAsset(1)]
        public static bool ShowWindow(int instanceId, int line)
        {
            UnityEngine.Object item = EditorUtility.InstanceIDToObject(instanceId);
            //Check The Type Of The Asset Opened
            if (item is GraphTree)
            {
                var window = (GraphEditorWindow)GetWindow(typeof(GraphEditorWindow));
                window.titleContent = new GUIContent("Graph Editor");
                //Reading the object as a Graph Tree
                window.currentGraphTree = item as GraphTree;
                //Setting This Window Minimum Size
                window.minSize = new Vector2(640, 360);
                window.Load();
                return true;
            }
            //Something Failed
            return false;
        }

        /// <summary>
        /// Generate a Graph Tree View via the Unity Graph Experimental Class
        /// </summary>
        private void GenerateGraphTreeView()
        {
            graphTreeView = new GraphTreeView(this) { name = "Graph" };
            //Setting The Graph Tree View To Stretch With Parent Window
            graphTreeView.StretchToParentSize();
            //Adding The Graph Tree View To Parent View
            rootVisualElement.Add(graphTreeView);
            saveUtility = new GraphSaveUtility(graphTreeView);
        }

        /// <summary>
        /// Generate Toolbar Object to be Attached To This Window
        /// </summary>
        /// 
        private void GenerateToolbar()
        {
            //Caching a new Toolbar
            var toolbar = new Toolbar();
            //Getting The Style Sheet
            StyleSheet styleSheet = Resources.Load<StyleSheet>(styleSheetName);
            rootVisualElement.styleSheets.Add(styleSheet);

            //Generating ToolBar Elements

            //Name
            graphTreeName = new Label("");
            graphTreeName.AddToClassList("graphTreeName");

            //Save
            saveButton = new ToolbarButton(() => { Save(); }) { text = "Save" };
            saveButton.AddToClassList("saveButton");
            //Load
            loadButton = new ToolbarButton(() => { Load(); }) { text = "Load" };
            loadButton.AddToClassList("loadButton");

            //Language
            languageToolbar = new ToolbarMenu();
            languageToolbar.AddToClassList("toolbarLanguage");

            //Caching The Language Enums As An Array
            foreach (LanguageType language in (LanguageType[])Enum.GetValues(typeof(LanguageType)))
            {
                //Adding The Action On The Type Selected
                languageToolbar
                    .menu
                    .AppendAction(language.ToString(), new Action<DropdownMenuAction>(x => SelectLanguageFromDropDown(language)));
            }

            //Adding Elements To The Toolbar
            toolbar.Add(graphTreeName);
            toolbar.Add(languageToolbar);
            toolbar.Add(saveButton);
            // Add CSV Dropdown
        loadCSV = new LoadCSV();
        csvFiles = loadCSV.GetAllCSVFiles();
        selectedFile = csvFiles.FirstOrDefault();

        var csvDropdown = new PopupField<string>("Select CSV", csvFiles, 0);
        csvDropdown.RegisterValueChangedCallback(evt =>
        {
            selectedFile = evt.newValue;
        });

        var loadCsvButton = new ToolbarButton(() =>
        {
            loadCSV.Load(selectedFile);
            Debug.Log($"<color=green>Loaded CSV: {selectedFile}</color>");
        })
        { text = "Load CSV" };

        toolbar.Add(csvDropdown);
        toolbar.Add(loadCsvButton);
            // toolbar.Add(loadButton);

            //Adding the Toolbar to the Window
            rootVisualElement.Add(toolbar);
        }

        /// <summary>
        /// Load The Graph Tree
        /// </summary>
        /// 
        private void Load()
        {
            if (currentGraphTree != null)
            {
                graphTreeName.text = "Name:   " + currentGraphTree.name;
                SelectLanguageFromDropDown(LanguageType.English);
                saveUtility.Load(currentGraphTree);
            }
        }

        /// <summary>
        /// Save The Graph Tree
        /// </summary>
        /// 
        private void Save()
        {
            if (currentGraphTree != null)
            {
                saveUtility.Save(currentGraphTree);
            }
        }

        /// <summary>
        /// Change The Language Between Avaialable
        /// </summary>
        /// 
        private void SelectLanguageFromDropDown(LanguageType language)
        {
            languageToolbar
                .text = "Language:   " + language
                .ToString();

            selectedLanguage = language;

            graphTreeView
                .ReloadLanguage();
        }
    }
}