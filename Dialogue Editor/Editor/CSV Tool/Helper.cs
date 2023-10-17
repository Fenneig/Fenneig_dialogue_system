using System.Collections.Generic;
using System.IO;
using System.Linq;
using Fenneig_Dialogue_Editor.Dialogue_Editor.Runtime.SO;
using UnityEditor;
using UnityEngine;

namespace Fenneig_Dialogue_Editor.Dialogue_Editor.CSV_Tool
{
    public static class Helper
    {
        public static List<T> FindAllObjectFromResources<T>()
        {
            List<T> returnList = new();
            string resourcesPath = Application.dataPath + "/Resources";
            string[] directories = Directory.GetDirectories(resourcesPath, "*", SearchOption.AllDirectories);
            
            foreach (var directory in directories)
            {
                string directoryPath = directory.Substring(resourcesPath.Length + 1);
                T[] result = Resources.LoadAll(directoryPath, typeof(T)).Cast<T>().ToArray();
                
                foreach (var item in result)
                {
                    if (!returnList.Contains(item))
                    {
                        returnList.Add(item);
                    }
                }
            }

            return returnList;
        }

        public static List<DialogueContainerSO> FindAllDialogueContainers()
        {
            string[] guids = AssetDatabase.FindAssets("t:DialogueContainerSO");

            return guids.Select(AssetDatabase.GUIDToAssetPath).Select(AssetDatabase.LoadAssetAtPath<DialogueContainerSO>).ToList();
        }
    }
}