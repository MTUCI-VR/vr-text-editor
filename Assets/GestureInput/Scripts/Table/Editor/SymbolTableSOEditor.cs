using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GestureInput.SymbolTable
{
    [CustomEditor(typeof(SymbolTableSO))]
    public class SymbolTableSOEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var symbolTable = (SymbolTableSO)target;

            EditorUtility.SetDirty(symbolTable);

            if (symbolTable.Rows == null)
            {
                symbolTable.Rows = new List<Row>();
            }

            List<Row> rows = symbolTable.Rows;
            int countRows = Mathf.Max(0, EditorGUILayout.IntField("Кол-во строк", rows.Count));

            while (countRows > rows.Count)
            {
                rows.Add(new Row());
            }

            while (countRows < rows.Count)
            {
                rows.RemoveAt(rows.Count - 1);
            }

            int rowNum = 1;

            foreach (var row in rows)
            {
                row.shoowRow = EditorGUILayout.Foldout(row.shoowRow, "Строка " + rowNum++, true);

                if (row.shoowRow)
                {
                    EditorGUILayout.BeginVertical("Box");

                    if (row.symbols == null)
                    {
                        row.symbols = new List<Symbol>();
                    }

                    List<Symbol> symbols = row.symbols;
                    int countSymbols = Mathf.Max(0, EditorGUILayout.IntField("Кол-во символов", symbols.Count));

                    while (countSymbols > symbols.Count)
                    {
                        symbols.Add(new Symbol());
                    }

                    while (countSymbols < symbols.Count)
                    {
                        symbols.RemoveAt(symbols.Count - 1);
                    }

                    int symbolNum = 1;

                    foreach (var symbol in symbols)
                    {
                        EditorGUILayout.BeginVertical("Box");

                        symbol.isCustom = EditorGUILayout.Toggle("Управляющий", symbol.isCustom);


                        if (!symbol.isCustom)
                        {
                            symbol.symbol = EditorGUILayout.TextField("Символ " + symbolNum++, symbol.symbol);
                        }
                        else
                        {
                            symbol.symbolType = (SymbolType)EditorGUILayout.EnumPopup("Тип символа", symbol.symbolType);
                            symbol.sprite = EditorGUILayout.ObjectField("Картинка", symbol.sprite, typeof(Sprite), true) as Sprite;
                        }

                        EditorGUILayout.EndVertical();
                        EditorGUILayout.Space();
                    }

                    EditorGUILayout.EndVertical();
                    EditorGUILayout.Space();
                }
            }

            ShowColorSwitch(symbolTable);

            serializedObject.ApplyModifiedProperties();
        }

        private void ShowColorSwitch(SymbolTableSO symbolTable)
        {
            EditorGUILayout.Space(5);
            EditorGUILayout.PrefixLabel("Настройки политры столбиков");
            EditorGUILayout.BeginVertical("Box");

            int countColumn = 0;

            foreach (var row in symbolTable.Rows)
            {
                if (row.symbols.Count > countColumn)
                {
                    countColumn = row.symbols.Count;
                }
            }

            if (symbolTable.colors == null)
            {
                symbolTable.colors = new List<Color>(countColumn);
            }

            List<Color> colors = symbolTable.colors;

            while (colors.Count < countColumn)
            {
                colors.Add(Color.gray);
            }

            while (colors.Count > countColumn)
            {
                colors.RemoveAt(colors.Count - 1);
            }

            for (int i = 0; i < colors.Count; i++)
            {
                colors[i] = EditorGUILayout.ColorField("Столбец "+ (1 + i), colors[i]);
            }

            foreach (var row in symbolTable.Rows)
            {
                int colorIndex = 0;

                foreach (var symbol in row.symbols)
                {
                    if (colorIndex < colors.Count)
                    {
                        symbol.colorFon = colors[colorIndex++];
                    }

                }
            }

            EditorGUILayout.EndVertical();
        }
    }
}
