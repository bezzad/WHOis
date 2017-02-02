using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WHOis
{
    public static class StringHelper
    {
        const int PrintColumnWidth = 35;
        private const string Seperator = ",";
        static readonly Dictionary<char, char> ReplaceChars;
        private static readonly string[] FilteringChars;
        private const string EnglishLetterals = "abcdefghijklmnopqrstuvwxyz-";

        static StringHelper()
        {
            ReplaceChars = new Dictionary<char, char> { { 'ə', 'a' }, { 'ā', 'a' } };
            FilteringChars = new[]
            {
                "\r\n"
                //, ",", ":", "'", "\"", ";", ".", "&", "~",
                //"!", "@", "#", "$", "%", "^", "*", "(", ")",
                //"+", "_", "+", "=", @"\", "|", "{", "}", "`", "?",
                //"/", "<", ">", "[", "]", " ", "\t", "\n", "\r"
            };
        }

        public static string[] GetNamesByPreCompile(this string text, bool isDigit)
        {
            text += Environment.NewLine;

            var names = text.ClearNonAlphabetChars(isDigit); //text.Split(FilteringChars, StringSplitOptions.RemoveEmptyEntries);

            var preCompiledNames = new SortedList<string, string>();

            foreach (var name in names)
            {
                if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name)) continue;

                var lowerName = name.ToLower();
                foreach (var c in lowerName)
                {
                    if (ReplaceChars.ContainsKey(c))
                    {
                        lowerName = lowerName.Replace(c, ReplaceChars[c]);
                    }
                }

                if (!preCompiledNames.ContainsKey(lowerName))
                    preCompiledNames.Add(lowerName, lowerName);
            }

            var ary = new string[preCompiledNames.Count];
            preCompiledNames.Values.CopyTo(ary, 0);

            return ary;
        }

        private static IList<string> ClearNonAlphabetChars(this string text, bool isDigit)
        {
            var res = new List<string>();
            var lastWord = "";
            for (var i = 0; i < text.Length; i++)
            {
                if ((isDigit && char.IsDigit(text[i])) || text[i].IsEnglishLetter())
                    lastWord += text[i].ToString();
                else
                {
                    if (!string.IsNullOrEmpty(lastWord))
                    {
                        res.Add(lastWord);
                        lastWord = "";
                    }
                }
            }

            return res;
        }

        private static bool IsEnglishLetter(this char c)
        {
            return EnglishLetterals.Any(letteral => letteral.Equals(c));
        }

        public static string[] PrintGridViewToText(this DataGridView dgv)
        {
            var rows = new List<string>();
            var buffer = "";

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                buffer += $"|  {col.HeaderText,-PrintColumnWidth} ";
            }
            rows.Add(buffer); // add header

            // create header line acourding header char lenght like: =============
            var line = "|".PadRight((dgv.Columns.Count - 1) * PrintColumnWidth, '=');
            rows.Add(line); // add header line

            line = line.Replace("=", "--"); // other lines like: ---------------------

            foreach (DataGridViewRow row in dgv.Rows)
            {
                buffer = "";

                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    var val = (row.Cells[col.Name].Value ?? CheckState.Unchecked).ToString();

                    if (val == CheckState.Indeterminate.ToString())
                        val = "Reserved";
                    else if (val == CheckState.Checked.ToString())
                        val = "Free";
                    else if (val == CheckState.Unchecked.ToString())
                        val = "Error";

                    buffer += $"|  {val,-PrintColumnWidth}";
                }

                rows.Add(buffer);
                rows.Add(line);
            }

            return rows.ToArray();
        }

        public static string[] PrintGridViewToCsv(this DataGridView dgv)
        {
            var rows = new List<string>();
            var buffer = dgv.Columns.Cast<DataGridViewColumn>().Aggregate("", (current, col) => current + $"{col.HeaderText}{Seperator}");

            rows.Add(buffer); // add header

            foreach (DataGridViewRow row in dgv.Rows)
            {
                buffer = "";

                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    var val = (row.Cells[col.Name].Value ?? CheckState.Unchecked).ToString();

                    if (val == CheckState.Indeterminate.ToString())
                        val = "Reserved";
                    else if (val == CheckState.Checked.ToString())
                        val = "Free";
                    else if (val == CheckState.Unchecked.ToString())
                        val = "Error";

                    buffer += $"{val}{Seperator}";
                }

                rows.Add(buffer);
            }

            return rows.ToArray();
        }
    }
}
