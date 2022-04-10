using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace LaniakeaCode.GraphSystem
{
    /// <summary>
    /// The Context Menu Item Interfacing The Creation Functions
    /// </summary>
    /// 
    public class NodeSearchWindow : ScriptableObject, ISearchWindowProvider
    {
        private GraphEditorWindow editorWindow;
        private GraphTreeView graphView;


        /// <summary>
        /// Takes references to the window to initialize on proper window the nodes
        /// </summary>
        /// <param name="_editorWindow"></param>
        /// <param name="_graphView"></param>
        public void Init(GraphEditorWindow _editorWindow, GraphTreeView _graphView)
        {
            editorWindow = _editorWindow;
            graphView = _graphView;
        }

        /// <summary>
        /// A fixed Method to get a dropdown list of items to instance on the window
        /// Can be improved cause now is a fixed list
        /// </summary>
        /// <param name="context"></param>
        /// <returns>A list of possible entries</returns>
        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            List<SearchTreeEntry> tree = new List<SearchTreeEntry>(){
                new SearchTreeGroupEntry(new GUIContent("Graph Node"), 0),
                new SearchTreeGroupEntry(new GUIContent("Graph Tree"),1),
                AddNodeSearch("Start Node", new StartNode()),
                AddNodeSearch("Dialogue Node", new DialogueNode()),
                AddNodeSearch("Graph Event Node", new GraphEventNode()),
                AddNodeSearch("End Node", new EndNode()),
            };


            return tree;
        }

        /// <summary>
        /// Fixed Method To fill the dropdown with the nodes
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_baseNode"></param>
        /// <returns>The dropdown entry window</returns>
        private SearchTreeEntry AddNodeSearch(string _name, BaseNode _baseNode)
        {
            return new SearchTreeEntry(new GUIContent(_name))
            {
                level = 2,
                userData = _baseNode
            };
            
        }

        /// <summary>
        /// On Selection of a Node From The List it gets the position in the window and instantiates a node there.
        /// </summary>
        /// <param name="_searchTreeEntry"></param>
        /// <param name="_context"></param>
        /// <returns></returns>
        public bool OnSelectEntry(SearchTreeEntry _searchTreeEntry, SearchWindowContext _context)
        {
            Vector2 pointerPosition = editorWindow
                .rootVisualElement
                .ChangeCoordinatesTo(editorWindow.rootVisualElement.parent, _context.screenMousePosition - editorWindow.position.position);

            Vector2 graphMousePosition = graphView
                .contentContainer
                .WorldToLocal(pointerPosition);

            return CheckForNodeType(_searchTreeEntry, graphMousePosition);
        }

        /// <summary>
        /// Method switching on Node Types To Instantiate Them At Mouse Position
        /// </summary>
        /// <param name="_searchTreeEntry"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        private bool CheckForNodeType(SearchTreeEntry _searchTreeEntry, Vector2 position)
        {
            switch (_searchTreeEntry.userData)
            {
                case StartNode node:
                    graphView.AddElement(graphView.CreateStartNode(position));
                    return true;
                case DialogueNode node:
                    graphView.AddElement(graphView.CreateDialogueNode(position));
                    return true;
                case GraphEventNode node:
                    graphView.AddElement(graphView.CreateGraphEventNode(position));
                    return true;
                case EndNode node:
                    graphView.AddElement(graphView.CreateEndNode(position));
                    return true;
                default:
                    break;
            }
            return false;
        }
    }
}