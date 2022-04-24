using System.Collections.Generic;

namespace TextEditor.Scripts.ActionRecorder
{
    public class ActionRecorder
    {
        private readonly Stack<ActionBase> _undoActions = new();
        private readonly Stack<ActionBase> _redoActions = new();

        public void Record(ActionBase action)
        {
            _undoActions.Push(action);
            action.Execute();
        }

        public void Undo()
        {
            if (!_undoActions.TryPop(out var action)) return;

            action.Undo();
            _redoActions.Push(action);
        }

        public void Redo()
        {
            if (!_redoActions.TryPop(out var action)) return;

            action.Redo();
            Record(action);
        }

        public void ClearRedo()
        {
            _redoActions.Clear();
        }
    }
}