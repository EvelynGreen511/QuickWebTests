using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTests
{
    class WebTestResultPage
    {
        private String resultPath;
        private List<WebTestResult> results;

        public WebTestResultPage(string resultPath, List<WebTestResult> results)
        {
            this.resultPath = resultPath;
            this.results = results;
        }

        public String CreateResultHTMLPage()
        {
            string filePath = resultPath + DateTimeOffset.UtcNow.ToUnixTimeSeconds() + ".html";

            if(File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            using (StreamWriter fs = new StreamWriter(filePath))
            {
                fs.WriteLine(CreateHTMLContent(results));
            }
            return filePath;
        }

        private String CreateHTMLContent(List<WebTestResult> resultList)
        {
            var content = string.Empty;

            var sb = new StringBuilder();
            sb.AppendLine("<html>");
                sb.AppendLine("<head></head>");
                sb.AppendLine("<body>");

                    foreach(WebTestResult result in resultList)
                    {
                        sb.AppendLine("<div class=\"webtest result\">");
                        sb.AppendLine("<h2>" + result.HttpStatus + "</h2>");
                        sb.AppendLine("<a href=\""+ result.TestTask.Url +"\">Go to URL</a>");
                        sb.AppendLine("<a href=\""+ result.ScreenshotPath+"\"><img src=\"" + result.ScreenshotPath + "\" style=\"max-width:250px; max-height:250px\"/></a>");
                        sb.AppendLine("</div>");
                    }

                sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return content = sb.ToString();
        }

    }
}
