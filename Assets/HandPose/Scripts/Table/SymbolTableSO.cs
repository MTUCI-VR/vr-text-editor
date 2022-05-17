using UnityEngine;

namespace SymbolTable
{
    [System.Serializable]
    public struct SymbolRows
    {
        public string[] symbols;
    }

    [CreateAssetMenu(menuName = "Table", fileName = "SymbolTable")]
    public class SymbolTableSO : ScriptableObject
    {
        [SerializeField] private SymbolRows[] rows;

        public SymbolRows[] Rows
        {
            get { return rows; }
        }
    }
}