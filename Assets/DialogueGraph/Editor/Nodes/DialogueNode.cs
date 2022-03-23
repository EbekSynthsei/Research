using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using LaniakeaCode.Utilities;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

namespace LaniakeaCode.GraphSystem
{
    public class DialogueNode : BaseNode
    {
        private string nodeTitle;
        private List<LanguageGeneric<string>> dialogueBoxTexts = new List<LanguageGeneric<string>>();
        private List<LanguageGeneric<AudioClip>> audioClips = new List<LanguageGeneric<AudioClip>>();

        private List<DialogueNodePort> dialogueNodePorts = new List<DialogueNodePort>();

        private Sprite speakerImage;
        public string NodeTitle { get => nodeTitle; set => nodeTitle = value; }
        public List<LanguageGeneric<string>> DialogueBoxTexts { get => dialogueBoxTexts; set => dialogueBoxTexts = value; }
        public List<LanguageGeneric<AudioClip>> AudioClips { get => audioClips; set => audioClips = value; }
        public Sprite SpeakerImage { get => speakerImage; set => speakerImage = value; }
        public SimplSwitchType SimpleSwitch { get => simpleSwitch; set => simpleSwitch = value; }
        public List<DialogueNodePort> DialogueNodePorts { get => dialogueNodePorts; set => dialogueNodePorts = value; }

        private SimplSwitchType simpleSwitch;

        private TextField nodeTitle_Field;
        private TextField dialogueBoxText_Field;
        private ObjectField controlImage_Field;
        private ObjectField audioClips_Field;
        private EnumField simpleSwitch_Field;

        public DialogueNode()
        {

        }
        public DialogueNode(Vector2 _vector2, GraphEditorWindow _graphEditor, GraphTreeView _graphTreeView)
        {
            editorWindow = _graphEditor;
            graphTreeView = _graphTreeView;

            SetPosition(new Rect(_vector2, defaultNodeSize));
            nodeGUID = Guid.NewGuid().ToString();

            title = "UIElement";
            AddInputPort("Input", Port.Capacity.Multi);
            //Iterating Types To Initialize Generic Lists
            foreach (LanguageType type in (LanguageType[])Enum.GetValues(typeof(LanguageType)))
            {
                DialogueBoxTexts
                    .Add(new LanguageGeneric<string>
                    {
                        LanguageType = type,
                        LanguageGenericType = ""
                    });
                AudioClips
                    .Add(new LanguageGeneric<AudioClip>
                    {
                        LanguageType = type,
                        LanguageGenericType = null
                    });
            }

            //Setting the Text Field For The Name
            nodeTitle_Field = new TextField("NodeTitle");
            nodeTitle_Field
                .RegisterValueChangedCallback(value =>
                nodeTitle = value
                .newValue
                );
            nodeTitle_Field
                .SetValueWithoutNotify(NodeTitle);

            mainContainer
                .Add(nodeTitle_Field);

            //Setting The Field For The Main Text Box
            dialogueBoxText_Field = new TextField("");
            dialogueBoxText_Field.RegisterValueChangedCallback(value => {
                dialogueBoxTexts
                .Find(text => text
                .LanguageType == editorWindow
                .LanguageType
                ).LanguageGenericType = value
                .newValue;
            });
            dialogueBoxText_Field
                .SetValueWithoutNotify(dialogueBoxTexts
                .Find(text => text
                .LanguageType == editorWindow
                .LanguageType
                ).LanguageGenericType);



            //Setting The Field For The Control Image
            //
            controlImage_Field = new ObjectField
            {
                objectType = typeof(Sprite),
                allowSceneObjects = false,
                value = speakerImage
            };

            controlImage_Field
                .RegisterValueChangedCallback(value =>
                {
                    speakerImage = value.newValue as Sprite;
                });
            controlImage_Field
                .SetValueWithoutNotify(speakerImage);

            mainContainer.Add(controlImage_Field);

            //Setting The Field For The Enum Field
            //
            simpleSwitch_Field = new EnumField()
            {
                value = simpleSwitch
            };

            //Enum Field Must be Initialized
            simpleSwitch_Field
                .Init(simpleSwitch);
            simpleSwitch_Field
                .RegisterValueChangedCallback(value =>
                simpleSwitch = (SimplSwitchType)value
                .newValue);

            simpleSwitch_Field
                .SetValueWithoutNotify(simpleSwitch);

            mainContainer.Add(simpleSwitch_Field);

            //Setting The Field For Audio Clips
            audioClips_Field = new ObjectField()
            {
                objectType = typeof(AudioClip),
                allowSceneObjects = false,
                value = audioClips
                .Find(audioClip => audioClip
                .LanguageType == editorWindow
                .LanguageType
                ).LanguageGenericType
            };
            audioClips_Field
                .RegisterValueChangedCallback(value =>
                {
                    audioClips
                      .Find(audioClip => audioClip
                      .LanguageType == editorWindow
                      .LanguageType)
                      .LanguageGenericType = value
                      .newValue as AudioClip;
                });
            audioClips_Field
                .SetValueWithoutNotify(audioClips
                .Find(audioClip => audioClip
                .LanguageType == editorWindow
                .LanguageType)
                .LanguageGenericType);

            mainContainer
                .Add(dialogueBoxText_Field);
            mainContainer
                .Add(audioClips_Field);

            Button addChoiceButton = new Button()
            {
                text = "Add Choice"
            };
            addChoiceButton
                .clicked += () => AddChoicePort(this);

            titleButtonContainer
                .Add(addChoiceButton);
        }

