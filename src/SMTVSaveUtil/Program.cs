using System;
using System.IO;
using CommandLine;
using CommandLine.Text;

namespace SMTVSaveUtil
{
    class Program
    {
        static void Main(string[] args)
        {
            ParseArgs(args);
        }

        static void ParseArgs(string[] args)
        {
            var parser = new Parser(with => with.HelpWriter = null);
            var parserResult = parser.ParseArguments<CryptOptions>(args);

            try
            {
                parserResult.MapResult(
                    (CryptOptions o) => Crypt(o),
                    errors => DisplayHelp(parserResult));
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        static int DisplayHelp<T>(ParserResult<T> result)
        {
            var helpText = HelpText.AutoBuild(
                result, help =>
                {
                    help.AdditionalNewLineAfterOption = false;
                    help.Heading = "smtv.saveutil - Shin Megami Tensei V Save Utility";
                    help.AutoVersion = false;
                    help.AddDashesToOption = true;
                    help.AddEnumValuesToHelpText = true;
                    help.MaximumDisplayWidth = 120;
                    return help;
                },
                e => e, verbsIndex: true);

            Console.WriteLine(helpText);
            return -1;
        }

        static int Crypt(CryptOptions o)
        {
            if (!File.Exists(o.PathIn))
            {
                Console.WriteLine("Input path doesn't exist.");
                return 1;
            }

            SaveCrypt.CryptFile(o.PathIn, o.PathOut);
            return 0;
        }
    }
}