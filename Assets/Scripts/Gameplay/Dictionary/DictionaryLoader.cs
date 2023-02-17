using System;
using System.IO;
using UnityEngine;

namespace Gameplay.Dictionary
{
    public class DictionaryLoader : IDictionaryLoader
    {
        public string[] Load()
        {
            const string path = "Assets/Resources/ukenglish.txt";
            var reader = new StreamReader(path);
        
            var content = reader.ReadToEndAsync();
            while (true)
            {
                if (content.IsCompletedSuccessfully)
                {
                    var result = content.Result;
                    var dictionary = result.Split(new[] {"\r\n", "\r", "\n"}, StringSplitOptions.None);

                    return dictionary;
                }

                if (content.IsFaulted)
                {
                    Debug.LogError("Error getting dictionary!");
                    break;
                }
            }
            reader.Close();

            return null;
        }
    }
}
