using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QuickTests
{
    class WebTestResult
    {
        public HttpStatusCode HttpStatus
        {
            get; set;
        }

        public string ScreenshotPath
        {
            get; set;
        }

        public string Message
        {
            get; set;
        }

        public string ErrorMessage
        {
            get; set;
        }

        public WebTestTask TestTask
        {
            get; set;
        }

        public string Typo3ErrorText
        {
            get; set;
        }
    }
}
