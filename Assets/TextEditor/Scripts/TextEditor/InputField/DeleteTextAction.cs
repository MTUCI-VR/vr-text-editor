using TextEditor.Scripts.ActionRecorder;

namespace TextEditor.Scripts.TextEditor.InputField
{
    class DeleteTextAction : ActionBase
    {
        private readonly string _deletedText;
        private readonly bool _shouldMoveCaret;

        public DeleteTextAction(CustomInputField inputField, string deletedText, bool shouldMoveCaret = true) : base(
            inputField)
        {
            _deletedText = deletedText;
            _shouldMoveCaret = shouldMoveCaret;
        }

        public override void Execute()
        {
        }

        public override void Undo()
        {
            InputField.InsertText(InputField.caretPosition, _deletedText);
            if (_shouldMoveCaret)
            {
                InputField.caretPosition += _deletedText.Length;
            }
        }

        public override void Redo()
        {
            InputField.DeleteText(InputField.caretPosition - _deletedText.Length + 1, _deletedText.Length);
        }
    }
}