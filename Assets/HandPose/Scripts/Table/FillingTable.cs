using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SymbolTable
{
    public class FillingTable : MonoBehaviour
    {
        [SerializeField] private SymbolTableSO symbolTable;
        [SerializeField] private GameObject rowSlotPrefab;
        [SerializeField] private Transform content;

        private List<RowSlot> _rows = new List<RowSlot>();

        public UnityEvent<List<RowSlot>> OnUpdateTable;

        private void Start()
        {
            Filling(symbolTable);
        }

        [ContextMenu("Filling")]
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

        private void CreateRows(int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                var rowSlot = Instantiate(rowSlotPrefab, content).GetComponent<RowSlot>();
                _rows.Add(rowSlot);
            }
        }
    }
}
