using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace SymbolTable
{
    public class FillingTable : MonoBehaviour
    {
        #region Fields

        [SerializeField] private SymbolTableSO symbolTable;
        [SerializeField] private GameObject rowSlotPrefab;
        [SerializeField] private Transform content;

        private List<RowSlot> _rows = new List<RowSlot>();

        #endregion

        #region Events

        public UnityEvent<List<RowSlot>> OnUpdateTable;

        #endregion

        #region LifeCycle

        private void Start()
        {
            Filling(symbolTable);
        }

        #endregion

        #region PublicMethods

        public void Filling(SymbolTableSO symbols)
        {
            _rows.Clear();

            for (int i = 0; i < content.childCount; i++)
            {
                Destroy(content.GetChild(i).gameObject);
            }

            foreach (var row in symbols.Rows)
            {
                var rowSlot = Instantiate(rowSlotPrefab, content).GetComponent<RowSlot>();
                rowSlot.Initialize(row.symbols);

                _rows.Add(rowSlot);
            }

            OnUpdateTable?.Invoke(_rows);
        }

        #endregion

        #region Debug

#if UNITY_EDITOR

        [ContextMenu("Filling")]
        public void FillingDebug()
        {
            Filling(symbolTable);
        }
#endif
        #endregion
    }
}
