using System;
using System.IO;

namespace MarkdownTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = args[0];

            using (var inputFile = File.OpenText(path))
            {
                var text = inputFile.ReadToEnd();
                var markedText = MarkdownProcessor.Markdown(text);
                Console.WriteLine(markedText);
            }
        }
    }
}
