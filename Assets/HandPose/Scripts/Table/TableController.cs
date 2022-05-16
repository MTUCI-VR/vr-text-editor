using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SymbolTable
{
    public class TableController : MonoBehaviour
    {
        [SerializeField] private SwitchRow switchRow;

        private RowSlot _currentRow;

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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                GetSymbol(0);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                GetSymbol(1);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                GetSymbol(2);
            }
        }

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

        private void OnSelectedRow(RowSlot rowSlot)
        {
            _currentRow = rowSlot;
        }
    }
}
