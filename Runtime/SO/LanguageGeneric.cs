using System;
using Fenneig_Dialogue_Editor.Runtime.Enums;

namespace Fenneig_Dialogue_Editor.Runtime.NodesData
{
    [Serializable]
    public class LanguageGeneric<T>
    {
        public LanguageType LanguageType;
        public T LanguageGenericType;
    }
}