using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GestureInput.SymbolTable
{
    public class SymbolSlot : MonoBehaviour
    {
        #region Fields

        [SerializeField] private TextMeshProUGUI symbolText;
        [SerializeField] private Image fonImage;
        [SerializeField] private Animator animator;

        #endregion

        #region Properties

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

        #endregion

        #region LifeCycle

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;

            Logger.Instance.PushText(Symbol);
        }

        #endregion

        #region PublicMethods

        public void Initialize(Symbol symbol)
        {
            symbolText.text = symbol.symbol;
            FonImage.color = symbol.colorFon;
        }

        #endregion
    }
}
