using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace LaniakeaCode.GraphSystem
{
    /// <summary>
    /// Represents the ending node in the dialogue graph.
    /// </summary>
    public class EndNode : BaseNode
    {
        private EndNodeType endNodeType = EndNodeType.End;
        private EnumField enumField;

        public EndNodeType EndNodeType { get => endNodeType; set => endNodeType = value; }

        public EndNode()
        {
            Debug.Log("<color=green>EndNode created with default constructor.</color>");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EndNode"/> class.
        /// </summary>
        /// <param name="_position">The position of the node.</param>
        /// <param name="_editorWindow">The editor window.</param>
        /// <param name="_graphTreeView">The graph tree view.</param>
        public EndNode(Vector2 _position, GraphEditorWindow _editorWindow, GraphTreeView _graphTreeView)
        {
            editorWindow = _editorWindow;
            graphTreeView = _graphTreeView;

            title = "End";
            SetPosition(new Rect(_position, defaultNodeSize));
            nodeGUID = Guid.NewGuid().ToString();

            AddInputPort("Input", Port.Capacity.Multi);

            enumField = new EnumField()
            {
                value = endNodeType
            };

            enumField.Init(EndNodeType);
            enumField.RegisterValueChangedCallback((value) =>
            {
                endNodeType = (EndNodeType)value.newValue;
                Debug.Log("<color=blue>EndNodeType updated:</color> " + endNodeType);
            });
            enumField.SetValueWithoutNotify(endNodeType);

            mainContainer.Add(enumField);

            Debug.Log("<color=green>EndNode created at position: </color>" + _position);
        }

        public override void LoadValueIntoField()
        {
            enumField.SetValueWithoutNotify(endNodeType);
            Debug.Log("<color=blue>EndNode values loaded into field.</color>");
        }
    }
}