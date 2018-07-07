using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace AngryWasp.Helpers
{
    public static class StringHelper
    {
        /// <summary>
        /// Splits parameters from a string in the format {a:0 b:1}
        /// </summary>
        /// <returns><c>true</c>, if get type parameters was tryed, <c>false</c> otherwise.</returns>
        /// <param name="s">S.</param>
        /// <param name="namedParameters">Named parameters.</param>
        public static bool TryGetTypeParams(string s, out Dictionary<string, string> namedParameters)
        {
            s = s.TrimStart(new char[] { '{' }).TrimEnd(new char[] { '}' });

            string[] parameters = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            namedParameters = new Dictionary<string, string>();
            foreach (string p in parameters)
            {
                try
                {
                    string[] np = p.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    namedParameters.Add(np[0], np[1]);
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Gets a set of comma delimited values inside a set of parenthesis. i.e. (1, 2, 3)
        /// </summary>
        /// <returns>The values inside parentheses.</returns>
        /// <param name="s">S.</param>
        public static List<string> GetValuesInsideParentheses(string s)
        {
            int startIndex = s.IndexOf('(') + 1;
            int endIndex = s.LastIndexOf(')');
            s = s.Substring(startIndex, endIndex - startIndex);
            string[] parameters = s.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> result = new List<string>();
            foreach (string p in parameters)
            {
                string r = p.Trim();
                result.Add(r);
            }

            return result;
        }

        /// <summary>
        /// Replaces all chars in toReplace with the corresponding element in replaceWith. The length of both arrays must be equal
        /// </summary>
        /// <param name="toReplace">array of chars to replace</param>
        /// <param name="replaceWith">array of chars to replace them with</param>
        /// <returns>string.Empty in an error, otherwise the processed string</returns>
        public static string ReplaceAll(string value, char[] toReplace, char[] replaceWith)
        {
            if (toReplace.Length != replaceWith.Length)
                return string.Empty;

            for (int i = 0; i < toReplace.Length; i++)
                value = value.Replace(toReplace[i], replaceWith[i]);

            return value;
        }

        /// <summary>
        /// Replaces all strings in toReplace with the corresponding element in replaceWith. The length of both arrays must be equal
        /// </summary>
        /// <param name="toReplace">array of strings to replace</param>
        /// <param name="replaceWith">array of strings to replace them with</param>
        /// <returns>string.Empty in an error, otherwise the processed string</returns>
        public static string ReplaceAll(string value, string[] toReplace, string[] replaceWith)
        {
            if (toReplace.Length != replaceWith.Length)
                return string.Empty;

            for (int i = 0; i < toReplace.Length; i++)
                value = value.Replace(toReplace[i], replaceWith[i]);

            return value;
        }

        /// <summary>
        /// Checks if the supplied string is a valid email address
        /// </summary>
        /// <param name="s">The string to check</param>
        /// <param name="onlineCheck">if true, performs secondary checking for the existence of the address</param>
        /// <returns>true if valid, else false</returns>
        public static bool IsValidEmail(string s, bool onlineCheck = false)
        {
            if (string.IsNullOrEmpty(s))
                return false;

            bool regexMatch = false;

            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(s))
                regexMatch = true;

            if (!regexMatch)
                return false;

            //untested
            if (onlineCheck)
            {
                string[] host = (s.Split('@'));
                string hostname = host[1];

                try
                {
                    IPHostEntry IPhst = Dns.GetHostEntry(hostname);
                    IPEndPoint endPt = new IPEndPoint(IPhst.AddressList[0], 25);
                    Socket sock = new Socket(endPt.AddressFamily,
                            SocketType.Stream, ProtocolType.Tcp);
                    sock.Connect(endPt);
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Parses a DateTime object to a file friendly string by removing illegal characters and replacing with substitutes.
        /// substitutions are:
        /// / = -
        /// : = .
        /// </summary>
        /// <param name="dt">The DateTime object to parse</param>
        /// <returns>A string which has file name illegal characters removed</returns>
        public static string ParseDateTimeToFileFriendly(DateTime dt, bool includeMilliseconds = false)
        {
            return ReplaceAll(dt.ToString(), new char[] { '/', ':' }, new char[] { '-', '.' }) + (includeMilliseconds ? dt.Millisecond.ToString() : string.Empty);
        }

        /// <summary>
        /// Converts a Unix timestamp to a DateTime structure
        /// </summary>
        /// <param name="ts">The timestamp to convert</param>
        /// <returns>A DateTime representing the timestamp</returns>
        public static DateTime UnixTimeStampToDateTime(ulong ts)
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return dt.AddSeconds(ts).ToLocalTime();
        }

        /// <summary>
        /// Converts all the tab characters \t in a string to spaces
        /// </summary>
        /// <param name="s">the string to convert</param>
        /// <param name="spaces">the number of spaces to use for each tab</param>
        /// <returns>the formatted string</returns>
        public static string TabsToSpaces(string s, int spaces)
        {
            string space = string.Empty;
            for (int i = 0; i < spaces; i++)
                space += " ";

            return ReplaceAll(s, new string[] { "\t" }, new string[] { space });
        }

        /// <summary>
        /// Removes all spaces from a string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string RemoveWhitespace(string s)
        {
            s = TabsToSpaces(s, 1);
            s = s.Replace(" ", "");
            return s;
        }

        /// <summary>
        /// checks a path for illegal characters
        /// </summary>
        /// <param name="s">The path to check</param>
        /// <returns>True if illegal characters found, otherwise false</returns>
        public static bool PathContainsIllegalCharacters(string s)
        {
            if (s.Contains('\\') || s.Contains('/') || s.Contains(':') ||
                s.Contains('*') || s.Contains('?') || s.Contains('"') ||
                s.Contains('<') || s.Contains('>') || s.Contains('|'))
                return true;

            return false;
        }

        public static string GenerateRandomString(int length)
        {
            char[] chars = ("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ").ToCharArray();

            string str = string.Empty;

            for (int i = 0; i < length; i++)
                str += chars[MathHelper.Random.NextInt(0, chars.Length)];

            return str;
        }

        public static string GenerateRandomHexString(int length, bool upperCase = false)
        {
            char[] chars = $"0123456789{(upperCase ? "ABCDEF" : "abcdef")}".ToCharArray();

            string str = string.Empty;

            for (int i = 0; i < length; i++)
                str += chars[MathHelper.Random.NextInt(0, chars.Length)];

            return str;
        }

        public static string EncodeBase64(string t)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(t));
        }

        public static string DecodeBase64(string t)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(t));
        }
        
        public static string NormalizeFilePath(string s)
        {
        	return s.Replace("\\", "/");
        }
    }
}