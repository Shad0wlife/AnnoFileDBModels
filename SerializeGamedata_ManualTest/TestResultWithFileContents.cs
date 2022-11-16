using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializeGamedata_ManualTest
{
    public class TestResultWithFileContents
    {

        public TestResultWithFileContents(bool success, string originalContent, string createdContent, string originalContentBinary = "", string createdContentBinary = "")
        {
            Success = success;
            OriginalContent = originalContent;
            CreatedContent = createdContent;
            OriginalContentWithBinaryData = originalContentBinary;
            CreatedContentWithBinaryData = createdContentBinary;
        }

        public bool Success { get; }
        public string OriginalContent { get; }

        public string CreatedContent { get; }

        public string OriginalContentWithBinaryData { get; }

        public string CreatedContentWithBinaryData { get; }
    }
}
