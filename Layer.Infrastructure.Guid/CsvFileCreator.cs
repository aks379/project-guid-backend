using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Infrastructure.Guid
{
    /// <summary>
    /// Class that implements the logic of creating a csv file.
    /// </summary>
    public class CsvFileCreator : IFileCreator
    {
        /// <summary>
        /// Creates a csv file with the name and content provided.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileContent"></param>
        /// <returns></returns>
        public string CreateFile(string filePath, string batchName, int fileIndex, List<string> fileContent)
        {
            string tempPath = filePath;
            string subFolder = System.IO.Path.Combine(tempPath, batchName);
            if (!System.IO.Directory.Exists(subFolder))
            {
                System.IO.Directory.CreateDirectory(subFolder);
            }
            string fileName = System.IO.Path.Combine(subFolder, string.Concat(fileIndex.ToString(), ".csv"));

            System.IO.File.WriteAllLines(fileName, fileContent);

            return fileName;
        }
    }
}
