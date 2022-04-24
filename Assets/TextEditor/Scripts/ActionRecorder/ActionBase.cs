using TextEditor.Scripts.TextEditor.InputField;

namespace TextEditor.Scripts.ActionRecorder
{
    public abstract class ActionBase
    {
        protected readonly CustomInputField InputField;

        protected ActionBase(CustomInputField inputField)
        {
            InputField = inputField;
        }

        public abstract void Execute();

        public abstract void Undo();

        public abstract void Redo();
    }
}