using UnityEditor;
using UnityEngine;

namespace Fenneig_Dialogue_Editor.Dialogue_Editor.CSV_Tool
{
    public class CustomTools
    {
        [MenuItem("CustomTools/Dialogue/Update Dialogue Languages")]
        public static void UpdateDialogueLanguage()
        {
            UpdateLanguageType updateLanguageType = new();
            updateLanguageType.UpdateLanguage();
            
            Debug.Log("<color=green>Dialogue languages updated successfully!</color>");
        }

        [MenuItem("CustomTools/Dialogue/Save to CSV")]
        public static void SaveToCSV()
        {
            SaveCSV saveCsv = new();
            saveCsv.Save();
            
            Debug.Log("<color=green>CSV saved successfully!</color>");
        }

        [MenuItem("CustomTools/Dialogue/Load from CSV")]
        public static void LoadFromCSV()
        {
            LoadCSV loadCsv = new();
            loadCsv.Load();
            
            Debug.Log("<color=green>CSV loaded successfully!</color>");
        }
    }
}
