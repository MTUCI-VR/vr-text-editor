using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GestureInput.SymbolTable
{
    public class TableController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private SwitchRow switchRow;

        private RowSlot _currentRow;

        #endregion

        #region LifeCycle

        private void OnEnable()
        {
            if(switchRow != null)
            {
                switchRow.OnSelectedRow.AddListener(OnSelectedRow);
            }
                
        }

        private void OnDisable()
        {
            if (switchRow != null)
            {
                switchRow.OnSelectedRow.RemoveListener(OnSelectedRow);
            }
        }

        #endregion

        #region PublicMethods

        public string GetSymbol(int index)
        {
            if(_currentRow == null)
            {
                Debug.LogError("Object null!");
                return "error";
            }

            return _currentRow.GetSymbol(index);
        }

        public void GetSymbool1(int index)
        {
            if (_currentRow == null)
            {
                Debug.LogError("Object null!");
                return ;
            }
            
            Logger.Instance.PushText(_currentRow.GetSymbol(index));
        }

        #endregion

        #region PrivateMethods

        private void OnSelectedRow(RowSlot rowSlot)
        {
            _currentRow = rowSlot;
        }

        #endregion
    }
}
