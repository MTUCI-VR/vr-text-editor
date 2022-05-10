using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TextEditor.Scripts.TextEditor.InputField
{
    public class CustomInputField : TMP_InputField
    {
        #region Fields

        private readonly Event _processingEvent = new();
        private readonly ActionRecorder.ActionRecorder _actionRecorder = new();

        #endregion

        #region Events

        public event Action<Vector2> OnSelectedText;
        public event Action OnUnselectedText;

        #endregion

        #region Public Methods

        public override void OnUpdateSelected(BaseEventData eventData)
        {
            if (!isFocused) return;

            bool shouldUpdateLabel = ProcessEvents();
            if (shouldUpdateLabel)
            {
                UpdateLabel();
            }

            eventData.Use();
        }

        public void ProcessKeyPress(Event keyEvent)
        {
            KeyPressed(keyEvent);
        }

        public void Undo()
        {
            _actionRecorder.Undo();
        }

        public void Redo()
        {
            _actionRecorder.Redo();
        }

        public bool EnterKey(Event keyEvent)
        {
            return ProcessKeyDown(keyEvent);
        }

        public void InsertText(int offset, string textValue)
        {
            text = text.Insert(offset, textValue);
        }

        public void DeleteText(int offset, int length)
        {
            text = text.Remove(offset, length);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            var initialCaretPosition = caretPosition;

            base.OnPointerDown(eventData);

            if (caretPosition != initialCaretPosition)
            {
                RecordMoveCaretAction(initialCaretPosition);
            }

            OnUnselectedText?.Invoke();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);

            if (m_CaretPosition == m_CaretSelectPosition) return;
            OnSelectedText?.Invoke(InputFieldUtils.GetCaretCanvasPosition(m_TextComponent, m_CaretSelectPosition));
        }

        #endregion

        #region Private Methods

        private bool ProcessKeyDown(Event keyEvent)
        {
            if (CheckIsUndoPressed(keyEvent))
            {
                _actionRecorder.Undo();
                return true;
            }

            if (CheckIsRedoPressed(keyEvent))
            {
                _actionRecorder.Redo();
                return true;
            }

            string symbolToDelete;
            DeleteTextAction deleteTextAction;
            switch (keyEvent.keyCode)
            {
                case KeyCode.Backspace:
                {
                    symbolToDelete = text.Substring(caretPosition - 1, 1);
                    deleteTextAction = new DeleteTextAction(this, symbolToDelete);
                    _actionRecorder.Record(deleteTextAction);
                    break;
                }
                case KeyCode.Delete:
                    symbolToDelete = text.Substring(caretPosition, 1);
                    deleteTextAction = new DeleteTextAction(this, symbolToDelete, false);
                    _actionRecorder.Record(deleteTextAction);
                    break;
                case KeyCode.LeftArrow:
                case KeyCode.RightArrow:
                case KeyCode.DownArrow:
                case KeyCode.UpArrow:
                    RecordMoveCaretAction(caretPosition);

                    break;
                case KeyCode.None:
                {
                    var enterKeyAction = new InsertTextAction(this, 1);
                    _actionRecorder.Record(enterKeyAction);

                    break;
                }
            }

            OnUnselectedText?.Invoke();

            _actionRecorder.ClearRedo();

            return KeyPressed(keyEvent) == EditState.Continue;
        }

        private bool ProcessEvents()
        {
            var shouldUpdateLabel = false;
            while (Event.PopEvent(_processingEvent))
            {
                switch (_processingEvent.rawType)
                {
                    case EventType.KeyDown:
                        shouldUpdateLabel = true;
                        bool shouldContinue = ProcessKeyDown(_processingEvent);
                        if (!shouldContinue)
                        {
                            if (!wasCanceled)
                            {
                                SendOnSubmit();
                            }

                            DeactivateInputField();
                        }

                        break;
                    case EventType.ValidateCommand:
                    case EventType.ExecuteCommand:
                        switch (_processingEvent.commandName)
                        {
                            case "SelectAll":
                                SelectAll();
                                shouldUpdateLabel = true;
                                break;
                        }

                        break;
                }
            }

            return shouldUpdateLabel;
        }

        private bool CheckIsUndoPressed(Event keyEvent)
        {
            var currentEventModifiers = keyEvent.modifiers;
            var isCtrlPressed = SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX
                ? (currentEventModifiers & EventModifiers.Command) != 0
                : (currentEventModifiers & EventModifiers.Control) != 0;
            var isShiftPressed = (currentEventModifiers & EventModifiers.Shift) != 0;
            var isAltPressed = (currentEventModifiers & EventModifiers.Alt) != 0;

            var isOnlyCtrlPressed = isCtrlPressed && !isAltPressed && !isShiftPressed;

            return Application.isEditor
                ? isCtrlPressed && isShiftPressed && keyEvent.keyCode == KeyCode.Z
                : isOnlyCtrlPressed && keyEvent.keyCode == KeyCode.Z;
        }

        private bool CheckIsRedoPressed(Event keyEvent)
        {
            var currentEventModifiers = keyEvent.modifiers;
            var isCtrlPressed = SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX
                ? (currentEventModifiers & EventModifiers.Command) != 0
                : (currentEventModifiers & EventModifiers.Control) != 0;
            var isShiftPressed = (currentEventModifiers & EventModifiers.Shift) != 0;
            var isAltPressed = (currentEventModifiers & EventModifiers.Alt) != 0;

            var isOnlyCtrlPressed = isCtrlPressed && !isAltPressed && !isShiftPressed;

            return Application.isEditor
                ? isCtrlPressed && isShiftPressed && keyEvent.keyCode == KeyCode.R
                : isOnlyCtrlPressed && keyEvent.keyCode == KeyCode.R;
        }

        private void RecordMoveCaretAction(int previousPosition)
        {
            var moveCaretAction = new MoveCaretAction(this, previousPosition);
            _actionRecorder.Record(moveCaretAction);
        }

        #endregion
    }
}