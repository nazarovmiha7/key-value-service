using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KeyValueService
{
    public static class StringExtension
    {
        public static string SentencesFormatting(this string sentencesRaw)
        {
            if (string.IsNullOrEmpty(sentencesRaw))
                return sentencesRaw;
            var sentences = sentencesRaw.Split('.').Select(s => s.Trim());
            var newSentenses = new List<string>();
            foreach (var s in sentences)
            {
                if (string.IsNullOrEmpty(s))
                    continue;
                newSentenses.Add(s.First().ToString().ToUpperInvariant() + s.Substring(1).ToLowerInvariant() + '.');
            }
            StringBuilder sb = new StringBuilder();
            return sb.AppendJoin(' ', newSentenses).ToString();
        }
    }
}
