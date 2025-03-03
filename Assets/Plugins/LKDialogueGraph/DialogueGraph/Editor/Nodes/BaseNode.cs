using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace LaniakeaCode.GraphSystem
{
    /// <summary>
    /// Base class for all nodes in the dialogue graph.
    /// </summary>
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
            Debug.Log("<color=green>BaseNode created.</color>");
        }

        /// <summary>
        /// Adds an output port to the node.
        /// </summary>
        /// <param name="name">The name of the port.</param>
        /// <param name="capacity">The capacity of the port.</param>
        public void AddOutputPort(string name, Port.Capacity capacity = Port.Capacity.Single)
        {
            Port outputPort = GetPortInstance(Direction.Output, capacity);
            outputPort.portName = name;
            outputContainer.Add(outputPort);
            Debug.Log("<color=green>Output port added:</color> " + name);
        }

        /// <summary>
        /// Adds an input port to the node.
        /// </summary>
        /// <param name="name">The name of the port.</param>
        /// <param name="capacity">The capacity of the port.</param>
        public void AddInputPort(string name, Port.Capacity capacity = Port.Capacity.Single)
        {
            var inputPort = GetPortInstance(Direction.Input, capacity);
            inputPort.portName = name;
            inputContainer.Add(inputPort);
            Debug.Log("<color=green>Input port added:</color> " + name);
        }

        /// <summary>
        /// Instantiates a port for the node.
        /// </summary>
        /// <param name="nodeDirection">The direction of the port.</param>
        /// <param name="capacity">The capacity of the port.</param>
        /// <returns>The instantiated port.</returns>
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