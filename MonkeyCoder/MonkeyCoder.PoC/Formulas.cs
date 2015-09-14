using MonkeyCoder.Functions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static NUnit.Framework.Assert;

namespace MonkeyCoder.PoC
{
    [TestFixture]
    public class Formulas
    {
        [DebuggerDisplay("{FormulaName} : {FormulaValues}")]
        class Box<T>
        {
            public T Value { get; set; }
            public string FormulaName { get; } = "{null}";

            private readonly string _staticFormulaValues;
            public string FormulaValues
            {
                get { return _staticFormulaValues ?? (Value != null ? Value.ToString() : "{null}"); }
            }

            public Box(string formulaName)
            {
                FormulaName = formulaName;
            }

            public Box(T value, string formulaName, string formulaValues)
            {
                Value = value;
                FormulaName = formulaName;
                _staticFormulaValues = formulaValues;
            }
        }

        [DebuggerDisplay("{FormulaName} : {FormulaValues}")]
        class BinaryBox<T1, T2, TResult> : Box<TResult>
        {
            public BinaryBox(TResult value, Box<T1> a, Box<T2> b, string mark)
                : base(value, $"({a.FormulaName} {mark} {b.FormulaName})", $"({a.FormulaValues} {mark} {b.FormulaValues})")
            { }
        }

        [Test]
        public void Solves_formula_1()
        {
            // Arrange
            // a + b = c
            var testCases = new Tuple<int, int, int>[]
            {
                Tuple.Create(1, 2, 3),
                Tuple.Create(5, 2, 7),
                Tuple.Create(3, 1, 4)
            };

            var arithmetic = new Func<Box<int>, Box<int>, Box<int>>[]
            {
                (x, y) => new BinaryBox<int, int, int>(x.Value + y.Value, x, y, "+")
            };

            var logical = new Func<Box<int>, Box<int>, Box<bool>>[]
            {
                (x, y) => new BinaryBox<int, int, bool>(x.Value == y.Value, x, y, "==")
            };

            var a = new Box<int>("a");
            var b = new Box<int>("b");
            var c = new Box<int>("c");
            
            var functionsTree = arithmetic
                .Cast<object>()
                .Concat(logical)
                .Concat(new[] { a, b, c })
                .AsFunctionsTree(1);

            var matches = new List<Func<object>>();

            // Act
            foreach (var function in functionsTree)
            {
                foreach (var testCase in testCases)
                {
                    a.Value = testCase.Item1;
                    b.Value = testCase.Item2;
                    c.Value = testCase.Item3;

                    var result = function();

                    if (!(result is Box<bool>))
                        goto Break1;

                    if (!((Box<bool>)result).Value)
                        goto Break1;
                }

                matches.Add(function);
                Break1:;
            }

            // Assert
            AreEqual(22, matches.Count());
            foreach (var match in matches)
            {
                foreach (var testCase in testCases)
                {
                    a.Value = testCase.Item1;
                    b.Value = testCase.Item2;
                    c.Value = testCase.Item3;

                    var result = match();

                    IsInstanceOf<Box<bool>>(result);
                    IsTrue(((Box<bool>)result).Value);
                }
            }

            foreach (var match in matches)
            {
                a.Value = 4;
                b.Value = 6;
                c.Value = 10;
                IsTrue(((Box<bool>)match()).Value);

                a.Value = 3;
                b.Value = 3;
                c.Value = 6;
                IsTrue(((Box<bool>)match()).Value);

                a.Value = 2;
                b.Value = 9;
                c.Value = 11;
                IsTrue(((Box<bool>)match()).Value);
            }
        }

        [Test]
        public void Solves_formula_2()
        {
            // Arrange
            // a + b = c
            var testCases = new Tuple<int, int, int>[]
            {
                Tuple.Create(1, 2, 3),
                Tuple.Create(5, 2, 7),
                Tuple.Create(3, 1, 4)
            };

            var arithmetic = new Func<Box<int>, Box<int>, Box<int>>[]
            {
                (x, y) => new BinaryBox<int, int, int>(x.Value + y.Value, x, y, "+")
            };

            var logical = new Func<Box<int>, Box<int>, Box<bool>>[]
            {
                (x, y) => new BinaryBox<int, int, bool>(x != y && x.FormulaName != y.FormulaName && x.Value == y.Value, x, y, "==")
            };

            var a = new Box<int>("a");
            var b = new Box<int>("b");
            var c = new Box<int>("c");

            var functionsTree = arithmetic
                .Cast<object>()
                .Concat(logical)
                .Concat(new[] { a, b, c })
                .AsFunctionsTree(1);

            var matches = new List<Func<object>>();

            // Act
            foreach (var function in functionsTree)
            {
                foreach (var testCase in testCases)
                {
                    a.Value = testCase.Item1;
                    b.Value = testCase.Item2;
                    c.Value = testCase.Item3;

                    var result = function();

                    if (!(result is Box<bool>))
                        goto Break1;

                    if (!((Box<bool>)result).Value)
                        goto Break1;
                }

                matches.Add(function);
                Break1:;
            }

            // Assert
            AreEqual(10, matches.Count());
            foreach (var match in matches)
            {
                foreach (var testCase in testCases)
                {
                    a.Value = testCase.Item1;
                    b.Value = testCase.Item2;
                    c.Value = testCase.Item3;

                    var result = match();

                    IsInstanceOf<Box<bool>>(result);
                    IsTrue(((Box<bool>)result).Value);
                }
            }

            foreach (var match in matches)
            {
                a.Value = 4;
                b.Value = 6;
                c.Value = 10;
                IsTrue(((Box<bool>)match()).Value);

                a.Value = 3;
                b.Value = 3;
                c.Value = 6;
                IsTrue(((Box<bool>)match()).Value);

                a.Value = 2;
                b.Value = 9;
                c.Value = 11;
                IsTrue(((Box<bool>)match()).Value);
            }
        }
    }
}
