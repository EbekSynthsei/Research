using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace LaniakeaCode.GraphSystem
{
    /// <summary>
    /// Represents a node that handles graph events.
    /// </summary>
    public class GraphEventNode : BaseNode
    {
        private GraphEvent graphEvent;
        private ObjectField objectField;

        public GraphEvent GraphEvent { get => graphEvent; set => graphEvent = value; }

        public GraphEventNode()
        {
            Debug.Log("<color=green>GraphEventNode created with default constructor.</color>");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphEventNode"/> class.
        /// </summary>
        /// <param name="_position">The position of the node.</param>
        /// <param name="_editorWindow">The editor window.</param>
        /// <param name="_graphTreeView">The graph tree view.</param>
        public GraphEventNode(Vector2 _position, GraphEditorWindow _editorWindow, GraphTreeView _graphTreeView)
        {
            editorWindow = _editorWindow;
            graphTreeView = _graphTreeView;

            title = "Graph Event";
            SetPosition(new Rect(_position, defaultNodeSize));
            nodeGUID = Guid.NewGuid().ToString();

            AddInputPort("Input", Port.Capacity.Multi);
            AddOutputPort("Output", Port.Capacity.Single);

            objectField = new ObjectField()
            {
                objectType = typeof(GraphEvent),
                allowSceneObjects = false,
                value = graphEvent
            };

            objectField.RegisterValueChangedCallback(value =>
            {
                graphEvent = objectField.value as GraphEvent;
                Debug.Log("<color=blue>GraphEvent updated:</color> " + graphEvent);
            });
            objectField.SetValueWithoutNotify(graphEvent);

            mainContainer.Add(objectField);

            Debug.Log("<color=green>GraphEventNode created at position: </color>" + _position);
        }

        public override void LoadValueIntoField()
        {
            objectField.SetValueWithoutNotify(graphEvent);
            Debug.Log("<color=blue>GraphEventNode values loaded into field.</color>");
        }
    }
}