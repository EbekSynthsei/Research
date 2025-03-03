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
    /// <summary>
    /// Represents a dialogue node in the dialogue graph.
    /// </summary>
    public class DialogueNode : BaseNode
    {
        private string characterName;
        private List<LanguageGeneric<string>> dialogueBoxTexts = new List<LanguageGeneric<string>>();
        private List<LanguageGeneric<AudioClip>> audioClips = new List<LanguageGeneric<AudioClip>>();

        private List<DialogueNodePort> dialogueNodePorts = new List<DialogueNodePort>();

        private Sprite speakerImage;
        public string CharacterName { get => characterName; set => characterName = value; }
        public List<LanguageGeneric<string>> DialogueBoxTexts { get => dialogueBoxTexts; set => dialogueBoxTexts = value; }
        public List<LanguageGeneric<AudioClip>> AudioClips { get => audioClips; set => audioClips = value; }
        public Sprite SpeakerImage { get => speakerImage; set => speakerImage = value; }
        public SimplSwitchType SimpleSwitch { get => simpleSwitch; set => simpleSwitch = value; }
        public List<DialogueNodePort> DialogueNodePorts { get => dialogueNodePorts; set => dialogueNodePorts = value; }

        private SimplSwitchType simpleSwitch;

        private TextField characterName_Field;
        private TextField dialogueBoxText_Field;
        private ObjectField speakerImage_Field;
        private ObjectField audioClips_Field;
        private EnumField simpleSwitch_Field;
        private Image preview;

        public DialogueNode()
        {
            Debug.Log("<color=green>DialogueNode created with default constructor.</color>");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogueNode"/> class.
        /// </summary>
        /// <param name="_vector2">The position of the node.</param>
        /// <param name="_graphEditor">The editor window.</param>
        /// <param name="_graphTreeView">The graph tree view.</param>
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
            characterName_Field = new TextField("NodeTitle");
            characterName_Field.RegisterValueChangedCallback(value =>
            {
                characterName = value.newValue;
                Debug.Log("<color=blue>CharacterName updated:</color> " + characterName);
            });
            characterName_Field.SetValueWithoutNotify(CharacterName);

            mainContainer.Add(characterName_Field);

            //Setting The Field For The Main Text Box
            dialogueBoxText_Field = new TextField("");
            dialogueBoxText_Field.RegisterValueChangedCallback(value => {
                dialogueBoxTexts
                .Find(text => text
                .LanguageType == editorWindow
                .SelectedLanguage
                ).LanguageGenericType = value
                .newValue;
                Debug.Log("<color=blue>DialogueBoxText updated:</color> " + value.newValue);
            });
            dialogueBoxText_Field
                .SetValueWithoutNotify(dialogueBoxTexts
                .Find(text => text
                .LanguageType == editorWindow
                .SelectedLanguage
                ).LanguageGenericType);



            //Setting The Field For The Speaker Image
            //
            speakerImage_Field = new ObjectField
            {
                objectType = typeof(Sprite),
                allowSceneObjects = false,
                value = speakerImage
            };
            preview = new Image();
            preview.AddToClassList("speakerPreview");

            speakerImage_Field.RegisterValueChangedCallback(value =>
            {
                Sprite tmp = value.newValue as Sprite;
                speakerImage = tmp;
                preview.image = (tmp != null ? tmp.texture : null);
                Debug.Log("<color=blue>SpeakerImage updated:</color> " + speakerImage);
            });
            speakerImage_Field.SetValueWithoutNotify(speakerImage);

            mainContainer.Add(speakerImage_Field);

            //Setting The Field For The Enum Field
            //
            simpleSwitch_Field = new EnumField()
            {
                value = simpleSwitch
            };

            //Enum Field Must be Initialized
            simpleSwitch_Field.Init(simpleSwitch);
            simpleSwitch_Field.RegisterValueChangedCallback(value =>
            {
                simpleSwitch = (SimplSwitchType)value.newValue;
                Debug.Log("<color=blue>SimpleSwitch updated:</color> " + simpleSwitch);
            });

            simpleSwitch_Field.SetValueWithoutNotify(simpleSwitch);

            mainContainer.Add(simpleSwitch_Field);

            //Setting The Field For Audio Clips
            audioClips_Field = new ObjectField()
            {
                objectType = typeof(AudioClip),
                allowSceneObjects = false,
                value = audioClips
                .Find(audioClip => audioClip
                .LanguageType == editorWindow
                .SelectedLanguage
                ).LanguageGenericType
            };
            audioClips_Field.RegisterValueChangedCallback(value =>
            {
                audioClips
                  .Find(audioClip => audioClip
                  .LanguageType == editorWindow
                  .SelectedLanguage)
                  .LanguageGenericType = value
                  .newValue as AudioClip;
                Debug.Log("<color=blue>AudioClip updated:</color> " + value.newValue);
            });
            audioClips_Field.SetValueWithoutNotify(audioClips
                .Find(audioClip => audioClip
                .LanguageType == editorWindow
                .SelectedLanguage)
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

            Debug.Log("<color=green>DialogueNode created at position: </color>" + _vector2);
        }

        public override void LoadValueIntoField()
        {
            audioClips_Field
                .SetValueWithoutNotify(audioClips
                .Find(language => language
                .LanguageType == editorWindow
                .SelectedLanguage)
                .LanguageGenericType);
            characterName_Field
                .SetValueWithoutNotify(characterName);
            dialogueBoxText_Field
                .SetValueWithoutNotify(dialogueBoxTexts
                .Find(language => language
                .LanguageType == editorWindow
                .SelectedLanguage)
                .LanguageGenericType);
            speakerImage_Field
                .SetValueWithoutNotify(speakerImage);
            simpleSwitch_Field
                .SetValueWithoutNotify(simpleSwitch);
                
            if(speakerImage != null)
            {
                preview.image = ((Sprite)speakerImage_Field.value).texture;
            }

            Debug.Log("<color=blue>DialogueNode values loaded into field.</color>");
        }

        public override void ReloadLanguage()
        {
            dialogueBoxText_Field
                .RegisterValueChangedCallback(value =>
                {
                    dialogueBoxTexts
                        .Find(text => text
                        .LanguageType == editorWindow
                        .SelectedLanguage).LanguageGenericType = value
                    .newValue;
                    Debug.Log("<color=blue>DialogueBoxText updated for new language:</color> " + value.newValue);
                });
            dialogueBoxText_Field
                .SetValueWithoutNotify(
                dialogueBoxTexts
                .Find(text => text
                .LanguageType == editorWindow
                .SelectedLanguage)
                .LanguageGenericType);

            audioClips_Field
                .RegisterValueChangedCallback(value =>
                {
                    audioClips
                    .Find(audio => audio
                    .LanguageType == editorWindow
                    .SelectedLanguage)
                    .LanguageGenericType = value
                    .newValue as AudioClip;
                    Debug.Log("<color=blue>AudioClip updated for new language:</color> " + value.newValue);
                });
            audioClips_Field
                .SetValueWithoutNotify(audioClips
                .Find(audio => audio
                .LanguageType == editorWindow
                .SelectedLanguage)
                .LanguageGenericType);

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
                        .SelectedLanguage
                        ).LanguageGenericType = value
                        .newValue;
                        Debug.Log("<color=blue>ChoiceText updated for new language:</color> " + value.newValue);
                    });
                uiPort
                    .choicePortName_Field
                    .SetValueWithoutNotify(
                    uiPort
                    .ChoiceText_List
                    .Find(language => language
                    .LanguageType == editorWindow
                    .SelectedLanguage
                    ).LanguageGenericType);
            }

            Debug.Log("<color=blue>DialogueNode language reloaded.</color>");
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
                    .SelectedLanguage
                    ).LanguageGenericType = value
                    .newValue;
                    Debug.Log("<color=blue>ChoicePortName updated:</color> " + value.newValue);
                });

            dialogueNodePort
                .choicePortName_Field
                .SetValueWithoutNotify(
                dialogueNodePort
                .ChoiceText_List
                .Find(language =>
                language
                .LanguageType == editorWindow
                .SelectedLanguage
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

            //Using the Guid as a Name and Hiding via uss
            port
                .portName = dialogueNodePort
                .PortGUID;

            Label portNameLabel = port
                .contentContainer
                .Q<Label>("type");
            portNameLabel
                .AddToClassList("PortName");

            DialogueNodePorts
                .Add(dialogueNodePort);
           
            _baseNode
                .outputContainer
                .Add(port);

            //Refresh
            _baseNode
                .RefreshPorts();
            _baseNode
                .RefreshExpandedState();

            Debug.Log("<color=green>Choice port added to DialogueNode.</color>");

            return port;
        }

        private void DeletePort(BaseNode _node, Port _port)
        {
            DialogueNodePort tmp = DialogueNodePorts
                .Find(port => port
                .PortGUID == _port
                .portName);

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

            Debug.Log("<color=red>Choice port removed from DialogueNode.</color>");
        }
    }
}