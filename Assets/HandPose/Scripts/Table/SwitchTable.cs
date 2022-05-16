using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SymbolTable
{
    public class SwitchTable : MonoBehaviour
    {
        [SerializeField] private FillingTable fillingTable;
        [SerializeField] private SymbolTableSO[] symbolTabls;
        [SerializeField] private Animator animator;

        private int _currentTableIndex = 0;

        [ContextMenu("Switch")]
        public void Switch()
        {
            animator.SetTrigger("ChangeTable");
            _currentTableIndex = (_currentTableIndex + 1) % symbolTabls.Length;
            fillingTable.Filling(symbolTabls[_currentTableIndex]);
        }

    }
}