using Layer.Domain.Guid;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectGuid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuidController : ControllerBase
    {
        private readonly FileSplitterFactory _fileSplitterFactory;
        private readonly IConfiguration _configuration;

        public GuidController(IConfiguration configuration,
            FileSplitterFactory fileSplitterFactory)
        {
            this._configuration = configuration;
            this._fileSplitterFactory = fileSplitterFactory;
        }

        [Route("upload")]
        [HttpPost]
        public async Task<IActionResult> UploadGuidFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("No file selected");

            var path = Path.Combine(this._configuration.GetValue<string>("TempPath"), string.Concat(DateTime.Now.ToString("yyyyMMddHHmmss-"), file.FileName));

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            IFileSplitter fileSplitter = this._fileSplitterFactory.GetFileSplitter(file.FileName);
            fileSplitter.Split(path, 100);

            return Ok();
        }

        /// <summary>
        /// Creates a test file with guids.
        /// </summary>
        private void CreateTestGuidFile()
        {
            List<string> fileContent = new List<string>();

            for (int i = 0; i < 10000; i++)
            {
                string guid = Guid.NewGuid().ToString();
                fileContent.Add(guid.Replace("-", ","));
            }

            var path = Path.Combine(this._configuration.GetValue<string>("TempPath"), "TestFile.csv");
            System.IO.File.WriteAllLines(path, fileContent);
        }
    }
}
