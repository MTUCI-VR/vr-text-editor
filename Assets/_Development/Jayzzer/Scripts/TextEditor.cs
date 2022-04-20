using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Development.Jayzzer.Scripts
{
    public class TextEditor : Selectable, IUpdateSelectedHandler
    {
        [SerializeField]
        private TMP_InputField inputField;

        private void OnEnable()
        {
            base.OnEnable();
            inputField.onSelect.AddListener(OnInputSelected);
            inputField.onDeselect.AddListener(OnInputDeselected);
        }

        private void OnDisable()
        {
            base.OnDisable();
            inputField.onSelect.RemoveListener(OnInputSelected);
            inputField.onDeselect.RemoveListener(OnInputDeselected);
        }

        private void OnInputSelected(string eventData)
        {
            Debug.Log("Input Selected");
        }

        private void OnInputDeselected(string eventData)
        {
            Debug.Log("Input Deselected");
        }

        public void OnUpdateSelected(BaseEventData eventData)
        {
            Debug.Log("On Input");
        }
    }
}