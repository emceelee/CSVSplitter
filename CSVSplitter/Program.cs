using System;
using System.IO;

namespace CSVSplitter
{
    class Program
    {
        static string extension = ".csv";
        static void Main(string[] args)
        {
            string fileName = @"C:\Devl\Git\CSVSplitter\Example\ProductQuality.csv";
            int maxLines = 1116;

            using (var reader = new StreamReader(fileName))
            {
                int count = 0;

                int index = fileName.IndexOf(extension);

                if(index > 0)
                {
                    string fileNameWithoutExtension = fileName.Substring(0, index);

                    string line;
                    bool initialized = false;
                    bool newFile = true;
                    string firstLine = "";

                    int lineCount = 0;

                    string splitFileName = fileNameWithoutExtension + "_" + 1.ToString("000") + extension;
                    StreamWriter writer = new StreamWriter(splitFileName);

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!initialized)
                        {
                            initialized = true;
                            firstLine = line;

                            continue;
                        }
                        if (newFile)
                        {
                            writer.Dispose();
                            splitFileName = fileNameWithoutExtension + "_" + (++count).ToString("000") + extension;
                            writer = new StreamWriter(splitFileName);
                            writer.WriteLine(firstLine);
                            lineCount = 0;
                            newFile = false;
                        }

                        writer.WriteLine(line);

                        if (++lineCount >= maxLines)
                        {
                            newFile = true;
                        }
                    }

                    writer.Dispose();
                }
                
            }
        }
    }
}
