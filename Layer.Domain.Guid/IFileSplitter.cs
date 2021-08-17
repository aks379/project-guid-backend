using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Domain.Guid
{
    /// <summary>
    /// IFileSplitter contract.
    /// </summary>
    public interface IFileSplitter
    {
        void Split(string path, int splitSize);
    }
}
