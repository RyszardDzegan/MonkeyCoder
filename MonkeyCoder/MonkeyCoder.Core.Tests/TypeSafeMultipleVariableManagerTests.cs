using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace MonkeyCoder.Core.Tests
{
    [TestClass]
    public class TypeSafeMultipleVariableManagerTests
    {
        class Foo0 { }

        class Foo1
        {
            public string X { get; set; }
        }

        class Foo2
        {
            public string X { get; set; }
            public int Y { get; set; }
        }

        class FooValues
        {
            public string[] X { get; } = new[] { "a", "b", "c" };
            public int[] Y { get; } = new[] { 1, 2 };
        }

        //[TestMethod]
        //public void Works_for_zero_variables()
        //{
        //    var variableManager = new TypeSafeMultipleVariableManager<Foo0>(new { });
        //    var enumerator = variableManager.GetEnumerator();
        //    IsTrue(enumerator.MoveNext());
        //    IsFalse(enumerator.MoveNext());
        //}

        [TestMethod]
        public void Works_for_one_variable_and_3_values()
        {
            var variableManager = new TypeSafeMultipleVariableManager<Foo1>(new { X = new[] { "a", "b", "c" } });
            var enumerator = variableManager.GetEnumerator();
            IsTrue(enumerator.MoveNext());
            AreEqual("a", enumerator.Current.X);
            IsTrue(enumerator.MoveNext());
            AreEqual("b", enumerator.Current.X);
            IsTrue(enumerator.MoveNext());
            AreEqual("c", enumerator.Current.X);
            IsFalse(enumerator.MoveNext());
        }

        [TestMethod]
        public void Works_for_two_variables_and_3_2_values()
        {
            var variableManager = new TypeSafeMultipleVariableManager<Foo2>(new FooValues());
            var enumerator = variableManager.GetEnumerator();
            IsTrue(enumerator.MoveNext());
            AreEqual("a", enumerator.Current.X);
            AreEqual(1, enumerator.Current.Y);
            IsTrue(enumerator.MoveNext());
            AreEqual("a", enumerator.Current.X);
            AreEqual(2, enumerator.Current.Y);
            IsTrue(enumerator.MoveNext());
            AreEqual("b", enumerator.Current.X);
            AreEqual(1, enumerator.Current.Y);
            IsTrue(enumerator.MoveNext());
            AreEqual("b", enumerator.Current.X);
            AreEqual(2, enumerator.Current.Y);
            IsTrue(enumerator.MoveNext());
            AreEqual("c", enumerator.Current.X);
            AreEqual(1, enumerator.Current.Y);
            IsTrue(enumerator.MoveNext());
            AreEqual("c", enumerator.Current.X);
            AreEqual(2, enumerator.Current.Y);
            IsFalse(enumerator.MoveNext());
        }

        [TestMethod]
        public void Works_for_two_variables_and_3_2_values_provided_as_anonymous_object()
        {
            var variableManager = new TypeSafeMultipleVariableManager<Foo2>(new { X = new[] { "a", "b", "c" }, Y = new[] { 1, 2 } });
            var enumerator = variableManager.GetEnumerator();
            IsTrue(enumerator.MoveNext());
            AreEqual("a", enumerator.Current.X);
            AreEqual(1, enumerator.Current.Y);
            IsTrue(enumerator.MoveNext());
            AreEqual("a", enumerator.Current.X);
            AreEqual(2, enumerator.Current.Y);
            IsTrue(enumerator.MoveNext());
            AreEqual("b", enumerator.Current.X);
            AreEqual(1, enumerator.Current.Y);
            IsTrue(enumerator.MoveNext());
            AreEqual("b", enumerator.Current.X);
            AreEqual(2, enumerator.Current.Y);
            IsTrue(enumerator.MoveNext());
            AreEqual("c", enumerator.Current.X);
            AreEqual(1, enumerator.Current.Y);
            IsTrue(enumerator.MoveNext());
            AreEqual("c", enumerator.Current.X);
            AreEqual(2, enumerator.Current.Y);
            IsFalse(enumerator.MoveNext());
        }
    }
}
