using System;
using System.Collections.Generic;

namespace AngryWasp.Helpers
{
    public class PasswordPrompt
    {
        public static string Get(string message = null, bool hideOutput = false)
        {
			if (message == null)
            	Console.WriteLine("Please enter your password");
			else
				Console.WriteLine(message);

            var pwd = new List<char>();
			while (true)
			{
				ConsoleKeyInfo i = Console.ReadKey(true);
				if (i.Key == ConsoleKey.Enter)
					break;
				else if (i.Key == ConsoleKey.Backspace)
				{
					if (pwd.Count > 0)
					{
						pwd.RemoveAt(pwd.Count - 1);
						if (!hideOutput)
							Console.Write("\b \b");
					}
				}
				else if (i.KeyChar != '\u0000' ) // KeyChar == '\u0000' if the key pressed does not correspond to a printable character, e.g. F1, Pause-Break, etc
				{
					pwd.Add(i.KeyChar);
					if (!hideOutput)
						Console.Write("*");
				}
			}
			Console.WriteLine();
			return new string(pwd.ToArray());
        }
    }
}