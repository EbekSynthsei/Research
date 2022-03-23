using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaniakeaCode.GraphSystem {
    /// <summary>
    /// It Parses The GUID To Get The Graph Tree Elements
    /// </summary>
    public class GraphDataParser : MonoBehaviour
    {
        [SerializeField] protected GraphTree graphTree;
        protected NodeData GetNodeByGuid(string _targetNodeGuid)
        {
            return graphTree
                .AllNodeDatas
                .Find(node => node
                .nodeGUID == _targetNodeGuid);
        }

        protected NodeData GetNodeByNodePort(DialogueNodePort _nodePort)
        {
            return graphTree
                .AllNodeDatas
                .Find(node => node.nodeGUID == _nodePort.InputGuid);
        }

        protected NodeData GetNextNode(NodeData _baseNodeData)
        {
            NodeLinkData nodeLinkData = graphTree
                .nodeLinks
                .Find(edge => edge
                .baseNodeGUID == _baseNodeData.nodeGUID);

            return GetNodeByGuid(nodeLinkData.targetNodeGUID);
        }
    }
}