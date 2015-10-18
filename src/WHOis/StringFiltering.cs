using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHOis
{
    public static class StringHelper
    {

        static Dictionary<char, char> replaceChars;

        static StringHelper()
        {
            replaceChars = new Dictionary<char, char>();
            replaceChars.Add('ə', 'a');
            replaceChars.Add('ā', 'a');
        }

        public static string AddFlowChars(this string item, int fitLength, char flowChar)
        {
            for (fitLength = fitLength - item.Length; fitLength > 0; fitLength--)
            {
                item += flowChar.ToString();
            }

            return item;
        }

        public static string[] GetNamesByPreCompile(this string text)
        {
            var names = text.Split(new[]
            {
                "\r\n",
                ",",
                ":",
                "'",
                "\"",
                ";",
                ".",
                "&",
                "~",
                "!",
                "@",
                "#",
                "$",
                "%",
                "^",
                "*",
                "(",
                ")",
                "-",
                "+",
                "_",
                "+",
                "=",
                @"\",
                "|",
                "{",
                "}",
                "`",
                "?",
                "/",
                "<",
                ">",
                "[",
                "]",
                " "
            }, StringSplitOptions.RemoveEmptyEntries);

            SortedList<string, string> preCompiledNames = new SortedList<string, string>();

            foreach (var name in names)
            {
                if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name)) continue;

                var lowerName = name.ToLower();
                foreach (char c in lowerName)
                {
                    if (replaceChars.ContainsKey(c))
                    {
                        lowerName = lowerName.Replace(c, replaceChars[c]);
                    }
                }

                if (!preCompiledNames.ContainsKey(lowerName))
                    preCompiledNames.Add(lowerName, lowerName);
            }

            var ary = new string[preCompiledNames.Count];
            preCompiledNames.Values.CopyTo(ary, 0);

            return ary;
        }
    }
}
