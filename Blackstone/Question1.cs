using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;

namespace Blackstone
{
    public class Question1
    {
        public List<int> cutBamboo(List<int> lengths)
        {
            var counts = new List<int>();

            lengths = lengths.OrderBy(x => x).ToList();

            while (lengths.Any())
            {
                // record #pieces at start of operation
                counts.Add(lengths.Count);

                var minLength = lengths.First();

                lengths = lengths
                    .SkipWhile(x => x == minLength)
                    .Select(x => x - minLength)
                    .ToList();
            }

            return counts;
        }

        [Test]
        public void TestA()
        {
            var lengths = new List<int>
            {
                5,
                4,
                4,
                2,
                2,
                8
            };

            var results = cutBamboo(lengths);

            results.ForEach(x => Debug.WriteLine(x));

            Assert.IsTrue(results.Any());
        }

        [Test]
        public void Testb()
        {
            Debug.WriteLine($"\nTest1");
            var lengths = new List<int>
            {
                1,
                2,
                3,
                4,
                3,
                3,
                2,
                1,
            };

            var results = cutBamboo(lengths);

            results.ForEach(x => Debug.WriteLine(x));

            Assert.IsTrue(results.Any());
        }
     }
}