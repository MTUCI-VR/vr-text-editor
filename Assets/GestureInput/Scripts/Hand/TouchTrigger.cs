using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GestureInput.Hand
{
    public enum FingersType
    {
        None,
        Thumb,
        Index,
        Middle,
        Ring,
        Pinky
    }

    public class TouchTrigger : MonoBehaviour
    {
        #region Fields

        [SerializeField] private FingersType thisFinger;
        [SerializeField] private FingersType touchFinger;

        #endregion

        #region Properties

        public FingersType ThisFinger
        {
            get { return thisFinger; }
        }

        #endregion

        #region Events

        public UnityEvent OnTouchEnter;

        #endregion

        #region LifeCycle

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<TouchTrigger>(out var touch))
            {
                if (touch.ThisFinger == touchFinger)
                {
                    OnTouchEnter?.Invoke();
                }
            }
        }

        #endregion
    }
}
