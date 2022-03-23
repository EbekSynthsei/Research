using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LaniakeaCode.Events;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace LaniakeaCode.GraphSystem
{
    /// <summary>
    /// GraphItem Stores The Node Connections and Datas
    /// </summary>
    /// Put In Here The References To The Type Of GraphNode
    /// 

    public abstract class GraphItem : ScriptableObject
    {
        public List<NodeLinkData> nodeLinks = new List<NodeLinkData>();

        public List<StartNodeData> startNodeDatas = new List<StartNodeData>();
        public List<EndNodeData> endNodeDatas = new List<EndNodeData>();
        public List<DialogueNodeData> dialogueNodeDatas = new List<DialogueNodeData>();
        public List<GraphEventNodeData> graphEventNodeDatas = new List<GraphEventNodeData>();

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
        public string baseNodeGUID;
        public string targetNodeGUID;
        public string outputPortName;
        public string inputPortName;
    }

    /// <summary>
    /// Base Node Data To Reference In The GraphTree
    /// </summary>
    [System.Serializable]
    public class NodeData
    {
        public string nodeGUID;
        public Vector2 position;
    }

    /// <summary>
    /// The Context Generic Reference
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// 
    [System.Serializable]
    public class LanguageGeneric<T>
    {
        public LanguageType LanguageType;
        public T LanguageGenericType;
    }


    /// <summary>
    /// EndNodeData Is An Enum Container
    /// </summary>
    [System.Serializable]
    public class EndNodeData : NodeData
    {
        public EndNodeType endNodeType;
    }

    /// <summary>
    /// StartNodeData Holds Just A Position And His GUID
    /// </summary>
    /// 
    [System.Serializable]
    public class StartNodeData : NodeData
    {

    }

    /// <summary>
    /// Graph Event Node Data is A Basic Reference To Work On
    /// </summary>
    [System.Serializable]
    public class GraphEventNodeData : NodeData
    {
        public GraphEvent graphEvent;
    }
    
    [System.Serializable]
    public class DialogueNodePort
    {
        public string PortGUID;
        public string InputGuid;
        public string OutputGuid;
        public Port MyPort;
        public TextField choicePortName_Field;
        public List<LanguageGeneric<string>> ChoiceText_List = new List<LanguageGeneric<string>>();
    }
    
    [System.Serializable]
    public class DialogueNodeData : NodeData
    {
        public List<DialogueNodePort> dialogueNodePorts;
        public Sprite sprite;
        public SimplSwitchType switchType;
        public List<LanguageGeneric<string>> textBox_languages;
        public List<LanguageGeneric<AudioClip>> audioClips_List;
        public string Name;
    }
}
