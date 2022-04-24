using TextEditor.Scripts.ActionRecorder;

namespace TextEditor.Scripts.TextEditor.InputField
{
    class MoveCaretAction : ActionBase
    {
        private readonly int _previousPosition;
        private int _nextPosition;

        public MoveCaretAction(CustomInputField inputField, int previousPosition) : base(inputField)
        {
            _previousPosition = previousPosition;
        }

        public override void Execute()
        {
        }

        public override void Undo()
        {
            _nextPosition = InputField.caretPosition;
            InputField.caretPosition = _previousPosition;
        }

        public override void Redo()
        {
            InputField.caretPosition = _nextPosition;
        }
    }
}