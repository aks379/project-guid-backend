using System;

namespace Layer.Infrastructure.Guid
{
    /// <summary>
    /// File creator factory that act as a factory for instatiating an instance of IFileCreator.
    /// </summary>
    public class FileCreatorFactory
    {
        private readonly IServiceProvider serviceProvider;

        public FileCreatorFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Returns an instance of IFileCreator based on the file extension.
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public IFileCreator GetFileCreator(string fileExtension)
        {
            IFileCreator? fileCreator = null;

            switch (fileExtension)
            {
                case "txt":
                    fileCreator = this.serviceProvider.GetService(typeof(TextFileCreator)) as IFileCreator;
                    break;
                case "csv":
                    fileCreator = this.serviceProvider.GetService(typeof(CsvFileCreator)) as IFileCreator;
                    break;
                default:
                    throw new NotSupportedException($"{fileExtension} files are not supported");
            }

            return fileCreator;
        }
    }
}
