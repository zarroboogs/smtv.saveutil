using CommandLine;

namespace SMTVSaveUtil
{
    [Verb("crypt", HelpText = "Encrypt/decrypt SMTV save data.")]
    class CryptOptions
    {
        [Option('i', "in", Required = true, HelpText = "Path to SMTV save file.")]
        public string PathIn { get; set; }

        [Option('o', "out", Required = false, HelpText = "Output file path.")]
        public string PathOut { get; set; }
    }
}