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
        private ScriptableObject graphEventAsset;
        private ObjectField objectField;

        public ScriptableObject GraphEventAsset { get => graphEventAsset; set => graphEventAsset = value; }

        public GraphEventNode()
        {
            Debug.Log("<color=green>GraphEventNode created with default constructor.</color>");
        }

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
                objectType = typeof(ScriptableObject),
                allowSceneObjects = false,
                value = graphEventAsset
            };

            objectField.RegisterValueChangedCallback(value =>
            {
                graphEventAsset = objectField.value as ScriptableObject;
                Debug.Log("<color=blue>GraphEvent asset updated:</color> " + graphEventAsset);
            });
            objectField.SetValueWithoutNotify(graphEventAsset);

            mainContainer.Add(objectField);

            Debug.Log("<color=green>GraphEventNode created at position: </color>" + _position);
        }

        public override void LoadValueIntoField()
        {
            objectField.SetValueWithoutNotify(graphEventAsset);
            Debug.Log("<color=blue>GraphEventNode values loaded into field.</color>");
        }
    }
}