using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

namespace LaniakeaCode.GraphSystem
{
    /// <summary>
    /// Represents the starting node in the dialogue graph.
    /// </summary>
    public class StartNode : BaseNode
    {
        public StartNode()
        {
            Debug.Log("<color=green>StartNode created with default constructor.</color>");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StartNode"/> class.
        /// </summary>
        /// <param name="position">The position of the node.</param>
        /// <param name="_editorWindow">The editor window.</param>
        /// <param name="_graphTreeView">The graph tree view.</param>
        public StartNode(Vector2 position, GraphEditorWindow _editorWindow, GraphTreeView _graphTreeView)
        {
            editorWindow = _editorWindow;
            graphTreeView = _graphTreeView;

            title = "Start";

            SetPosition(new Rect(position, defaultNodeSize));
            //Referencing Field, not Property
            nodeGUID = Guid.NewGuid().ToString();

            AddOutputPort("Output", Port.Capacity.Single);

            RefreshExpandedState();
            RefreshPorts();

            Debug.Log("<color=green>StartNode created at position: </color>" + position);
        }
    }
}