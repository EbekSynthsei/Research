using LaniakeaCode.GraphSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LaniakeaCode.Utilities
{
    public class DialogueController : GraphDataParser
    {
        [SerializeField] private UIController uIController;
        [SerializeField] private AudioSource audioSource;

        private DialogueNodeData currentDialogueNodeData;
        private DialogueNodeData lastDialogueNodeData;

        /// <summary>
        /// CHANGE THIS FUNCTION!
        /// </summary>
        /// //TODO : READ ABOVE
        private void Awake()
        {
            uIController = FindObjectOfType<UIController>();
            audioSource = GetComponent<AudioSource>();

            
        }

        public void StartUIPanel()
        {
            CheckNodeType(GetNextNode(graphTree.startNodeDatas[0]));
            uIController
                .ShowUI(true);
        }

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

        //START NODE RUN
        private void RunNode(StartNodeData _nodeData)
        {
            CheckNodeType(GetNextNode(graphTree.startNodeDatas[0]));
        }

        //UI NODE RUN
        private void RunNode(DialogueNodeData _nodeData)
        {
            if (currentDialogueNodeData != _nodeData)
            {
                lastDialogueNodeData = currentDialogueNodeData;
                currentDialogueNodeData = _nodeData;
            }
            uIController
                .SetText(_nodeData
                .CharacterName, _nodeData
                .textBox_languages
                .Find(text => text
                .LanguageType == LanguageController
                .Instance
                .Language
                ).LanguageGenericType);

            uIController
                .SetImage(_nodeData.SpeakerImage, _nodeData.switchType);


            audioSource
                .clip = _nodeData
                .audioClips_List
                .Find(audioclip => audioclip
                .LanguageType == LanguageController
                .Instance
                .Language
                ).LanguageGenericType;

            audioSource
                .Play();

            MakeButtons(_nodeData
                .dialogueNodePorts);

        }

        //EVENT NODE RUN
        private void RunNode(GraphEventNodeData _nodeData)
        {
            if(_nodeData
                .graphEvent != null)
            {
                _nodeData
                    .graphEvent
                    .Raise();
            }
            CheckNodeType(GetNextNode(_nodeData));
        }

        //END NODE RUN
        private void RunNode(EndNodeData _nodeData)
        {
            switch (_nodeData.endNodeType)
            {
                case EndNodeType.End:
                    uIController
                        .ShowUI(false);
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

        private void MakeButtons(List<DialogueNodePort> _dialogueNodePorts)
        {
            List<string> texts = new List<string>();
            List<UnityAction> unityActions = new List<UnityAction>();

            foreach(DialogueNodePort nodePort in _dialogueNodePorts)
            {
                texts
                    .Add(nodePort
                    .ChoiceText_List
                    .Find(text => text
                    .LanguageType == LanguageController
                    .Instance
                    .Language
                    ).LanguageGenericType);

                UnityAction tempAction = null;
                tempAction += () =>
                {
                    CheckNodeType(GetNodeByGuid(nodePort
                        .InputGuid));
                    audioSource.Stop();
                };
                unityActions
                    .Add(tempAction);
            }
            uIController
                .SetButtons(texts, unityActions);
        }
    }
}