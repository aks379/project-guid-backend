using Layer.Infrastructure.Guid;
using System;
using System.Linq;
using System.Threading;

namespace Layer.Domain.Guid
{
    /// <summary>
    /// Class that implements the logic of splitting a txt file.
    /// </summary>
    public class TextFileSplitter : IFileSplitter
    {
        private readonly FileCreatorFactory _fileCreatorFactory;

        public TextFileSplitter(FileCreatorFactory fileCreatorFactory)
        {
            this._fileCreatorFactory = fileCreatorFactory;
        }

        /// <summary>
        /// Splits a txt file provided into no of files based on the splitsize provided.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="splitSize"></param>
        public void Split(string path, int splitSize)
        {
            var fileContent = System.IO.File.ReadAllLines(path).ToList();
            int fileCount = 0;
            IFileCreator fileCreator = this._fileCreatorFactory.GetFileCreator("txt");
            string batchName = System.IO.Path.GetFileNameWithoutExtension(path);

            while (fileContent.Any())
            {
                Thread.Sleep(1000);

                fileCount++;
                var splitContent = fileContent.Take(splitSize).ToList();
                fileContent.RemoveRange(0, splitSize);

                fileCreator.CreateFile(System.IO.Path.GetDirectoryName(path), batchName, fileCount, splitContent);
            }
        }
    }
}
