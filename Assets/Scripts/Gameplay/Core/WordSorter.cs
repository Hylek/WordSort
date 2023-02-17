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
        private Dictionary<string, List<string>> _sortedDictionary;
        private readonly List<string> _combinations;
        
        public WordSorter()
        {
            LoadDictionary();
            SortDictionary();
            _combinations = new List<string>();
        }

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

        private void SortDictionary()
        {
            _sortedDictionary = new Dictionary<string, List<string>>();
            foreach (var word in _englishDictionary)
            {
                var sortedWord = SortByAlphabeticalOrder(word);
                if (_sortedDictionary.TryGetValue(sortedWord, out var wordList))
                {
                    wordList.Add(word);
                    _sortedDictionary[sortedWord] = wordList;
                }
                else
                {
                    var list = new List<string> { word };
                    _sortedDictionary.Add(sortedWord, list);
                }
            }
        }
        
        public List<string> StartWordSearch(string input)
        {
            _input = input;
            _combinations.Clear();
            InputCombinations_Recursive("", _input);
            
            var words = new List<string>();
            foreach (var combination in _combinations)
            {
                if (_sortedDictionary.TryGetValue(combination, out var results))
                {
                    words.AddRange(results);
                }
            }

            return words;
        }
        
        private void InputCombinations_Recursive(string result, string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return;
            }
            for (var i = 0; i < input.Length; i++)
            {
                var remaining = input.Substring(0, i) + input.Substring(i + 1);

                var sortedCombo = SortByAlphabeticalOrder(input[i] + result);
                if ((result + input[i]).Length > 1)
                {
                    if (_combinations.Contains(sortedCombo))
                    {
                        // If we have seen this combination of letters before we can safely skip it.
                        continue;
                    }
                    _combinations.Add(sortedCombo);
                }
			
                InputCombinations_Recursive(result + input[i], remaining);
            }
        }

        private static bool IsWordInString(string word, string source)
        {
            var letterList = source.ToList();

            return word.All(letterList.Remove);
        }
        
        private static string SortByAlphabeticalOrder(string input)
        {
            var chars = input.ToCharArray();
            Array.Sort(chars);
            return new string(chars);
        }
    }
}
