using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace LaniakeaCode.GraphSystem
{
    /// <summary>
    /// GraphItem Stores The Node Connections and Datas
    /// </summary>
    /// Put In Here The References To The Type Of GraphNode
    public abstract class GraphItem : ScriptableObject
    {
        /// <summary>
        /// List of node links in the graph.
        /// </summary>
        public List<NodeLinkData> nodeLinks = new List<NodeLinkData>();

        /// <summary>
        /// List of start node data.
        /// </summary>
        public List<StartNodeData> startNodeDatas = new List<StartNodeData>();

        /// <summary>
        /// List of end node data.
        /// </summary>
        public List<EndNodeData> endNodeDatas = new List<EndNodeData>();

        /// <summary>
        /// List of dialogue node data.
        /// </summary>
        public List<DialogueNodeData> dialogueNodeDatas = new List<DialogueNodeData>();

        /// <summary>
        /// List of graph event node data.
        /// </summary>
        public List<GraphEventNodeData> graphEventNodeDatas = new List<GraphEventNodeData>();

        /// <summary>
        /// Gets all node data combined into a single list.
        /// </summary>
        public List<NodeData> AllNodeDatas
        {
            get
            {
                List<NodeData> tmp = new List<NodeData>();
                tmp.AddRange(startNodeDatas);
                tmp.AddRange(endNodeDatas);
                tmp.AddRange(dialogueNodeDatas);
                tmp.AddRange(graphEventNodeDatas);
                return tmp;
            }
        }
    }

    /// <summary>
    /// Node Link Data To Navigate The GraphTree
    /// </summary>
    [System.Serializable]
    public class NodeLinkData
    {
        /// <summary>
        /// GUID of the base node.
        /// </summary>
        public string baseNodeGUID;

        /// <summary>
        /// GUID of the target node.
        /// </summary>
        public string targetNodeGUID;

        /// <summary>
        /// Name of the output port.
        /// </summary>
        public string outputPortName;

        /// <summary>
        /// Name of the input port.
        /// </summary>
        public string inputPortName;
    }

    /// <summary>
    /// Base Node Data To Reference In The GraphTree
    /// </summary>
    [System.Serializable]
    public class NodeData
    {
        /// <summary>
        /// GUID of the node.
        /// </summary>
        public string nodeGUID;

        /// <summary>
        /// Position of the node.
        /// </summary>
        public Vector2 position;
    }

    /// <summary>
    /// The Context Generic Reference
    /// </summary>
    /// <typeparam name="T">Type of the language generic.</typeparam>
    [System.Serializable]
    public class LanguageGeneric<T>
    {
        /// <summary>
        /// Type of the language.
        /// </summary>
        public LanguageType LanguageType;

        /// <summary>
        /// Generic type of the language.
        /// </summary>
        public T LanguageGenericType;
    }

    /// <summary>
    /// EndNodeData Is An Enum Container
    /// </summary>
    [System.Serializable]
    public class EndNodeData : NodeData
    {
        /// <summary>
        /// Type of the end node.
        /// </summary>
        public EndNodeType endNodeType;
    }

    /// <summary>
    /// StartNodeData Holds Just A Position And His GUID
    /// </summary>
    [System.Serializable]
    public class StartNodeData : NodeData
    {
        // No additional fields or properties
    }

    /// <summary>
    /// Graph Event Node Data is A Basic Reference To Work On
    /// </summary>
    [System.Serializable]
    public class GraphEventNodeData : NodeData
    {
        /// <summary>
        /// Graph event associated with the node.
        /// </summary>
        public GraphEvent graphEvent;
    }

    /// <summary>
    /// Dialogue Node Port Data
    /// </summary>
    [System.Serializable]
    public class DialogueNodePort
    {
        /// <summary>
        /// GUID of the port.
        /// </summary>
        public string PortGUID;

        /// <summary>
        /// GUID of the input node.
        /// </summary>
        public string InputGuid;

        /// <summary>
        /// GUID of the output node.
        /// </summary>
        public string OutputGuid;

        /// <summary>
        /// Text field for the choice port name.
        /// </summary>
        public TextField choicePortName_Field;

        /// <summary>
        /// List of choice texts in different languages.
        /// </summary>
        public List<LanguageGeneric<string>> ChoiceText_List = new List<LanguageGeneric<string>>();
    }

    /// <summary>
    /// Dialogue Node Data
    /// </summary>
    [System.Serializable]
    public class DialogueNodeData : NodeData
    {
        /// <summary>
        /// List of dialogue node ports.
        /// </summary>
        public List<DialogueNodePort> dialogueNodePorts;

        /// <summary>
        /// Image of the speaker.
        /// </summary>
        public Sprite SpeakerImage;

        /// <summary>
        /// Type of the switch.
        /// </summary>
        public SimplSwitchType switchType;

        /// <summary>
        /// List of text boxes in different languages.
        /// </summary>
        public List<LanguageGeneric<string>> textBox_languages;

        /// <summary>
        /// List of audio clips in different languages.
        /// </summary>
        public List<LanguageGeneric<AudioClip>> audioClips_List;

        /// <summary>
        /// Name of the character.
        /// </summary>
        public string CharacterName;
    }
}
