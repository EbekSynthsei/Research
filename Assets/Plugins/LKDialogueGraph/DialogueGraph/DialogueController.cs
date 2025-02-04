using LaniakeaCode.GraphSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LaniakeaCode.Utilities
{
    /// <summary>
    /// Controls the dialogue system.
    /// </summary>
    public class DialogueController : GraphDataParser
    {
        [SerializeField] private UIController uIController;
        [SerializeField] private AudioSource audioSource;

        private DialogueNodeData currentDialogueNodeData;
        private DialogueNodeData lastDialogueNodeData;

        /// <summary>
        /// Initializes the dialogue controller.
        /// </summary>
        private void Awake()
        {
            uIController = FindObjectOfType<UIController>();
            audioSource = GetComponent<AudioSource>();
        }

        /// <summary>
        /// Starts the UI panel for the dialogue.
        /// </summary>
        public void StartUIPanel()
        {
            CheckNodeType(GetNextNode(graphTree.startNodeDatas[0]));
            uIController.ShowUI(true);
        }

        /// <summary>
        /// Checks the type of the node and runs the appropriate method.
        /// </summary>
        /// <param name="_baseNodeData">The base node data to check.</param>
        private void CheckNodeType(NodeData _baseNodeData)
        {
            switch (_baseNodeData)
            {
                case StartNodeData nodeData:
                    RunNode(nodeData);
                    break;
                case DialogueNodeData nodeData:
                    RunNode(nodeData);
                    break;
                case EndNodeData nodeData:
                    RunNode(nodeData);
                    break;
                case GraphEventNodeData nodeData:
                    RunNode(nodeData);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Runs the start node.
        /// </summary>
        /// <param name="_nodeData">The start node data.</param>
        private void RunNode(StartNodeData _nodeData)
        {
            CheckNodeType(GetNextNode(graphTree.startNodeDatas[0]));
        }

        /// <summary>
        /// Runs the dialogue node.
        /// </summary>
        /// <param name="_nodeData">The dialogue node data.</param>
        private void RunNode(DialogueNodeData _nodeData)
        {
            if (currentDialogueNodeData != _nodeData)
            {
                lastDialogueNodeData = currentDialogueNodeData;
                currentDialogueNodeData = _nodeData;
            }
            uIController.SetText(_nodeData.CharacterName, _nodeData.textBox_languages.Find(text => text.LanguageType == LanguageController.Instance.Language).LanguageGenericType);
            uIController.SetImage(_nodeData.SpeakerImage, _nodeData.switchType);
            audioSource.clip = _nodeData.audioClips_List.Find(audioclip => audioclip.LanguageType == LanguageController.Instance.Language).LanguageGenericType;
            audioSource.Play();
            MakeButtons(_nodeData.dialogueNodePorts);
        }

        /// <summary>
        /// Runs the graph event node.
        /// </summary>
        /// <param name="_nodeData">The graph event node data.</param>
        private void RunNode(GraphEventNodeData _nodeData)
        {
            if (_nodeData.graphEvent != null)
            {
                _nodeData.graphEvent.Raise();
            }
            CheckNodeType(GetNextNode(_nodeData));
        }

        /// <summary>
        /// Runs the end node.
        /// </summary>
        /// <param name="_nodeData">The end node data.</param>
        private void RunNode(EndNodeData _nodeData)
        {
            switch (_nodeData.endNodeType)
            {
                case EndNodeType.End:
                    uIController.ShowUI(false);
                    break;
                case EndNodeType.Repeat:
                    CheckNodeType(GetNodeByGuid(currentDialogueNodeData.nodeGUID));
                    break;
                case EndNodeType.GoBack:
                    CheckNodeType(GetNodeByGuid(lastDialogueNodeData.nodeGUID));
                    break;
                case EndNodeType.ReturnToStart:
                    CheckNodeType(GetNextNode(graphTree.startNodeDatas[0]));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Creates buttons for the dialogue choices.
        /// </summary>
        /// <param name="_dialogueNodePorts">The dialogue node ports.</param>
        private void MakeButtons(List<DialogueNodePort> _dialogueNodePorts)
        {
            List<string> texts = new List<string>();
            List<UnityAction> unityActions = new List<UnityAction>();

            foreach (DialogueNodePort nodePort in _dialogueNodePorts)
            {
                texts.Add(nodePort.ChoiceText_List.Find(text => text.LanguageType == LanguageController.Instance.Language).LanguageGenericType);

                UnityAction tempAction = null;
                tempAction += () =>
                {
                    CheckNodeType(GetNodeByGuid(nodePort.InputGuid));
                    audioSource.Stop();
                };
                unityActions.Add(tempAction);
            }
            uIController.SetButtons(texts, unityActions);
        }
    }
}