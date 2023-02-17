using System.Collections.Generic;
using Gameplay.Core;
using NUnit.Framework;

namespace Tests.EditMode
{
    public class WordSearchTests
    {
        [Test]
        public void ThreeLetterSearch()
        {
            var wordSearcher = new WordSorter();
            var expectedResults = new List<string>() { "cat", "act", "at", "ta", "ca" };
            var results = wordSearcher.StartWordSearch("cat");

            var counter = 0;
            foreach (var result in expectedResults)
            {
                if (results.Contains(result))
                {
                    counter++;
                }
            }
            Assert.AreEqual(expectedResults.Count, counter);
        }
        
        [Test]
        public void SixLetterSearchWithRepeatedLetters()
        {
            var wordSearcher = new WordSorter();
            var expectedResults = new List<string>() { "cc", "ch", "church", "cur", "huh", "ruc" };
            var results = wordSearcher.StartWordSearch("church");

            var counter = 0;
            foreach (var result in expectedResults)
            {
                if (results.Contains(result))
                {
                    counter++;
                }
            }
            Assert.AreEqual(expectedResults.Count, counter);
        }
    }
}
