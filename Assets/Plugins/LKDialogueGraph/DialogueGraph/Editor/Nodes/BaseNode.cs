using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace LaniakeaCode.GraphSystem
{
    public class BaseNode : Node
    {
        protected string nodeGUID;
        protected GraphTreeView graphTreeView;
        protected GraphEditorWindow editorWindow;
        protected Vector2 defaultNodeSize = new Vector2(200, 250);

        public string NodeGUID { get => nodeGUID; set => nodeGUID = value; }
        public BaseNode()
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Node"));
            AddToClassList("Node");
        }

        //Adding the Base OutputPort with a Name
        public void AddOutputPort(string name, Port.Capacity capacity = Port.Capacity.Single)
        {
            Port outputPort = GetPortInstance(Direction.Output, capacity);
            outputPort.portName = name;
            outputContainer.Add(outputPort);
        }

        //Adding the Base InputPort with a Name
        public void AddInputPort(string name, Port.Capacity capacity = Port.Capacity.Single)
        {
            var inputPort = GetPortInstance(Direction.Input, capacity);
            inputPort.portName = name;
            inputContainer.Add(inputPort);
        }

        //Calling the Base Port Instantiation
        public Port GetPortInstance(Direction nodeDirection, Port.Capacity capacity = Port.Capacity.Single)
        {
            return InstantiatePort(Orientation.Horizontal, nodeDirection, capacity, typeof(float));
        }

        public virtual void LoadValueIntoField()
        {

        }

        public virtual void ReloadLanguage()
        {

        }
    }
}