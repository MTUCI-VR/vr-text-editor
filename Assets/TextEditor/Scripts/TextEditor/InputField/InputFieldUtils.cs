using TMPro;
using UnityEngine;

namespace TextEditor.Scripts.TextEditor.InputField
{
    public class InputFieldUtils
    {
        public static Vector2 GetCaretCanvasPosition(TMP_Text textComponent, int caretPosition)
        {
            TMP_CharacterInfo caretCharacterInfo = textComponent.textInfo.characterInfo[caretPosition];
            Vector3 charPosition = caretCharacterInfo.topLeft;

            return charPosition;
        }
    }
}