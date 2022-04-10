using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using UnityEngine.UIElements;

namespace LaniakeaCode.GraphSystem
{
    /// <summary>
    /// Graph Save Utility Reads And Updates The Graph Tree In Relation To The Graph Tree View
    /// </summary>
    public class GraphSaveUtility
    {
        //Referencing a Graph Tree View
        private GraphTreeView graphView;
        public GraphSaveUtility(GraphTreeView _graphView)
        {
            graphView = _graphView;
        }

        //Making a link between to Graph View Reference
        private List<Edge> edges => graphView.edges.ToList();
        private List<BaseNode> nodes => graphView
            .nodes
            .ToList()
            .Where(node => node is BaseNode)
            .Cast<BaseNode>()
            .ToList();

        /// <summary>
        /// Saves Edges, Nodes, then sets dirty the asset and Saves it
        /// </summary>
        /// <param name="_graphTree"></param>
        public void Save(GraphTree _graphTree)
        {
            SaveEdges(_graphTree);
            SaveNodes(_graphTree);

            EditorUtility.SetDirty(_graphTree);
            AssetDatabase.SaveAssets();
        }

        /// <summary>
        /// Clears the current tree, then Generates the nodes and connects them
        /// </summary>
        /// <param name="_graphTree"></param>
        public void Load(GraphTree _graphTree)
        {
            ClearGraph();
            GenerateNodes(_graphTree);
            ConnectNodes(_graphTree);

        }

        #region Save

        /// <summary>
        /// Saving The Edges via clearing previous list of edges
        /// Taking all the edges of the graph in which we have a connection
        /// For each of them create two references nodes
        /// </summary>
        /// <param name="_graphTree"></param>
        private void SaveEdges(GraphTree _graphTree)
        {
            //Clearing the list of previous links on the asset
            _graphTree
                .nodeLinks
                .Clear();

            //Enlisting all the edges actually connected to one other
            Edge[] connectedEdges = edges
                .Where(edge => edge
                    .input
                    .node != null)
                    .ToArray();

            //Cycling the Edges to get the connected ones, both from in and out
            for (int i = 0; i < connectedEdges.Count(); i++)
            {
                //Two Methods possible To take the Connected Edges
                BaseNode outputNode = (BaseNode)connectedEdges[i]
                                                .output
                                                .node as BaseNode;

                BaseNode inputNode = connectedEdges[i]
                                                .input
                                                .node as BaseNode;

                //Readding the links to the edges list in the appropriate container
                _graphTree.nodeLinks.Add(new NodeLinkData
                {
                    baseNodeGUID = outputNode.NodeGUID,
                    targetNodeGUID = inputNode.NodeGUID
                });
            }
        }

        /// <summary>
        /// Clears the previous nodes in the assets
        /// Saves the temporary ones switching on their type
        /// </summary>
        /// <param name="_graphTree"></param>
        private void SaveNodes(GraphTree _graphTree)
        {
            //Clearing All the Lists of Nodes in the Tree
            _graphTree
                .dialogueNodeDatas
                .Clear();

            _graphTree
                .graphEventNodeDatas
                .Clear();

            _graphTree
                .startNodeDatas
                .Clear();

            _graphTree
                .endNodeDatas
                .Clear();

            //Adding A specific type node foreach of the temporary nodes on screen
            nodes.ForEach(node =>
            {
                switch (node)
                {

                    case DialogueNode dialogueNode:
                        _graphTree
                        .dialogueNodeDatas
                        .Add(SaveNodeData(dialogueNode));
                        break;
                    case StartNode startNode:
                        _graphTree
                        .startNodeDatas
                        .Add(SaveNodeData(startNode));
                        break;
                    case EndNode endNode:
                        _graphTree
                        .endNodeDatas
                        .Add(SaveNodeData(endNode));
                        break;
                    case GraphEventNode eventNode:
                        _graphTree
                        .graphEventNodeDatas
                        .Add(SaveNodeData(eventNode));
                        break;
                    default:
                        break;
                }
            });
        }

