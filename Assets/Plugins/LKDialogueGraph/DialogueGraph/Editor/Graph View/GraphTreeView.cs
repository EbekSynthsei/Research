using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

namespace LaniakeaCode.GraphSystem
{
    /// <summary>
    /// View Manager that connects what is executed on the window.
    /// It uses the Graph View from Unity, so could be broke
    /// </summary>
    public class GraphTreeView : GraphView
    {
        //Style Sheet Name Reference
        private string styleSheetName = "Graph Tree";

        //Actual Editor Window
        private GraphEditorWindow editorWindow;

        //Drop Down Window to create Nodes
        private NodeSearchWindow searchWindow;

        /// <summary>
        /// Constructor Adding Funcionality To The Window Passed on
        /// Functionality includes:
        /// Dragging, Selection Dragging, Node Selection
        /// Zooming
        /// A Grid Reference for visual Alignment
        /// </summary>
        /// <param name="_editorWindow"></param>
        public GraphTreeView(GraphEditorWindow _editorWindow)
        {
            editorWindow = _editorWindow;
            //Setting The Style
            styleSheets.Add(Resources.Load<StyleSheet>(styleSheetName));
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            //Adding Elements Manipulators
            //Check Unity References if doesn't work cause Experimental Packages

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

        /// <summary>
        /// Adding a dropdown window to enlist and instantiate nodes
        /// </summary>
        private void AddSearchWindow()
        {
            searchWindow = ScriptableObject
                .CreateInstance<NodeSearchWindow>();

            searchWindow
                .Init(editorWindow, this);

            //Action Request On The Mouse Position Of A New Search Window 
            nodeCreationRequest = context => SearchWindow
            .Open(new SearchWindowContext(context.screenMousePosition), searchWindow);
        }

        /// <summary>
        /// Calls a Reload on every Node to Update the Selected Language
        /// </summary>
        public void ReloadLanguage()
        {
            List<BaseNode> allNodes = nodes
                .ToList()
                .Where(node => node is BaseNode)
                .Cast<BaseNode>()
                .ToList();

            foreach (BaseNode node in allNodes)
            {
                node
                    .ReloadLanguage();
            }
        }

        #region CreateNode

        public StartNode CreateStartNode(Vector2 position)
        {
            return new StartNode(position, editorWindow, this);
        }
        public DialogueNode CreateDialogueNode(Vector2 position)
        {
            return new DialogueNode(position, editorWindow, this);   
        }
        public GraphEventNode CreateGraphEventNode(Vector2 position)
        {
            return new GraphEventNode(position, editorWindow, this);
        }
        public EndNode CreateEndNode(Vector2 position)
        {
            return new EndNode(position, editorWindow, this);
        }

        /// <summary>
        /// Method to avoid:
        /// Self connecting of the Node
        /// In to In or Out to Out connections
        /// </summary>
        /// <param name="startPort"></param>
        /// <param name="nodeAdapter"></param>
        /// <returns>A list of Compatible Ports</returns>
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