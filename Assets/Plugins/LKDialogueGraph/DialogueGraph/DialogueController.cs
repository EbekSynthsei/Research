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

        private bool isDialogueActive;

        /// <summary>
        /// Initializes the dialogue controller.
        /// </summary>
        private void Awake()
        {
            uIController = FindAnyObjectByType<UIController>();
            audioSource = GetComponent<AudioSource>();
        }

        /// <summary>
        /// Starts the dialogue UI panel with a specific graph, or the default one if none is provided.
        /// </summary>
        /// <param name="graph">The GraphTree to use for this dialogue instance. If null, uses the currently assigned graphTree.</param>
        public void StartUIPanel(GraphTree graph = null)
        {
            Debug.Log("DialogueController: StartUIPanel called", this);
            if (isDialogueActive)
            {
                Debug.LogWarning("Dialogue already active — ignoring new StartUIPanel call.", this);
                return;
            }

            if (graph != null)
                graphTree = graph;

            Debug.Log("DialogueController: graphTree assigned. Is null? " + (graphTree == null), this);
            if (graphTree == null || graphTree.startNodeDatas.Count == 0)
            {
                Debug.LogError("No valid GraphTree assigned to DialogueController.", this);
                return;
            }

            // Reset dello stato tra dialoghi diversi — evita "GoBack"/"Repeat" sporchi
            currentDialogueNodeData = null;
            lastDialogueNodeData = null;

            isDialogueActive = true;

            CheckNodeType(GetNextNode(graphTree.startNodeDatas[0]));
            uIController.ShowUI(true);
        }

        /// <summary>
        /// Checks the type of the node and runs the appropriate method.
        /// </summary>
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

        private void RunNode(StartNodeData _nodeData)
        {
            CheckNodeType(GetNextNode(graphTree.startNodeDatas[0]));
        }

        private void RunNode(DialogueNodeData _nodeData)
        {
            if (currentDialogueNodeData != _nodeData)
            {
                lastDialogueNodeData = currentDialogueNodeData;
                currentDialogueNodeData = _nodeData;
            }

            uIController.SetText(
                _nodeData.CharacterName,
                _nodeData.textBox_languages.Find(text => text.LanguageType == LanguageController.Instance.Language).LanguageGenericType);

            uIController.SetImage(_nodeData.SpeakerImage, _nodeData.switchType);

            audioSource.clip = _nodeData.audioClips_List
                .Find(audioclip => audioclip.LanguageType == LanguageController.Instance.Language).LanguageGenericType;
            audioSource.Play();

            MakeButtons(_nodeData.dialogueNodePorts);
        }

        private void RunNode(GraphEventNodeData _nodeData)
        {
            _nodeData.GraphEvent?.Raise();
            CheckNodeType(GetNextNode(_nodeData));
        }

        private void RunNode(EndNodeData _nodeData)
        {
            switch (_nodeData.endNodeType)
            {
                case EndNodeType.End:
                    EndDialogue();
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
        /// Cleanly ends the dialogue and resets internal state.
        /// </summary>
        private void EndDialogue()
        {
            uIController.ShowUI(false);
            audioSource.Stop();
            currentDialogueNodeData = null;
            lastDialogueNodeData = null;
            isDialogueActive = false;
        }

        private void MakeButtons(List<DialogueNodePort> _dialogueNodePorts)
        {
            List<string> texts = new List<string>();
            List<UnityAction> unityActions = new List<UnityAction>();

            foreach (DialogueNodePort nodePort in _dialogueNodePorts)
            {
                texts.Add(nodePort.ChoiceText_List
                    .Find(text => text.LanguageType == LanguageController.Instance.Language).LanguageGenericType);

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