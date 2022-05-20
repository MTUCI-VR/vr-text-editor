using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GestureInput.SymbolTable
{
    public class RowSlot : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject symbolSlotPrefab;
        [SerializeField] private Animator animator;

        private List<SymbolSlot> _symbols = new List<SymbolSlot>();

        #endregion

        #region PublicMethods

        public void Initialize(List<Symbol> symbols)
        {
            FillingRow(symbols);
        }

        public string GetSymbol(int index)
        {
            if (index < 0 || index >= _symbols.Count)
            {
                Debug.LogError("Not found symbol!");
                return "error";
            }

            return _symbols[index].Symbol;
        }

        [ContextMenu("Selected")]
        public void Selected()
        {
            animator.SetBool("Selected", true);

            foreach (var symbol in _symbols)
            {
                var cuurentColor = symbol.FonImage.color;
                symbol.FonImage.color = new Color(cuurentColor.r, cuurentColor.g, cuurentColor.b, 1);
            }
        }

        [ContextMenu("UnSelected")]
        public void UnSelected()
        {
            animator.SetBool("Selected", false);

            foreach (var symbol in _symbols)
            {
                var cuurentColor = symbol.FonImage.color;
                symbol.FonImage.color = new Color(cuurentColor.r, cuurentColor.g, cuurentColor.b, 0.3f);
            }
        }

        #endregion

        #region PrivateMethods

        private void FillingRow(List<Symbol> symbols)
        {
            _symbols.Clear();

            foreach (var symbol in symbols)
            {
                var symbolSlot = Instantiate(symbolSlotPrefab, transform).GetComponent<SymbolSlot>();
                symbolSlot.Initialize(symbol);

                _symbols.Add(symbolSlot);
            }

            UnSelected();
        }

        #endregion
    }
}
