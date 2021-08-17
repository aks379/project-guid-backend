using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Infrastructure.Guid
{
    /// <summary>
    /// IFileCreator contract.
    /// </summary>
    public interface IFileCreator
    {
        string CreateFile(string filePath, string batchName, int fileIndex, List<string> fileContent);
    }
}
