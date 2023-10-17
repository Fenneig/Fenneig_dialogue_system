using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Fenneig_Dialogue_Editor.Dialogue_Editor.Runtime.Enums;
using Fenneig_Dialogue_Editor.Dialogue_Editor.Runtime.SO;
using UnityEngine;

namespace Fenneig_Dialogue_Editor.Dialogue_Editor.CSV_Tool
{
    public class SaveCSV
    {
        private string _csvDirectoryName = "Resources/Dialogue Editor/CSV File";
        private string _csvFileName = "DialogueCSV_Save.csv";
        private string _csvSeparator = ",";
        private string[] _csvHeader;
        private string _idName = "Guid ID";
        private string _name = "Name";
        private string _dialogueName = "Dialogue name";

        private string DirectoryPath => $"{Application.dataPath}/{_csvDirectoryName}";

        private string FilePath => $"{DirectoryPath}/{_csvFileName}";

        public void Save()
        {
            List<DialogueContainerSO> dialogueContainers = Helper.FindAllDialogueContainers();
            CreateFile();

            dialogueContainers.ForEach(dialogueContainer =>
            {
                dialogueContainer.DialogueNodeData.ForEach(nodeData =>
                {
                    List<string> texts = new()
                    {
                        nodeData.NodeGuid, 
                        dialogueContainer.name
                    };

                    foreach (var languageType in (LanguageType[])Enum.GetValues(typeof(LanguageType)))
                    {
                        string temp = nodeData.Texts.Find(language => language.LanguageType == languageType).LanguageGenericType.Replace("\"", "\"\"");
                        texts.Add($"\"{temp}\"");
                    }
                    
                    AppendToFile(texts);
                    
                    texts = new() 
                    {
                        $"{nodeData.NodeGuid}-{_name}",
                        dialogueContainer.name
                    };
                    
                    foreach (var languageType in (LanguageType[])Enum.GetValues(typeof(LanguageType)))
                    {
                        string temp = nodeData.CharacterName.Find(language => language.LanguageType == languageType).LanguageGenericType.Replace("\"", "\"\"");
                        texts.Add($"\"{temp}\"");
                    }
                    
                    AppendToFile(texts);
                    
                    nodeData.DialogueNodePorts.ForEach(nodePort =>
                    {
                        texts = new()
                        {
                            nodePort.PortGuid,
                            dialogueContainer.name
                        };

                        foreach (var languageType in (LanguageType[])Enum.GetValues(typeof(LanguageType)))
                        {
                            string temp = nodePort.TextLanguages.Find(language => language.LanguageType == languageType).LanguageGenericType.Replace("\"", "\"\"");
                            texts.Add($"\"{temp}\"");
                        }
                        AppendToFile(texts);
                    });
                    
                });
            });
        }

        private void VerifyDirectory()
        {
            string directory = DirectoryPath;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        private void MakeHeader()
        {
            List<string> headerText = new()
            {
                _idName,
                _dialogueName
            };
            headerText.AddRange(from language in (LanguageType[]) Enum.GetValues(typeof(LanguageType)) select language.ToString());

            _csvHeader = headerText.ToArray();
        }

        private void CreateFile()
        {
            VerifyDirectory();
            MakeHeader();
            using StreamWriter sw = File.CreateText(FilePath);
            string finalString = "";
            foreach (var header in _csvHeader)
            {
                if (finalString != "")
                {
                    finalString += _csvSeparator;
                }

                finalString += header;
            }

            sw.WriteLine(finalString);
        }

        private void AppendToFile(List<string> strings)
        {
            using StreamWriter sw = File.AppendText(FilePath);
            string finalString = "";
            foreach (var text in strings)
            {
                if (finalString != "") finalString += _csvSeparator;
                finalString += text;
            }

            sw.WriteLine(finalString);
        }
    }
}