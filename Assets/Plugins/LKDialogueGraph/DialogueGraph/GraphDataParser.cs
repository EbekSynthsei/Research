using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaniakeaCode.GraphSystem
{
    /// <summary>
    /// It Parses The GUID To Get The Graph Tree Elements
    /// </summary>
    public class GraphDataParser : MonoBehaviour
    {
        /// <summary>
        /// The graph tree containing all node data and links.
        /// </summary>
        [SerializeField] protected GraphTree graphTree;

        /// <summary>
        /// Gets the node data by its GUID.
        /// </summary>
        /// <param name="_targetNodeGuid">The GUID of the target node.</param>
        /// <returns>The node data with the specified GUID.</returns>
        protected NodeData GetNodeByGuid(string _targetNodeGuid)
        {
            return graphTree
                .AllNodeDatas
                .Find(node => node
                .nodeGUID == _targetNodeGuid);
        }

        /// <summary>
        /// Gets the node data by the node port.
        /// </summary>
        /// <param name="_nodePort">The node port containing the input GUID.</param>
        /// <returns>The node data connected to the specified node port.</returns>
        protected NodeData GetNodeByNodePort(DialogueNodePort _nodePort)
        {
            return graphTree
                .AllNodeDatas
                .Find(node => node.nodeGUID == _nodePort.InputGuid);
        }

        /// <summary>
        /// Gets the next node data connected to the base node data.
        /// </summary>
        /// <param name="_baseNodeData">The base node data.</param>
        /// <returns>The next node data connected to the base node data.</returns>
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