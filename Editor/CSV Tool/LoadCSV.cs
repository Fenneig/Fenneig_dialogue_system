using System;
using System.Collections.Generic;
using System.IO;
using Fenneig_Dialogue_Editor.Dialogue_Editor.Runtime.Enums;
using Fenneig_Dialogue_Editor.Dialogue_Editor.Runtime.NodesData;
using Fenneig_Dialogue_Editor.Dialogue_Editor.Runtime.SO;
using UnityEditor;
using UnityEngine;

namespace Fenneig_Dialogue_Editor.Dialogue_Editor.CSV_Tool
{
    public class LoadCSV
    {
        private string _csvDirectoryName = "Resources/Dialogue Editor/CSV File";
        private string _csvFileName = "DialogueCSV_Load.csv";
        private string[] _csvHeader;
        
        private string _name = "-Name";

        private string DirectoryPath => $"{Application.dataPath}/{_csvDirectoryName}";


        private CSVReader _csvReader = new();
        
        public void Load()
        {
            List<List<string>> result = _csvReader.ParseCSV(File.ReadAllText($"{DirectoryPath}/{_csvFileName}"));

            List<string> headers = result[0];
            List<DialogueContainerSO> dialogueContainers = Helper.FindAllDialogueContainers();
            
            dialogueContainers.ForEach(dialogueContainer =>
            {
                dialogueContainer.DialogueNodeData.ForEach(nodeData =>
                {
                    LoadIntoNode(result,headers,nodeData);
                    nodeData.DialogueNodePorts.ForEach(nodePort =>
                    {
                        LoadIntoNodePort(result, headers, nodePort);
                    });
                });
                EditorUtility.SetDirty(dialogueContainer);
                AssetDatabase.SaveAssets();
            });
        }

        private void LoadIntoNode(List<List<string>> result, List<string> headers, DialogueNodeData nodeData)
        {
            result.ForEach(line =>
            {
                if (!line[0].Contains(nodeData.NodeGuid)) return;
                for (int i = 0; i < line.Count; i++)
                {
                    foreach (var languageType in (LanguageType[]) Enum.GetValues(typeof(LanguageType)))
                    {
                        if (headers[i] != languageType.ToString()) continue;
                        
                        if (line[0].Contains(_name))
                            nodeData.CharacterName.Find(text => text.LanguageType == languageType).LanguageGenericType = line[i];
                        else
                            nodeData.Texts.Find(text => text.LanguageType == languageType).LanguageGenericType = line[i];
                    }
                }
            });
        }

        private void LoadIntoNodePort(List<List<string>> result, List<string> headers, DialogueNodePort nodePort)
        {
            result.ForEach(line =>
            {
                if (!line[0].Contains(nodePort.PortGuid)) return;
                for (int i = 0; i < line.Count; i++)
                {
                    foreach (var languageType in (LanguageType[]) Enum.GetValues(typeof(LanguageType)))
                    {
                        if (headers[i] == languageType.ToString())
                        {
                            nodePort.TextLanguages.Find(text => text.LanguageType == languageType).LanguageGenericType = line[i];
                        }
                    }
                }
            });
        }
    }
}