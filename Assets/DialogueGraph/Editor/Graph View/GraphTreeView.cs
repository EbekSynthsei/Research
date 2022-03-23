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
    public class GraphTreeView : GraphView
    {
        private GraphEditorWindow editorWindow;
        private NodeSearchWindow searchWindow;
        public GraphTreeView(GraphEditorWindow _editorWindow)
        {
            editorWindow = _editorWindow;
            //Setting The Style
            styleSheets.Add(Resources.Load<StyleSheet>("Graph Tree"));
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            //Adding Elements Manipulators

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(new FreehandSelector());

            //Adding A Grid 
            var grid = new GridBackground();
            //Inserting Through Visual Element Base Method
            Insert(0, grid);
            grid.StretchToParentSize();

            AddSearchWindow();
        }
        private void AddSearchWindow()
        {
            searchWindow = ScriptableObject
                .CreateInstance<NodeSearchWindow>();
            searchWindow
                .Init(editorWindow, this);

            //Action Request On The Mouse Position Of A New SearchWindow
            //
            nodeCreationRequest = context => SearchWindow
            .Open(new SearchWindowContext(context.screenMousePosition), searchWindow);
        }

        public void LanguageReload()
        {
            List<DialogueNode> graphNodes = nodes
                .ToList()
                .Where(node => node is DialogueNode)
                .Cast<DialogueNode>()
                .ToList();

            foreach (DialogueNode graphNode in graphNodes)
            {
                graphNode
                    .ReloadLanguage();
            }
        }

        /// <summary>
        /// Methods Creating the Nodes
        /// 
        /// </summary>
        /// <param name="_pos"></param>
        /// <returns></returns>
        /// 
        #region CreateNode

        public StartNode CreateStartNode(Vector2 _pos)
        {
            StartNode tmp = new StartNode(_pos, editorWindow, this);
            return tmp;
        }
        public DialogueNode CreateUINode(Vector2 _pos)
        {
            DialogueNode tmp = new DialogueNode(_pos, editorWindow, this);
            return tmp;
        }
        public GraphEventNode CreateGraphEventNode(Vector2 _pos)
        {
            GraphEventNode tmp = new GraphEventNode(_pos, editorWindow, this);
            return tmp;
        }
        public EndNode CreateEndNode(Vector2 _pos)
        {
            EndNode tmp = new EndNode(_pos, editorWindow, this);
            return tmp;
        }
        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            List<Port> compatiblePorts = new List<Port>();
            Port startPortView = startPort;

            ports
                .ForEach((port) =>
                {
                    Port portView = port;
                    if (startPortView != portView && startPortView.node != portView.node && startPortView.direction != port.direction)
                    {
                        compatiblePorts
                        .Add(port);
                    }
                });
            return compatiblePorts;
        }

        #endregion
    }
}