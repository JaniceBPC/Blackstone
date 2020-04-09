using System.Diagnostics;
using System.Linq;
using NUnit.Framework;

namespace Blackstone
{
    public class Question2
    {
        // constraint, sequence of words delimited by a single space, ending in a "."
        public static string arrange(string sentence)
        {
            var words = sentence
                .Replace(".", "").Split(' ')
                .Select(x => x.ToLower())
                .OrderBy(x=> x.Length)
                .ToList();

            var newSentence = string.Join(" ", words);

            return $"{newSentence.Substring(0, 1).ToUpper()}{newSentence.Substring(1)}.";
        }

        [Test]
        public void Test2_Case0()
        {
            Debug.WriteLine($"\nTest2");
            Debug.WriteLine(arrange("Cats and hats"));
            Debug.WriteLine(arrange("In the lines are order printer reverse."));
            Debug.WriteLine(arrange("I here come"));
            Debug.WriteLine(arrange("I to love code."));
        }
     }
}