using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LaniakeaCode.Utilities {
    public class UIController : MonoBehaviour
    {

        //Adding References, Serializing for Checks
        [SerializeField]
        private GameObject referenceUI;

        [Header("Text")]
        [SerializeField] private Text panelNameText;
        [SerializeField] private Text panelTextBoxText;

        [Header("Image")]
        [SerializeField] private Image leftImage;
        [SerializeField] private GameObject leftImageGO;
        [SerializeField] private Image rightImage;
        [SerializeField] private GameObject rightImageGO;

        [Header("Buttons")]
        [SerializeField] private Button button;
        [SerializeField] private Text buttonText;
        [SerializeField] private Button button1;
        [SerializeField] private Text buttonText1;
        [SerializeField] private Button button2;
        [SerializeField] private Text buttonText2;
        [SerializeField] private Button button3;
        [SerializeField] private Text buttonText3;

        List<Button> buttons = new List<Button>();
        private List<Text> buttonTexts = new List<Text>();

        private void Awake()
        {
            ShowUI(false);
            buttons.Add(button);
            buttonTexts.Add(buttonText);
            buttons.Add(button1);
            buttonTexts.Add(buttonText1);
            buttons.Add(button2);
            buttonTexts.Add(buttonText2);
            buttons.Add(button3);
            buttonTexts.Add(buttonText3);
        }

        //Setting a bool to activate or deactivate the UI Panel
        public void ShowUI(bool _show)
        {
            referenceUI
                .SetActive(_show);
        }

        //Set the text of the referenced objects
        public void SetText(string _name, string _text)
        {
            panelNameText
                .text = _name;

            panelTextBoxText
                .text = _text;
        }

        public void SetImage(Sprite _image, SimplSwitchType simplSwitch)
        {
            leftImageGO.SetActive(false);
            rightImageGO.SetActive(false);

            if(_image != null)
            {
                if(simplSwitch == SimplSwitchType.On)
                {
                    leftImage
                        .sprite = _image;
                    leftImageGO
                        .SetActive(true);
                }
                else
                {
                    rightImage
                        .sprite = _image;
                    rightImageGO
                        .SetActive(true);
                }
            }
        }

        public void SetButtons(List<string> _buttonTexts, List<UnityAction> _unityActions)
        {
            buttons
                .ForEach(button => button.gameObject.SetActive(false));

            for (int i = 0; i < _buttonTexts.Count; i++)
            {
                buttonTexts[i]
                    .text = _buttonTexts[i];
                buttons[i]
                    .gameObject
                    .SetActive(true);
                buttons[i]
                    .onClick = new Button.ButtonClickedEvent();
                buttons[i]
                    .onClick
                    .AddListener(_unityActions[i]);
            }
        }
    }
}