using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace LaniakeaCode.GraphSystem
{
    public class EndNode : BaseNode
    {
        private EndNodeType endNodeType = EndNodeType.End;
        private EnumField enumField;

        public EndNodeType EndNodeType { get => endNodeType; set => endNodeType = value; }

        public EndNode()
        {

        }

        public EndNode(Vector2 _position, GraphEditorWindow _editorWindow, GraphTreeView _graphTreeView)
        {
            editorWindow = _editorWindow;
            graphTreeView = _graphTreeView;

            title = "End";
            SetPosition(new Rect(_position, defaultNodeSize));
            nodeGUID = Guid
                .NewGuid()
                .ToString();

            //Setting The Input Port To Be Able To Receive Multiple Inputs
            AddInputPort("Input", Port.Capacity.Multi);

            enumField = new EnumField()
            {
                value = endNodeType
            };

            //The Field Need To Be Initialized
            enumField
                .Init(EndNodeType);

            //Setting The Field To be Able To Record Type Changes
            enumField
                .RegisterValueChangedCallback((value) =>
            {
                endNodeType = (EndNodeType)value.newValue;
            });
            enumField
                .SetValueWithoutNotify(endNodeType);

            //Adding The Field In The Node
            mainContainer
                .Add(enumField);
        }

        public override void LoadValueIntoField()
        {
            enumField.SetValueWithoutNotify(endNodeType);
        }
    }
}