        public void ReloadLanguage()
        {
            dialogueBoxText_Field
                .RegisterValueChangedCallback(value =>
                {
                    dialogueBoxTexts
                        .Find(text => text
                        .LanguageType == editorWindow
                        .LanguageType).LanguageGenericType = value
                    .newValue;
                });
            dialogueBoxText_Field
                .SetValueWithoutNotify(
                dialogueBoxTexts
                .Find(text => text
                .LanguageType == editorWindow
                .LanguageType)
                .LanguageGenericType);

            audioClips_Field
                .RegisterValueChangedCallback(value =>
                audioClips
                .Find(audio => audio
                .LanguageType == editorWindow
                .LanguageType)
                .LanguageGenericType = value
                .newValue as AudioClip);
            audioClips_Field
                .SetValueWithoutNotify(audioClips
                .Find(audio => audio
                .LanguageType == editorWindow
                .LanguageType
                ).LanguageGenericType);

            foreach(DialogueNodePort uiPort in DialogueNodePorts)
            {
                uiPort
                    .choicePortName_Field
                    .RegisterValueChangedCallback(value =>
                    {
                        uiPort
                        .ChoiceText_List
                        .Find(language => language
                        .LanguageType == editorWindow
                        .LanguageType
                        ).LanguageGenericType = value
                        .newValue;
                    });
                uiPort
                    .choicePortName_Field
                    .SetValueWithoutNotify(
                    uiPort
                    .ChoiceText_List
                    .Find(language => language
                    .LanguageType == editorWindow
                    .LanguageType
                    ).LanguageGenericType);
            }
        }

        public Port AddChoicePort(BaseNode _baseNode, DialogueNodePort _dialogueNodePort = null)
        {
            Port port = GetPortInstance(Direction.Output);

            int outputPortCount = _baseNode
                .outputContainer
                .Query("connector")
                .ToList()
                .Count();
            string outputPortName = $"Choice{outputPortCount + 1}";

            //Adding to the nodeport a list of generics and setting this list to the respective outputPort
            //This way each choice will have already a list that has to be exchanged with the NodePortData
            //If No Node is Passed We just pass in a new port setted to the same type

            DialogueNodePort dialogueNodePort = new DialogueNodePort();
            dialogueNodePort.PortGUID = Guid.NewGuid().ToString();

            foreach (LanguageType language in (LanguageType[])Enum.GetValues(typeof(LanguageType)))
            {
                dialogueNodePort
                .ChoiceText_List
                .Add(new LanguageGeneric<string>()
                {
                    LanguageType = language,
                    LanguageGenericType = outputPortName
                });

            }

            if (_dialogueNodePort != null)
            {
                dialogueNodePort.InputGuid = _dialogueNodePort.InputGuid;
                dialogueNodePort.OutputGuid = _dialogueNodePort.OutputGuid;
                dialogueNodePort.PortGUID = _dialogueNodePort.PortGUID;

                foreach (LanguageGeneric<string> languageGeneric in _dialogueNodePort.ChoiceText_List)
                {
                    dialogueNodePort
                        .ChoiceText_List
                        .Find(language => language
                        .LanguageType == languageGeneric
                        .LanguageType
                        ).LanguageGenericType = languageGeneric
                        .LanguageGenericType;
                }
            }
            //Adding A text Field to the Port and initializing it to be the same of the inserted port
            dialogueNodePort
                .choicePortName_Field = new TextField();

            dialogueNodePort
                .choicePortName_Field
                .RegisterValueChangedCallback(value =>
                {
                    dialogueNodePort
                    .ChoiceText_List
                    .Find(language => language
                    .LanguageType == editorWindow
                    .LanguageType
                    ).LanguageGenericType = value
                    .newValue;
                });

            dialogueNodePort
                .choicePortName_Field
                .SetValueWithoutNotify(
                dialogueNodePort
                .ChoiceText_List
                .Find(language =>
                language
                .LanguageType == editorWindow
                .LanguageType
                ).LanguageGenericType);

            port
                .contentContainer
                .Add(dialogueNodePort.choicePortName_Field);
            //Adding A Delete Button On Each Of The Port

            Button deleteButton = new Button(() => DeletePort(_baseNode, port))
            {
                text = "X",
            };

            port
                .contentContainer
                .Add(deleteButton);

            dialogueNodePort
                .MyPort = port;
            port
                .portName = "";

            DialogueNodePorts
                .Add(dialogueNodePort);
           
            _baseNode
                .outputContainer
                .Add(port);

            _baseNode
                .RefreshPorts();
            _baseNode
                .RefreshExpandedState();

            return port;
        }

        private void DeletePort(BaseNode _node, Port _port)
        {
            DialogueNodePort tmp = DialogueNodePorts
                .Find(port => port
                .MyPort == _port);

            DialogueNodePorts
            .Remove(tmp);

            IEnumerable<Edge> portEdge = graphTreeView
                .edges
                .ToList()
                .Where(edge => edge
                .output == _port);

            if (portEdge.Any())
            {
                Edge edge = portEdge
                    .First();
                edge
                    .input
                    .Disconnect(edge);
                edge
                    .output
                    .Disconnect(edge);
                graphTreeView
                    .RemoveElement(edge);
            }

            _node
                .outputContainer
                .Remove(_port);
            _node
                .RefreshPorts();
            _node
                .RefreshExpandedState();
        }
    }
}