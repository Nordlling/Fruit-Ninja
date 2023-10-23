using System;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "WordEndingsConfig", menuName = "Configs/WordEndings")]
    public class WordEndingsConfig : ScriptableObject
    {
        public WordDictionary[] FruitDictionary;
    }
    
    [Serializable]
    public class WordDictionary
    {
        public int Number;
        public string Word;
    }
    
}