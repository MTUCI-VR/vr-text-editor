using TextEditor.Scripts.TextEditor.InputField;
using UnityEngine;
using UnityEngine.UI;

namespace TextEditor.Scripts.TextEditor
{
    public class TextEditor : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private CustomInputField inputField;

        [SerializeField]
        private Button undoButton;

        [SerializeField]
        private Button redoButton;

        [SerializeField]
        private TextActions textActions;

        private RectTransform _textActionsTransform;

        #endregion

        #region LifeCycle

        private void Awake()
        {
            textActions.gameObject.SetActive(false);
            _textActionsTransform = textActions.GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            undoButton.onClick.AddListener(inputField.Undo);
            redoButton.onClick.AddListener(inputField.Redo);

            inputField.OnSelectedText += OnSelectedText;
            inputField.OnUnselectedText += OnUnselectedText;
        }

        private void OnDisable()
        {
            undoButton.onClick.RemoveListener(inputField.Undo);
            redoButton.onClick.RemoveListener(inputField.Redo);

            inputField.OnSelectedText -= OnSelectedText;
            inputField.OnUnselectedText -= OnUnselectedText;
        }

        #endregion

        #region Private Methods

        private void ShowTextActions(Vector2 showPosition)
        {
            _textActionsTransform.localPosition = showPosition - new Vector2(0, 80);
            textActions.gameObject.SetActive(true);
        }

        private void OnSelectedText(Vector2 lastPosition)
        {
            ShowTextActions(lastPosition);
        }

        private void OnUnselectedText()
        {
            textActions.gameObject.SetActive(false);
        }

        #endregion
    }
}