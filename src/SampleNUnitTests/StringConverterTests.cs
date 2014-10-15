using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SampleAppConsole;

namespace SampleNUnitTests
{
    [TestFixture]
    public class StringConverterTests
    {

        Dictionary<string, string> _rules = new Dictionary<string, string>()
        {
            {"AB", "AA"},
            {"BA", "AA"},
            {"CB", "CC"},
            {"BC", "CC"},
            {"AA", "A"},
            {"CC", "C"}
        };

        [Test]
        public void test_string_converter_always_pick_the_first_matched_rule()
        {
            var converter = new StringConverter(_rules);
            converter.RuleSelector = (rules) =>
            {
                rules = rules.OrderBy(x => x.Key).ToList();
                foreach (var r in rules) { Console.Write("{0}:{1},", r.Key, r.Value); }
                Console.WriteLine();
                return rules[0];
            };
            var result = converter.Convert("CBABC");
            Assert.AreEqual(result, "CAC");
        }

        [Test]
        public void test_string_converter_always_pick_the_last_matched_rule()
        {
            var converter = new StringConverter(_rules);
            converter.RuleSelector = (rules) =>
                                     {
                                         rules = rules.OrderBy(x => x.Key).ToList();
                                         foreach(var r in rules) {Console.Write("  rules: {0}:{1},", r.Key, r.Value);}
                                         Console.Write("\n");
                                         return rules[rules.Count() - 1];
                                     };

            var result = converter.Convert("CBABC");
            Assert.AreEqual(result, "CAC");

            //result = converter.Convert("ABBCA");
            //Assert.AreEqual(result, "ACA");
        }

        [Test]
        public void test_string_converter_always_pick_a_random_matched_rule()
        {
            var converter = new StringConverter(_rules);
            converter.RuleSelector = (rules) =>
                                     {
                                         var random = new Random((int)DateTime.Now.Ticks);
                                         return rules[random.Next(rules.Count)];
                                     };

            var result = converter.Convert("CBABC");
            Assert.AreEqual(result, "CAC");
        }

    }

}