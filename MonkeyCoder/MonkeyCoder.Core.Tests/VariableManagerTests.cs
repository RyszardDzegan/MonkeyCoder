using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MonkeyCoder.Core.Tests
{
    [TestClass]
    public class VariableManagerTests
    {
        [TestMethod]
        public void Works_with_all_features()
        {
            var ip = new ValuePlaceholder();

            var vm1 = new VariableManager(
                new[] { new ValuePlaceholder(2), new ValuePlaceholder(5), new ValuePlaceholder(7) },
                "x",
                new VariablePlaceholder("y", (vb, y) => y > vb.x));

            var vm2 = new VariableManager(
                new ValuePlaceholder[] { new ValuePlaceholder(1), ip },
                "x",
                new VariablePlaceholder("y", (vb, y) => y > vb.x));

            Vm1Label:
            while (vm1.MoveNext())
            {
                var vb1 = vm1.VariableBag;
                Console.WriteLine("1 {" + vb1.x + " " + vb1.y + "}");

                if (!vm1.IsValid)
                {
                    Console.WriteLine("1 Invalid");
                    goto Vm1Label;
                }

                Vm2Label:
                while (vm2.MoveNext())
                {
                    var vb2 = vm2.VariableBag;
                    Console.WriteLine("2 {" + vb2.x + " " + vb2.y + "}");

                    for (int i = vb1.x; i < vb1.y; i++)
                    {
                        ip.Value = i;

                        if (!vm2.IsValid)
                        {
                            Console.WriteLine("2 Invalid");
                            goto Vm2Label;
                        }

                        for (int j = vb2.x; j < vb2.y; j++)
                        {
                            Console.WriteLine("i = {0}, j = {1}", i, j);
                        }
                    }

                    Console.WriteLine();
                }
                vm2.Reset();
            }
            vm1.Reset();
        }
    }
}
