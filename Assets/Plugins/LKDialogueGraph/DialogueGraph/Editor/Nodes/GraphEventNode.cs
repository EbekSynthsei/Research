using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace LaniakeaCode.GraphSystem
{
    public class GraphEventNode : BaseNode
    {
        private GraphEvent graphEvent;
        private ObjectField objectField;
        public GraphEvent GraphEvent { get => graphEvent; set => graphEvent = value; }


        public GraphEventNode()
        {

        }

        /// <summary>
        /// Building The Event Node. Refer To This To implement a Similar Object Field Node
        /// </summary>
        /// <param name="_position"></param>
        /// <param name="_editorWindow"></param>
        /// <param name="_graphTreeView"></param>
        public GraphEventNode(Vector2 _position, GraphEditorWindow _editorWindow, GraphTreeView _graphTreeView)
        {
            editorWindow = _editorWindow;
            graphTreeView = _graphTreeView;

            //Initializing
            title = "Graph Event";
            SetPosition(new Rect(_position, defaultNodeSize));
            nodeGUID = Guid.NewGuid().ToString();

            //Adding Multi Input Single Output
            AddInputPort("Input", Port.Capacity.Multi);
            AddOutputPort("Output", Port.Capacity.Single);

            //Setting The Object Field To Accept The Graph Event
            objectField = new ObjectField()
            {
                objectType = typeof(GraphEvent),
                allowSceneObjects = false,
                value = graphEvent
            };
            //Recording Changes On The Field
            objectField.RegisterValueChangedCallback(value =>
            {
                graphEvent = objectField.value as GraphEvent;
            });
            objectField.SetValueWithoutNotify(graphEvent);

            //Adding The ObjectField To Container
            mainContainer.Add(objectField);
        }

        public override void LoadValueIntoField()
        {
            objectField.SetValueWithoutNotify(graphEvent);
        }
    }
}