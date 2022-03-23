using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

namespace LaniakeaCode.GraphSystem
{
    public class StartNode : BaseNode
    {
        public StartNode()
        {

        }
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
        }
    }
}