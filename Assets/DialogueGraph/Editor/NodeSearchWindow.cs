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


        //Takes The Reference To The Graph Tree View From This Editor Window
        public void Init(GraphEditorWindow _editorWindow, GraphTreeView _graphView)
        {
            editorWindow = _editorWindow;
            graphView = _graphView;
        }


        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            //Setting The Node and The Tree To be Searched
            List<SearchTreeEntry> tree = new List<SearchTreeEntry>()
        {
            new SearchTreeGroupEntry(new GUIContent("Graph Node"), 0),
            new SearchTreeGroupEntry(new GUIContent("Graph Tree"),1),
            AddNodeSearch("Start Node", new StartNode()),
            AddNodeSearch("Dialogue Node", new DialogueNode()),
            AddNodeSearch("Graph Event Node", new GraphEventNode()),
            AddNodeSearch("End Node", new EndNode()),
        };


            return tree;
        }

        private SearchTreeEntry AddNodeSearch(string _name, BaseNode _baseNode)
        {
            SearchTreeEntry tmp = new SearchTreeEntry(new GUIContent(_name))
            {
                level = 2,
                userData = _baseNode
            };
            return tmp;
        }

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

        //Checks For Node Types To Instantiate Them At Mouse Position
        private bool CheckForNodeType(SearchTreeEntry _searchTreeEntry, Vector2 _pos)
        {
            switch (_searchTreeEntry.userData)
            {

                case StartNode node:
                    graphView.AddElement(graphView.CreateStartNode(_pos));
                    return true;
                case DialogueNode node:
                    graphView.AddElement(graphView.CreateDialogueNode(_pos));
                    return true;
                case GraphEventNode node:
                    graphView.AddElement(graphView.CreateGraphEventNode(_pos));
                    return true;
                case EndNode node:
                    graphView.AddElement(graphView.CreateEndNode(_pos));
                    return true;
                default:
                    break;
            }
            return false;
        }
    }
}