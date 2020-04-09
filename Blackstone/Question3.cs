using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;

namespace Blackstone
{
    public class Question3
    {
        // Number of occurrences of a char in an array of chars
        private static int Occurrences(char @char, char[] chars) =>
            chars.Count(x => x==@char);

        // List the number of chars that need to be either added or removed such that the two strings have the same number of occurrences for the union of distinct chars for each
        private static List<int> Swaps(string a, string b)
        {
            var chars = a.Concat(b).Distinct().OrderBy(x => x).ToList();

            var aChars = a.ToArray();
            var bChars = b.ToArray();

            // Generate frequency count dictionaries for each string
            var aDict = a
                .Distinct()
                .ToDictionary(x => x, y=> Occurrences(y, aChars));
            var bDict = b
                .Distinct()
                .ToDictionary(x => x, y => Occurrences(y, bChars));

            // List the differences in counts per char between the two strings
            return chars
                .Select(x => new { A = !aDict.ContainsKey(x) ? 0 : aDict[x], B = !bDict.ContainsKey(x) ? 0 : bDict[x] })
                .Select(x=> x.A-x.B)
                .OrderByDescending(x=>x)
                .ToList();
        }
        // Determine the minimum numbers of changes to normalize the frequency counts of the distinct chars between strings
        // For each instance where there is an excess in the number of occurrences for a char in the first string, make a corresponding number of adjustments 
        //   where there are more occurrences for a char in the 2nd string vs the first
        // Note that the sum of the differences is zero
        private static int MinimumSwaps(string a, string b)
        {
            var swaps = Swaps(a, b);

            int totalSwaps = 0;

            int i = 0;
            while (i < swaps.Count)
            {

                if (swaps[i] > 0)
                {
                    int j = 0;
                    int toRemove = swaps[i];
                    totalSwaps += toRemove;

                    while (toRemove > 0)
                    {
                        if (swaps[j] < 0)
                        {
                            int change = swaps[j];
                            swaps[j] = toRemove + swaps[j] >= 0 ? 0 : swaps[j] + toRemove;
                            toRemove += change;
                        }

                        j++;
                    }
                }

                i++;
            }

            return totalSwaps;
        }
        // if not the same length than -1
        // if exactly the same then one letter needs to be changed
        // if the sets of chars are different the calculate the minimum number of changes to for a pair of anagrams
        private static int Differences(string a, string b)
        {
            if (a.Length != b.Length)
            {
                return -1;
            }

            if (a == b)
            {
                return 1;
            }

            var z = MinimumSwaps(a, b);

            return z;
        }
        private List<int> Differences(string [] aWords, string [] bWords)
        {
            if (aWords.Length != bWords.Length)
            {
                throw new ArgumentException();
            }

            return aWords.Zip(bWords, Differences).Select(x => x).ToList();
        }
        [Test]
        public void Test3()
        {
            var a = new String[] { "a", "jk", "abb", "mn", "abc" };
            var b = new String[] { "bb", "kj", "bbc", "op", "def" };

            var differences1 = Differences(a, b);

            differences1.ForEach(x => Debug.WriteLine(x));

            var c = new List<string>();
            var d = new List<string>();

            c.Add(C());
            d.Add(D());

            var differences2 = Differences(c.ToArray(), d.ToArray());

            differences2.ForEach(x => Debug.WriteLine(x));
        }

        public static string C2() => "AAAABB";
        public static string C3() => "ACCCBD";
        public static string C0() => "abc";
        public static string C1() => "bba";
        public static string C()
        {
            return "nzdvdqtgotierllkqtvfztesncymazaoigzubwmrwhbdjhvjeuiudbcapaxkmjouafhcrecuajnttdtsuruwyjjuwarmbqeoeupczckvlxbqkjyooloycdzqkryevetidwgqwhnpjmtkkfaosnkxggwyrwfyhldjoxitphsaouguxqmfexvzmwcscghzpabdravqavvtowunseshtmwtznjqxxidabbzqybcxzdrdfvbpdrmcfzxgaunshgcasarnjmgbocbphwtjwxwgbxofcsnhftptpagpsphqtefdhfbusvhfaoqhyuzndyhqeduesdlnsgdtihxjmplquzunzbccnmakyaqkzoyzdwltkwtszsozoiuokuqbezltoujbzbgbtobuqttlwdilfpgqnilklanmxuahsrzxqahmfhgbflhboiyntvrvwxcvblxngmnhtksabkufqhwktr";
        }

        public static string D()
        {
            return "iyfbpvaoohjlftasyotlbjycdqckqfwugsqqokhaolltremzkdouhtwkhxcjnjvzgevlhatvmibnhleqhwiscncgqzkcsedjyidqketigvsidklteisqzzecssseswawgntsubrvhkjdxzxlmnhkknwpfhpavtbowseovzliqntbzrazxpihyxwemougxnssmtbhsomzbnxszgrlkemajvlvzoiyxqtaedetftnkmnsqogpptjzngadmeidxvbbabhsfttfoqhwfzjocslvuepjwnwshoujrwbhiobirflentxqkzdjipxpdflujjzyopfafhfqimbqakxgpqnpevrwdizsyowgdpzdlhczbgxbjvzhveytajflfhoxlusxbkvanxspqhosviygqdjymceyngdwqtbsfglhnnethbweacnztqyculrlmqsibyhoebymlkonlhrteyskaogk";
        }
    }
}