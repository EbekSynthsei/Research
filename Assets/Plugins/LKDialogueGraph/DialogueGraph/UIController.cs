using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LaniakeaCode.Utilities
{
    /// <summary>
    /// Controls the dialogue UI panel.
    /// NON è un Singleton: resta un componente normale, referenziato via Inspector
    /// da DialogueController (che invece è Singleton, vedi GraphDataParser<T>).
    /// Refactor: bottoni dinamici (nessun limite fisso), TextMeshPro invece di Text legacy,
    /// layout non centrato sullo schermo per evitare sovrapposizioni.
    /// </summary>
    public class UIController : MonoBehaviour
    {
        [Header("Panel Root")]
        [SerializeField] private GameObject referenceUI;

        [Header("Text (TMP)")]
        [SerializeField] private TextMeshProUGUI panelNameText;
        [SerializeField] private TextMeshProUGUI panelTextBoxText;

        [Header("Image")]
        [SerializeField] private Image leftImage;
        [SerializeField] private GameObject leftImageGO;
        [SerializeField] private Image rightImage;
        [SerializeField] private GameObject rightImageGO;

        [Header("Choices — Dynamic List")]
        [Tooltip("Prefab con Button + TextMeshProUGUI (DialogueChoiceButton.cs). " +
                 "Nessun limite al numero di scelte: uno per ogni DialogueNodePort del nodo.")]
        [SerializeField] private DialogueChoiceButton choiceButtonPrefab;

        [Tooltip("Deve avere un LayoutGroup (Vertical consigliato) + ContentSizeFitter " +
                 "(Vertical Fit = Preferred Size) per adattarsi dinamicamente al numero di bottoni.")]
        [SerializeField] private RectTransform choicesContainer;

        private readonly List<DialogueChoiceButton> spawnedButtons = new List<DialogueChoiceButton>();

        private void Awake()
        {
            ShowUI(false);
        }

        /// <summary>
        /// Setting a bool to activate or deactivate the UI Panel.
        /// </summary>
        public void ShowUI(bool _show)
        {
            Debug.Log("UIController: ShowUI called with show=" + _show + ", referenceUI=null?" + (referenceUI == null), this);
            if (referenceUI == null)
            {
                Debug.LogError("UIController: referenceUI is null!", this);
                return;
            }
            referenceUI.SetActive(_show);
        }

        /// <summary>
        /// Set the text of the referenced objects.
        /// </summary>
        public void SetText(string _name, string _text)
        {
            panelNameText.text = _name;
            panelTextBoxText.text = _text;
        }

        public void SetImage(Sprite _image, SimplSwitchType simplSwitch)
        {
            leftImageGO.SetActive(false);
            rightImageGO.SetActive(false);

            if (_image != null)
            {
                if (simplSwitch == SimplSwitchType.On)
                {
                    leftImage.sprite = _image;
                    leftImageGO.SetActive(true);
                }
                else
                {
                    rightImage.sprite = _image;
                    rightImageGO.SetActive(true);
                }
            }
        }

        /// <summary>
        /// Popola dinamicamente i bottoni di scelta: uno per ogni testo/azione passati,
        /// senza limite massimo. Distrugge e ricrea i bottoni ad ogni nodo per evitare
        /// listener residui da nodi precedenti.
        /// </summary>
        public void SetButtons(List<string> _buttonTexts, List<UnityAction> _unityActions)
        {
            ClearButtons();

            if (choiceButtonPrefab == null || choicesContainer == null)
            {
                Debug.LogError("UIController: choiceButtonPrefab o choicesContainer non assegnati!", this);
                return;
            }

            for (int i = 0; i < _buttonTexts.Count; i++)
            {
                DialogueChoiceButton instance = Instantiate(choiceButtonPrefab, choicesContainer);
                instance.Setup(_buttonTexts[i], _unityActions[i]);
                spawnedButtons.Add(instance);
            }
        }

        private void ClearButtons()
        {
            foreach (var b in spawnedButtons)
            {
                if (b != null)
                    Destroy(b.gameObject);
            }
            spawnedButtons.Clear();
        }
    }
}