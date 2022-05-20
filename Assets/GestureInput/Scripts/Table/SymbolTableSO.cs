using System.Collections.Generic;
using UnityEngine;

namespace GestureInput.SymbolTable
{
    public enum SymbolType
    {
        Space,
        Eraser
    }

    [System.Serializable]
    public class Symbol
    {
        public string symbol;
        public Color colorFon;
        public bool isCustom = false;

        public SymbolType symbolType;
        public Sprite sprite;
    }

    [System.Serializable]
    public class Row 
    {
        public List<Symbol> symbols;
        public bool shoowRow = false;
    }

    [CreateAssetMenu(menuName = "Table", fileName = "SymbolTable")]
    public class SymbolTableSO : ScriptableObject
    {
        [SerializeField] private List<Row> rows;

        public List<Color> colors = new List<Color>();
        public bool isShowColor = false;

        public List<Row> Rows
        {
            get { return rows; }
            set { rows = value; }
        }
    }
}