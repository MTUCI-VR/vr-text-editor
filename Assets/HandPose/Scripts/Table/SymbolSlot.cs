using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SymbolTable
{
    public class SymbolSlot : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI symbolText;
        [SerializeField] private Image fonImage;
        [SerializeField] private Animator animator;

        public Image FonImage
        {
            get { return fonImage; }
        }

        /// <summary>
        /// Возвращает символ закрепленный за этим слотом
        /// </summary>
        public string Symbol
        {
            get
            {
                animator.SetTrigger("Selected");     
                return symbolText.text;
            }
        }

        public void Initialize(string symbol)
        {
            symbolText.text = symbol;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(!other.CompareTag("Player"))
                return;

            Logger.Instance.PushText(Symbol);
         //   var b = Symbol; 
        }
    }
}
