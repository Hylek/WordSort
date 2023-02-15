using System;
using UnityEngine;

namespace Gameplay.Core
{
    public class WordManager : MonoBehaviour
    {
        private WordSorter _wordSorter;

        private void Awake()
        {
            _wordSorter = new WordSorter();
        }
    }
}
