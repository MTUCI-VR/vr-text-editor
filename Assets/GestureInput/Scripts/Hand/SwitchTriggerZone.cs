using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GestureInput.Hand
{
    [RequireComponent(typeof(Rigidbody))]
    public class SwitchTriggerZone : MonoBehaviour
    {
        #region Events

        public UnityEvent OnTriggerEnterZone;

        #endregion

        #region LifeCycle

        private void OnTriggerEnter(Collider other)
        {
            if(!other.CompareTag("Player"))
                return;

            OnTriggerEnterZone?.Invoke();
        }

        #endregion
    }
}
