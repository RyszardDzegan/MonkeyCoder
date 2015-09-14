using MonkeyCoder.Variables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static NUnit.Framework.Assert;

namespace MonkeyCoder.PoC
{
    [TestFixture]
    public class MinMax
    {
        private class Variables
        {
            public int Min { get; set; }
            public int Max { get; set; }

            public override string ToString()
            {
                return $"Min: {Min}\n" + $"Max: {Max}\n";
            }
        }

        private class VariablesWithPredicate : Variables
        {
            public Expression<Func<int, int, bool>> MinPredicate { get; set; }
            public Expression<Func<int, int, bool>> MaxPredicate { get; set; }

            public override string ToString()
            {
                return base.ToString() + $"Predicate: {MinPredicate}\n" + $"Predicate: {MaxPredicate}\n";
            }
        }

        [Test]
        public void Original_min_max_algorithm()
        {
            var input = new[] { 1, 5, 6, 2, 9, 7, 3, 8, 4, 0 };
            var min = 10;
            var max = -1;

            foreach (var item in input)
            {
                if (item < min)
                    min = item;

                if (item > max)
                    max = item;
            }

            AreEqual(0, min);
            AreEqual(9, max);
        }

        [Test]
        public void Solves_min_max_algorithm_when_initial_values_are_missing()
        {
            var possibleValues = new[] { -1, 10 };
            var vm = VariableManagerFactory.Create<Variables>(new { Min = possibleValues, Max = possibleValues });
            var fails = new List<Variables>();
            var successes = new List<Variables>();

            foreach (var vb in vm)
            {
                var input = new[] { 1, 5, 6, 2, 9, 7, 3, 8, 4, 0 };
                var min = vb.Min;
                var max = vb.Max;

                foreach (var item in input)
                {
                    if (item < min)
                        min = item;

                    if (item > max)
                        max = item;
                }

                try
                {
                    AreEqual(0, min);
                    AreEqual(9, max);
                }
                catch
                {
                    fails.Add(vb);
                    continue;
                }

                successes.Add(vb);
            }

            Console.WriteLine("Failed for:");
            foreach (var vb in fails)
                Console.WriteLine(vb);
            Console.WriteLine();
            Console.WriteLine("Succeed for:");
            foreach (var vb in successes)
                Console.WriteLine(vb);
            Console.WriteLine();

            AreEqual(3, fails.Count);
            AreEqual(1, successes.Count);
        }

        [Test]
        public void Solves_min_max_algorithm_when_initial_values_and_if_conditions_are_missing()
        {
            var possibleValues = new[] { -1, 10 };
            var predicates = new Expression<Func<int, int, bool>>[] { (x, y) => x < y, (x, y) => x == y, (x, y) => x > y };
            var vm = VariableManagerFactory.Create<VariablesWithPredicate>(new { Min = possibleValues, Max = possibleValues, MinPredicate = predicates, MaxPredicate = predicates });
            var fails = new List<VariablesWithPredicate>();
            var successes = new List<VariablesWithPredicate>();

            foreach (var vb in vm)
            {
                var input = new[] { 1, 5, 6, 2, 9, 7, 3, 8, 4, 0 };
                var min = vb.Min;
                var max = vb.Max;
                var minPredicate = vb.MinPredicate.Compile();
                var maxPredicate = vb.MaxPredicate.Compile();

                foreach (var item in input)
                {
                    if (minPredicate.Invoke(item, min))
                        min = item;

                    if (maxPredicate.Invoke(item, max))
                        max = item;
                }

                try
                {
                    AreEqual(0, min);
                    AreEqual(9, max);
                }
                catch
                {
                    fails.Add(vb);
                    continue;
                }

                successes.Add(vb);
            }

            Console.WriteLine("Failed for:");
            foreach (var vb in fails)
                Console.WriteLine(vb);
            Console.WriteLine();
            Console.WriteLine("Succeed for:");
            foreach (var vb in successes)
                Console.WriteLine(vb);
            Console.WriteLine();

            AreEqual(35, fails.Count);
            AreEqual(1, successes.Count);
        }
    }
}
