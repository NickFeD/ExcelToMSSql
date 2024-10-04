using CommandLine;

namespace ConsoleExcelToMSSql;

internal class Options
{
    [Option('r', "read", Required = true, HelpText = "Input files to be processed.")]
    public FileInfo InputFiles { get; set; }
}
