using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace TextEditor.General.Scripts.Ui
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ButtonExtended : Button
    {
        #region Fields

        [SerializeField]
        [Range(0, 1)]
        private float disabledOpacity = 0.5f;

        private CanvasGroup _canvasGroup;

        #endregion

        #region LifeCycle

        protected override void Awake()
        {
            base.Awake();

            _canvasGroup = GetComponent<CanvasGroup>();
        }

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            base.DoStateTransition(state, instant);

            _canvasGroup.alpha = state == SelectionState.Disabled ? disabledOpacity : 1f;
        }

        #endregion
    }
}