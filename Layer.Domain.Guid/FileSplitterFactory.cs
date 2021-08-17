using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Domain.Guid
{
    /// <summary>
    /// File splitter factory that act as a factory for instatiating an instance of IFileSplitter.
    /// </summary>
    public class FileSplitterFactory
    {
        private readonly IServiceProvider serviceProvider;

        public FileSplitterFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Returns an instance of IFileSplitter based on the file extension.
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public IFileSplitter GetFileSplitter(string filepath)
        {
            IFileSplitter? fileSplitter = null;

            string fileExtension = filepath.Substring(filepath.Length - 3, 3);

            switch (fileExtension)
            {
                case "txt":
                    fileSplitter = this.serviceProvider.GetService(typeof(TextFileSplitter)) as IFileSplitter;
                    break;
                case "csv":
                    fileSplitter = this.serviceProvider.GetService(typeof(CsvFileSplitter)) as IFileSplitter;
                    break;
                default:
                    throw new NotSupportedException($"{fileExtension} files are not supported");
            }

            return fileSplitter;
        }
    }
}
