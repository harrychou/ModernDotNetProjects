using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace SampleNUnitTests
{
    [TestFixture]
    public class _DummyTests
    {
        [Test]
        public void test_two_plus_two()
        {
             Assert.AreEqual(2 + 2, 4);
        }

    }
}
