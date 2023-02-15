using System;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Gameplay.Core
{
    public class WordSorter
    {
        private string _input;
        private readonly string[] _englishDictionary;
        
        public WordSorter()
        {
            var path = "Assets/Resources/ukenglish.txt";
            var reader = new StreamReader(path);
        
            var content = reader.ReadToEndAsync();
            content.Start();
            content.Wait();

            if (content.IsCompletedSuccessfully)
            {
                var result = content.Result;
                _englishDictionary = result.Split(new[] {"\r\n", "\r", "\n"}, StringSplitOptions.None);
            }
            else
            {
                Debug.LogError("Something went wrong when trying to obtain the english dictionary!");
            }
            reader.Close();
        }
        
        public void StartWordSearch(string input)
        {
            _input = input;
            ProcessInput();
        }

        private void ProcessInput()
        {
            var words =  _englishDictionary.Where(word => word.Length >= 2).Where(word => IsWordInString(word, _input)).ToList();
        }
        
        private static bool IsWordInString(string word, string source)
        {
            var letterList = source.ToList();

            return word.All(letterList.Remove);
        }
    }
}
