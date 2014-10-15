using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleAppConsole
{
    public class StringConverter
    {
        private Dictionary<string, string> _rules = new Dictionary<string, string>();

        public StringConverter(Dictionary<string, string> rules)
        {
            _rules = rules;

            // default rule selector is random

            RuleSelector = (ruleList) =>
                           {
                               var random = new Random((int)DateTime.Now.Ticks);
                               return ruleList[random.Next(ruleList.Count)];
                           };
        }

        public Func<IList<KeyValuePair<string, int>>, KeyValuePair<string, int>> RuleSelector { get; set; }

        public IList<KeyValuePair<string, int>> FindMatches(string s)
        {
            var result = new List<KeyValuePair<string, int>> ();
            foreach (var key in _rules.Keys)
            {
                var index = s.IndexOf(key);
                if (index >= 0)
                {
                    result.Add(new KeyValuePair<string, int>(key, index));
                }
            }

            return result;
        }

        private string ApplyConversion(string s, KeyValuePair<string, int> ruleToApply)
        {
            return s.Substring(0, ruleToApply.Value) +
                   _rules[ruleToApply.Key] +
                   s.Substring(ruleToApply.Value + ruleToApply.Key.Length);
        }

        public string Convert(string s)
        {
            // handle null
            if (s == null) return null;
            // handle empty string
            if (s.IsNullOrEmpty()) return "";

            var tmpResult = s;
            var matches = FindMatches(tmpResult);

            Console.WriteLine(tmpResult);

            while (matches.Any())
            {
                var ruleToApply = RuleSelector(matches);
                Console.WriteLine("- apply rule {0}:{1}", ruleToApply.Key, ruleToApply.Value);
                tmpResult = ApplyConversion(tmpResult, ruleToApply);
                Console.WriteLine("  result:{0}", tmpResult);
                matches = FindMatches(tmpResult);
            }

            return tmpResult;
        }

    }
}