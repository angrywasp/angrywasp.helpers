using System.Collections.Generic;

namespace AngryWasp.Helpers
{
    public class CommandLineParserOption
    {
        private string flag;
        private string value;

        public string Flag
        {
            get { return flag; }
        }

        public string Value
        {
            get { return value; }
        }

        public CommandLineParserOption(string flag, string value)
        {
            this.flag = flag;
            this.value = value;
        }
    }

    public class CommandLineParser
    {
        private List<CommandLineParserOption> options = new List<CommandLineParserOption>();

        public CommandLineParserOption this[int index]
        {
            get
            {
                if (index < 0 || index >= options.Count)
                    return null;

                return options[index];
            }
        }

        public CommandLineParserOption this[string flag]
        {
            get
            {
                string f1 = string.IsNullOrEmpty(flag) ? null : flag.ToLower().Trim(new char[] { '-' });

                for (int i = 0; i < options.Count; i++)
                {
                    string f2 = string.IsNullOrEmpty(options[i].Flag) ? null : options[i].Flag.ToLower().Trim(new char[] { '-' });
                    if (f2 == f1)
                        return options[i];
                }

                return null;
            }
        }

        public static CommandLineParser Parse(string[] args)
        {
            CommandLineParser cmd = new CommandLineParser();

            if (args == null)
                return cmd;

            for (int i = 0; i < args.Length; i++)
            {
                bool isFlag = args[i].StartsWith("-");
                bool hasParam = (i + 1) < args.Length ? !args[i + 1].StartsWith("-") : false;

                string parameter = hasParam ? args[i + 1] : null;
                string arg = args[i].TrimStart(new char[] { '-' });

                if (isFlag)
                {
                    char[] argChars = arg.ToCharArray();

                    if (args[i].StartsWith("--"))
                        cmd.Push(arg, parameter);
                    else
                    {
                        if (arg.Length == 1)
                            cmd.Push(arg, parameter);
                        else
                            for (int c = 0; c < argChars.Length; c++)
                                cmd.Push(argChars[c].ToString(), null);
                    }

                    if (argChars.Length == 1 && hasParam)
                        i++;
                }
                else
                    cmd.Push(null, arg);
            }

            return cmd;
        }

        public static CommandLineParser New(params CommandLineParserOption[] arguments)
        {
            return new CommandLineParser();
        }

        public CommandLineParserOption Pop()
        {
            CommandLineParserOption o = options[0];
            options.RemoveAt(0);
            return o;
        }

        public void Push(CommandLineParserOption o)
        {
            options.Add(o);
        }

        public void Push(string flag, string value)
        {
            options.Add(new CommandLineParserOption(flag, value));
        }

        public int Count
        {
            get { return options.Count; }
        }
    }
}