        /// <summary>
        /// Sets the datas in the node to be saved
        /// </summary>
        /// <param name="_node"></param>
        /// <returns>A Dialogue Node Data</returns>
        private DialogueNodeData SaveNodeData(DialogueNode _node)
        {
            //Setting all the DataContainer Elements to the one passed trough the view
            DialogueNodeData graphNodeData = new DialogueNodeData
            {
                nodeGUID = _node
                .NodeGUID,

                position = _node
                .GetPosition()
                .position,

                textBox_languages = _node
                .DialogueBoxTexts,

                CharacterName = _node
                .CharacterName,

                SpeakerImage = _node
                .SpeakerImage,

                switchType = _node
                .SimpleSwitch,

                audioClips_List = _node
                .AudioClips,

                dialogueNodePorts = new List<DialogueNodePort>(_node.DialogueNodePorts)
            };
            
            //Cycling through Ports to save the edges 
            foreach (DialogueNodePort nodePort in graphNodeData.dialogueNodePorts)
            {
                nodePort.OutputGuid = string.Empty;
                nodePort.InputGuid = string.Empty;
                //Cycling through Edges to save the reference to the input and output one
                foreach (Edge edge in edges)
                {
                    if (edge.output.portName == nodePort.PortGUID)
                    {
                        nodePort
                            .OutputGuid = (edge.output.node as BaseNode).NodeGUID;
                        nodePort
                            .InputGuid = (edge.input.node as BaseNode).NodeGUID;
                    }
                }
            }
            return graphNodeData;
        }

        /// <summary>
        /// Sets the datas in the node to be saved
        /// </summary>
        /// <param name="_node"></param>
        /// <returns>A Start Node Data</returns>
        private StartNodeData SaveNodeData(StartNode _node)
        {
            StartNodeData nodeData = new StartNodeData()
            {
                nodeGUID = _node.NodeGUID,
                position = _node.GetPosition().position
            };
            return nodeData;
        }

        /// <summary>
        /// Sets the datas in the node to be saved
        /// </summary>
        /// <param name="_node"></param>
        /// <returns>An End Node Data</returns>
        private EndNodeData SaveNodeData(EndNode _node)
        {
            EndNodeData nodeData = new EndNodeData()
            {
                nodeGUID = _node.NodeGUID,
                position = _node.GetPosition().position,
                endNodeType = _node.EndNodeType
            };
            return nodeData;
        }
        /// <summary>
        /// Sets the datas in the node to be saved
        /// </summary>
        /// <param name="_node"></param>
        /// <returns>An Event Node Data</returns>
        private GraphEventNodeData SaveNodeData(GraphEventNode _node)
        {
            GraphEventNodeData nodeData = new GraphEventNodeData()
            {
                nodeGUID = _node
                .NodeGUID,

                position = _node
                .GetPosition()
                .position,

                graphEvent = _node
                .GraphEvent
            };
            return nodeData;
        }

        #endregion

        #region Load

        /// <summary>
        /// Clears all the nodes on screen
        /// </summary>
        private void ClearGraph()
        {
            edges.ForEach(edge => graphView.RemoveElement(edge));

            foreach (BaseNode node in nodes)
            {
                graphView.RemoveElement(node);
            }
        }

