using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTests
{
    class Program
    {
        static void Main(string[] args)
        {

            if(args.Count() > 0)
            {

                //Liste mit Webseiten einlesen

                //e.g. "C:\\Temp\\QTScreenshots\\QuickTestTasks.xlsx";
                string excelPath = args[0];
                string resultPath = excelPath.Substring(0,excelPath.LastIndexOf("\\")+1);

                var worker = new ExcelWorker(excelPath);
                var taskList = worker.GetTestTasksFromExcel();
                PrintTasksToConsole(taskList);

                //Jede Seite aufrufen
                WebsiteTest test = new WebsiteTest(taskList, resultPath);

                //Ergebnis sammeln
                List<WebTestResult> resultList = test.TestWebsiteAvailablility();
                PrintResultsToConsole(resultList);

                //Ergebnisliste + Screenshots als Website ablegen

                var html = new WebTestResultPage(resultPath, resultList);
                html.CreateResultHTMLPage();
                //Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Please provide link to Excel Table as parameter");
            }
        }

        private static void PrintResultsToConsole(List<WebTestResult> resultList)
        {
            foreach (WebTestResult result in resultList)
            {
                Console.WriteLine("Status: " + result.HttpStatus + " ImgPath: " + result.ScreenshotPath + " Message: " + result.Message + " Error: " + result.ErrorMessage);
            }
        }

        private static void PrintTasksToConsole(List<WebTestTask> taskList)
        {
            foreach (WebTestTask task in taskList)
            {
                Console.WriteLine("TestScope: " + task.Task + "Customer: " + task.Customer + " URL: " + task.Url + " ExpectedStatus: " + task.ExpectedHttpStatus);
            }
        }

    }
}
