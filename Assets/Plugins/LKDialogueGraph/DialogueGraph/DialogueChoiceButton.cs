using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LaniakeaCode.Utilities
{
    /// <summary>
    /// Componente sul prefab del singolo bottone di scelta dialogo.
    /// Richiede: Button (root o figlio), TextMeshProUGUI (per il testo scelta).
    /// Il prefab deve avere anche un LayoutElement se il container usa un LayoutGroup
    /// con controllo dimensioni figli disattivato.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class DialogueChoiceButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI label;

        private void Awake()
        {
            if (button == null) button = GetComponent<Button>();
        }

        /// <summary>
        /// Configura testo e azione del bottone. Rimuove listener precedenti
        /// per sicurezza (i bottoni vengono comunque distrutti/ricreati ad ogni nodo).
        /// </summary>
        public void Setup(string text, UnityAction onClick)
        {
            if (label != null)
                label.text = text;

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(onClick);
        }
    }
}
