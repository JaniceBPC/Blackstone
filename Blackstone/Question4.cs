using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Blackstone
{
    public class Question4
    {
         class Denomination
        {
            public Denomination(string name, decimal amount = 0)
            {
                Name = name;
                Amount = amount;
            }

            public string Name { get;  }
            public decimal Amount { get; }
            public override string ToString() => $"{Name}, {Amount:C}";
        }

        private static List<Denomination> Error =>
            new List<Denomination>
            {
                new Denomination("ERROR")
            };
        private static List<Denomination> NoChangeDue =>
            new List<Denomination>
            {
                new Denomination("ZERO", 0M)
            };
        private static List<Denomination> Denominations =>
            new List<Denomination>
            {
                new Denomination("PENNY", 0.01M),
                new Denomination("NICKLE", 0.05M),
                new Denomination("DIME", 0.10M),
                new Denomination("QUARTER", 0.25M),
                new Denomination("HALF DOLLAR", 0.50M),
                new Denomination("ONE", 1.00M),
                new Denomination("TWO", 2.00M),
                new Denomination("FIVE", 5.00M),
                new Denomination("TEN", 10.00M),
                new Denomination("TWENTY", 20.00M),
                new Denomination("FIFTY", 50.00M),
                new Denomination("ONE HUNDRED", 100.00M),
            };
        private static List<Denomination> Cascade(decimal? amount)
        {
            var change = new List<Denomination>();
            var denominations = Denominations.OrderByDescending(x=>x.Amount).ToList();
            var residual = amount;

            while (residual>0)
            {
                denominations = denominations
                    .SkipWhile(x => x.Amount > residual)
                    .ToList();

                var denomination = denominations.First();

                while (residual>=denomination.Amount)
                {
                    change.Add(denomination);
                    residual = residual - denomination.Amount;
                }

                denominations = denominations.Skip(1).ToList();
            }

            return change;
        }
        private static List<Denomination> Change(decimal purchasePrice, decimal amountTendered)
        {
            if (purchasePrice > amountTendered)
            {
                return Error;
            }
            if (purchasePrice == amountTendered)
            {
                return NoChangeDue;
            }

            return Cascade(amountTendered-purchasePrice);
        }
        public static string MakeChange(string purchaseInfo)
        {
            var purchasePrice = decimal.Parse(purchaseInfo.Split(';').First());
            var amountTendered = decimal.Parse(purchaseInfo.Split(';').Last());

            var change = Change(purchasePrice, amountTendered);

            return string.Join(", ", change.Select(x => x.Name));
        }
        [Test]
        public void Case4_Test1()
        {
            var change = Change(2, 1);

            Assert.AreEqual(change.Count, 1);
            Assert.AreEqual(change.First().Name, Error.First().Name);
        }
        [Test]
        public void Case4_Test2()
        {
            var change = Change(2, 2);

            Assert.AreEqual(change.Count, 1);
            Assert.AreEqual(change.First().Name, NoChangeDue.First().Name);
        }
        public void Case4_Test3()
        {
            var amountTendered =
                Denominations.Select(x => x.Amount*2-0.01M).Sum();
            var purchasePrice =
                Denominations.Select(x => x.Amount).Sum();
            var change = Change(purchasePrice, amountTendered);

            var expected = amountTendered - purchasePrice;
            var actual = change.Select(x => x.Amount).Sum();

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Case4_Test4()
        {
            var actual = MakeChange("0.01;0.02");

            Assert.AreEqual(actual, "PENNY");
        }
    }
}