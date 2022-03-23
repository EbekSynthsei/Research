using System.Linq;
using System.Collections;

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

        //Making a link between Property and Graph View Reference
        private List<Edge> edges => graphView.edges.ToList();
        private List<BaseNode> nodes => graphView.nodes.ToList()
            .Where(node => node is BaseNode)
            .Cast<BaseNode>().ToList();

        public void Save(GraphTree _graphTree)
        {
            SaveEdges(_graphTree);
            SaveNodes(_graphTree);

            EditorUtility.SetDirty(_graphTree);
            AssetDatabase.SaveAssets();
        }
        public void Load(GraphTree _graphTree)
        {
            ClearGraph();
            GenerateNodes(_graphTree);
            ConnectNodes(_graphTree);

        }

        #region Save
        // Saving The Edges via clearing the list of nodes
        // Taking all the edges of the graph in which we have an input
        // For each of them create two references nodes
        private void SaveEdges(GraphTree _graphTree)
        {
            _graphTree
                .nodeLinks
                .Clear();

            Edge[] connectedEdges = edges
                .Where(edge => edge
                    .input
                    .node != null)
                    .ToArray();

            for (int i = 0; i < connectedEdges.Count(); i++)
            {
                //Two Methods possible To take the Connected Edges
                BaseNode outputNode = (BaseNode)connectedEdges[i]
                                                .output
                                                .node as BaseNode;

                BaseNode inputNode = connectedEdges[i]
                                                .input
                                                .node as BaseNode;

                _graphTree.nodeLinks.Add(new NodeLinkData
                {
                    baseNodeGUID = outputNode.NodeGUID,
                    targetNodeGUID = inputNode.NodeGUID
                });
            }
        }

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

                    case DialogueNode uiNode:
                        _graphTree
                        .dialogueNodeDatas
                        .Add(SaveNodeData(uiNode));
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

        private DialogueNodeData SaveNodeData(DialogueNode _graphNode)
        {
            //Setting all the DataContainer Elements to the one passed trough the view
            DialogueNodeData graphNodeData = new DialogueNodeData
            {
                nodeGUID = _graphNode
                .NodeGUID,

                position = _graphNode
                .GetPosition()
                .position,

                textBox_languages = _graphNode
                .DialogueBoxTexts,

                Name = _graphNode
                .NodeTitle,

                sprite = _graphNode
                .SpeakerImage,

                switchType = _graphNode
                .SimpleSwitch,

                audioClips_List = _graphNode
                .AudioClips,

                dialogueNodePorts = new List<DialogueNodePort>(_graphNode.DialogueNodePorts)
            };

            foreach (DialogueNodePort nodePort in graphNodeData.dialogueNodePorts)
            {
                nodePort.OutputGuid = string.Empty;
                nodePort.InputGuid = string.Empty;
                foreach (Edge edge in edges)
                {
                    if (edge.output == nodePort.MyPort)
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

        private StartNodeData SaveNodeData(StartNode _node)
        {
            StartNodeData nodeData = new StartNodeData()
            {
                nodeGUID = _node.NodeGUID,
                position = _node.GetPosition().position
            };
            return nodeData;
        }
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
        private void ClearGraph()
        {
            edges.ForEach(edge => graphView.RemoveElement(edge));

            foreach (BaseNode node in nodes)
            {
                graphView.RemoveElement(node);
            }
        }

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
                DialogueNode tempNode = graphView.CreateUINode(nodeData.position);

                tempNode
                    .NodeGUID = nodeData.nodeGUID;
                tempNode
                    .NodeTitle = nodeData.Name;
                tempNode
                    .DialogueBoxTexts = nodeData.textBox_languages;
                tempNode
                    .SpeakerImage = nodeData.sprite;
                tempNode
                    .AudioClips = nodeData.audioClips_List;
                tempNode
                    .SimpleSwitch = nodeData.switchType;

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

                foreach (DialogueNodePort nodePort in nodeData.dialogueNodePorts)
                {
                    tempNode
                        .AddChoicePort(tempNode, nodePort);
                }

                tempNode
                    .LoadValueIntoField();

                graphView
                    .AddElement(tempNode);
            }
        }
        public void ConnectNodes(GraphTree _graphTree)
        {
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
                        LinkAppropriateNodes(nodes[i].outputContainer[j].Q<Port>(), (Port)targetNode.inputContainer[0]);
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
                    if (nodePort.InputGuid != string.Empty)
                    {
                        BaseNode targetNode = nodes
                            .First(Node => Node
                            .NodeGUID == nodePort.InputGuid);

                        LinkAppropriateNodes(nodePort.MyPort, (Port)targetNode.inputContainer[0]);
                    }
                }
            }
        }

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