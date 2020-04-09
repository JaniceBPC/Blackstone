using System.Diagnostics;
using NUnit.Framework;

namespace Blackstone
{
    public class Sample
    {
        // Manually check output vs expected
        [Test]
        public void Sample_Test()
        {
            Debug.WriteLine($"\nSample");

            int n = 15;

            for (int i = 0; i <= n; i++)
            {
                if (i % 3 == 0 && i % 5 == 0)
                {
                    Debug.WriteLine($"FizzBuz");
                }
                if (i % 3 == 0 && i % 5 != 0)
                {
                    Debug.WriteLine($"Fizz");
                }
                if (i % 3 != 0 && i % 5 == 0)
                {
                    Debug.WriteLine($"Buz");
                }
                if (i % 3 != 0 && i % 5 != 0)
                {
                    Debug.WriteLine($"i={i}");
                }
            }

            Assert.AreEqual(n, 15);
        }
    }
}