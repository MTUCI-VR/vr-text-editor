using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GestureInput.SymbolTable
{
    public class SwitchTable : MonoBehaviour
    {
        #region Fields

        [SerializeField] private FillingTable fillingTable;
        [SerializeField] private SymbolTableSO[] symbolTabls;
        [SerializeField] private Animator animator;

        private int _currentTableIndex = 0;

        #endregion

        #region PyblicMethods

        [ContextMenu("Switch")]
        public void Switch()
        {
            animator.SetTrigger("ChangeTable");
            _currentTableIndex = (_currentTableIndex + 1) % symbolTabls.Length;
            fillingTable.Filling(symbolTabls[_currentTableIndex]);
        }

        #endregion
    }
}