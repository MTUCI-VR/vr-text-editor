using TextEditor.Scripts.ActionRecorder;

namespace TextEditor.Scripts.TextEditor.InputField
{
    public class InsertTextAction : ActionBase
    {
        private readonly int _textLength;
        private string _deletedText;

        public InsertTextAction(CustomInputField inputField, int textLength) : base(inputField)
        {
            _textLength = textLength;
        }

        public override void Execute()
        {
        }

        public override void Undo()
        {
            var deleteStartPosition = InputField.caretPosition - _textLength;
            _deletedText = InputField.text.Substring(deleteStartPosition, _textLength);

            InputField.DeleteText(deleteStartPosition, _textLength);
        }

        public override void Redo()
        {
            InputField.InsertText(InputField.caretPosition, _deletedText);

            InputField.caretPosition += _deletedText.Length;
        }
    }
}