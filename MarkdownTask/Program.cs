using System;
using System.IO;
using System.Text;

namespace MarkdownTask
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: file_for_markdown file_for_result");
                return;
            }

            var inputPath = args[0];
            var outputPath = args[1];
            var text = File.ReadAllText(inputPath, Encoding.UTF8);
            var markedText = MarkdownProcessor.Markdown(text);
            File.WriteAllText(outputPath, markedText, Encoding.UTF8);
        }
    }
}
