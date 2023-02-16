using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Gameplay.Core
{
    public class WordSorter
    {
        private string _input;
        private string[] _englishDictionary;
        
        public WordSorter() => LoadDictionary();

        private void LoadDictionary()
        {
            var path = "Assets/Resources/ukenglish.txt";
            var reader = new StreamReader(path);
        
            var content = reader.ReadToEndAsync();

            while (true)
            {
                if (content.IsCompletedSuccessfully)
                {
                    var result = content.Result;
                    _englishDictionary = result.Split(new[] {"\r\n", "\r", "\n"}, StringSplitOptions.None);

                    break;
                }

                if (content.IsFaulted)
                {
                    Debug.LogError("Error getting dictionary!");
                    break;
                }
            }
            reader.Close();
        }
        
        public List<string> StartWordSearch(string input)
        {
            _input = input;
            
            return  _englishDictionary.Where(word => word.Length >= 2).Where(word => IsWordInString(word, _input)).ToList();
        }

        private static bool IsWordInString(string word, string source)
        {
            var letterList = source.ToList();

            return word.All(letterList.Remove);
        }
    }
}