        /// <summary>
        /// Generate the nodes according to their type, cycling on the asset's enlisted nodes 
        /// </summary>
        /// <param name="_graphTree"></param>
        public void GenerateNodes(GraphTree _graphTree)
        {
            //StartNode
            foreach (StartNodeData nodeData in _graphTree.startNodeDatas)
            {
                StartNode tempNode = graphView
                    .CreateStartNode(nodeData.position);
                tempNode
                    .NodeGUID = nodeData.nodeGUID;

                graphView
                    .AddElement(tempNode);
            }

            //EndNode
            foreach (EndNodeData nodeData in _graphTree.endNodeDatas)
            {

                EndNode tempNode = graphView
                    .CreateEndNode(nodeData.position);
                tempNode
                .NodeGUID = nodeData.nodeGUID;
                tempNode
                .EndNodeType = nodeData.endNodeType;

                tempNode
                    .LoadValueIntoField();
                graphView
                    .AddElement(tempNode);

            }

            //EventNode
            foreach (GraphEventNodeData nodeData in _graphTree.graphEventNodeDatas)
            {
                GraphEventNode tempNode = graphView
                    .CreateGraphEventNode(nodeData.position);

                tempNode
                    .NodeGUID = nodeData.nodeGUID;
                tempNode
                    .GraphEvent = nodeData.graphEvent;

                tempNode
                    .LoadValueIntoField();
                graphView
                    .AddElement(tempNode);

            }

            //Graph node
            foreach (DialogueNodeData nodeData in _graphTree.dialogueNodeDatas)
            {
                //Instantiating a temporary Node
                DialogueNode tempNode = graphView.CreateDialogueNode(nodeData.position);

                //Getting the Values of the Datas in the node
                tempNode
                    .NodeGUID = nodeData.nodeGUID;
                tempNode
                    .CharacterName = nodeData.CharacterName;
                tempNode
                    .DialogueBoxTexts = nodeData.textBox_languages;
                tempNode
                    .SpeakerImage = nodeData.SpeakerImage;
                tempNode
                    .AudioClips = nodeData.audioClips_List;
                tempNode
                    .SimpleSwitch = nodeData.switchType;

                //Cycling on the Text Boxes and setting the appropriate one to the right language box
                foreach(LanguageGeneric<string> languageGeneric in nodeData.textBox_languages)
                {
                    tempNode
                        .DialogueBoxTexts
                        .Find(language => language
                        .LanguageType == languageGeneric
                        .LanguageType
                        ).LanguageGenericType = languageGeneric
                        .LanguageGenericType;
                }

                //Cycling on the AudioClips and setting the appropriate one to the right language box
                foreach (LanguageGeneric<AudioClip> languageGeneric in nodeData.audioClips_List)
                {
                    tempNode
                        .AudioClips
                        .Find(language => language
                        .LanguageType == languageGeneric
                        .LanguageType
                        ).LanguageGenericType = languageGeneric
                        .LanguageGenericType;
                }

                //Cycling on Choices
                foreach (DialogueNodePort nodePort in nodeData.dialogueNodePorts)
                {
                    tempNode
                        .AddChoicePort(tempNode, nodePort);
                }

                //Loading the values on the field
                tempNode
                    .LoadValueIntoField();

                //Readding the node on the view
                graphView
                    .AddElement(tempNode);
            }
        }

        /// <summary>
        /// Reconnects the nodes, cycling on their Node Datas and finding the relative ports to be connected to
        /// </summary>
        /// <param name="_graphTree"></param>
        public void ConnectNodes(GraphTree _graphTree)
        {
            //Cycling on connections and linking them
            for (int i = 0; i < nodes.Count; i++)
            {
                List<NodeLinkData> connections = _graphTree
                    .nodeLinks
                    .Where(edge => edge
                    .baseNodeGUID == nodes[i].NodeGUID)
                    .ToList();

                for (int j = 0; j < connections.Count; j++)
                {
                    string targetNodeGuid = connections[j].targetNodeGUID;

                    BaseNode targetNode = nodes
                        .First(node => node
                        .NodeGUID == targetNodeGuid);

                    if ((nodes[i] is DialogueNode) == false)
                    {
                        LinkAppropriateNodes(nodes[i]
                            .outputContainer[j]
                            .Q<Port>(), (Port)targetNode.inputContainer[0]);
                    }
                }
            }

            List<DialogueNode> graphNodes = nodes
                .FindAll(node => node is DialogueNode)
                .Cast<DialogueNode>()
                .ToList();

            foreach (DialogueNode graphNode in graphNodes)
            {
                foreach (DialogueNodePort nodePort in graphNode.DialogueNodePorts)
                {
                    //Check if port has connection
                    if (nodePort.InputGuid != string.Empty)
                    {
                        BaseNode targetNode = nodes
                            .First(Node => Node
                            .NodeGUID == nodePort.InputGuid);

                        //Using a Null Port to set it to a found valid one
                        Port tempPort = null;
                        for(int i= 0; i<graphNode.outputContainer.childCount; i++)
                        {
                            if(graphNode.outputContainer[i].Q<Port>().portName == nodePort.PortGUID)
                            {
                                tempPort = graphNode.outputContainer[i].Q<Port>();
                            }
                        }

                        LinkAppropriateNodes(tempPort, (Port)targetNode.inputContainer[0]);
                    }
                }
            }
        }

        /// <summary>
        /// Method to link outputs to inputs
        /// </summary>
        /// <param name="_outputPort"></param>
        /// <param name="_inputPort"></param>
        private void LinkAppropriateNodes(Port _outputPort, Port _inputPort)
        {
            Edge tempEdge = new Edge()
            {
                output = _outputPort,
                input = _inputPort
            };
            tempEdge.input.Connect(tempEdge);
            tempEdge.output.Connect(tempEdge);
            graphView.Add(tempEdge);
        }

        #endregion
    }
}