using System;
using System.Collections.Generic;
using System.Linq;
using Fenneig_Dialogue_Editor.Dialogue_Editor.Runtime.Enums;
using Fenneig_Dialogue_Editor.Dialogue_Editor.Runtime.NodesData;
using Fenneig_Dialogue_Editor.Dialogue_Editor.Runtime.SO;

namespace Fenneig_Dialogue_Editor.Dialogue_Editor.CSV_Tool
{
    public class UpdateLanguageType
    {
        public void UpdateLanguage()
        {
            List<DialogueContainerSO> dialogueContainers = Helper.FindAllDialogueContainers();

            dialogueContainers.ForEach(dialogueContainer =>
            {
                dialogueContainer.DialogueNodeData.ForEach(dialogueNodeData =>
                {
                    dialogueNodeData.Texts = UpdateLanguageGeneric(dialogueNodeData.Texts);
                    dialogueNodeData.AudioClips = UpdateLanguageGeneric(dialogueNodeData.AudioClips);
                    dialogueNodeData.CharacterName = UpdateLanguageGeneric(dialogueNodeData.CharacterName);

                    foreach (var nodePort in dialogueNodeData.DialogueNodePorts)
                    {
                        nodePort.TextLanguages = UpdateLanguageGeneric(nodePort.TextLanguages);
                    }
                });
            });
        }

        private List<LanguageGeneric<T>> UpdateLanguageGeneric<T>(List<LanguageGeneric<T>> languageGenerics)
        {
            List<LanguageGeneric<T>> returnList = ((LanguageType[]) Enum.GetValues(typeof(LanguageType))).Select(languageType => new LanguageGeneric<T> {LanguageType = languageType}).ToList();

            foreach (var languageGeneric in languageGenerics.Where(languageGeneric => returnList.Find(language => language.LanguageType == languageGeneric.LanguageType) != null))
            {
                returnList.Find(language => language.LanguageType == languageGeneric.LanguageType).LanguageGenericType = languageGeneric.LanguageGenericType;
            }

            return returnList;
        } 
    }
}