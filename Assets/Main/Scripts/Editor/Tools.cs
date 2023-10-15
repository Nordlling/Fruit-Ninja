using UnityEditor;
using UnityEngine;

namespace Main.Scripts.Editor
{
    public class Tools
    {
        [MenuItem("Tools/ClearPrefs")]
        public static void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}