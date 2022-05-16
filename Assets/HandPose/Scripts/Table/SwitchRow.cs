using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SymbolTable
{
    public class SwitchRow : MonoBehaviour
    {
        [SerializeField] private FillingTable fillingTable;

        private List<RowSlot> _rows = new List<RowSlot>();
        private int _currentSelectRow = -1;

        public UnityEvent<RowSlot> OnSelectedRow;

        private void Awake()
        {
            fillingTable.OnUpdateTable.AddListener(OnUpdateTable);
        }

        private void OnUpdateTable(List<RowSlot> rows)
        {
            _rows = rows;
        }

        public void Switch(int index)
        {
            if (index < 0 || index >= _rows.Count)
            {
                Debug.LogError("Такой строки нету!");
                return;
            }

            if (_currentSelectRow >= 0 && _currentSelectRow < _rows.Count)
            {
                _rows[_currentSelectRow].UnSelected();
            }

            _currentSelectRow = index;

            _rows[index].Selected();
            OnSelectedRow?.Invoke(_rows[index]);
        }

        [ContextMenu("NextUp")]
        public void NextUp()
        {
            var nextIndex = (_currentSelectRow - 1) % _rows.Count;

            if (nextIndex < 0)
                nextIndex = _rows.Count - 1;

            Switch(nextIndex);
        }

        [ContextMenu("NextDown")]
        public void NextDown()
        {
            var nextIndex = (_currentSelectRow + 1) % _rows.Count;
            Switch(nextIndex);
        }
    }
}
