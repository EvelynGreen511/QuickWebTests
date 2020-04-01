using System;
using System.Collections.Generic;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Linq;

namespace QuickTests
{
    class ExcelWorker
    {
        string excelPath;


        public ExcelWorker(string excelPath)
        {
            this.excelPath = excelPath;
        }


        public List<WebTestTask> GetTestTasksFromExcel()
        {
            List<WebTestTask> taskList = new List<WebTestTask>();

            var workbook = new XLWorkbook(excelPath);

            var sheet = workbook.Worksheet("Testliste");
            foreach (var table in sheet.Tables)
            {
                if(table.Name.Equals("Testliste"))
                {
                    //TODO Enum.TryParse
                    taskList = table.DataRange.Rows()
                        .Select(testRow => new WebTestTask(WebTestTask.TestType.AVAILABILTIY, testRow.Field("Customer").GetString(), testRow.Field("URL").GetString(), testRow.Field("ExpectedHttpStatus").GetString()))
                        .ToList();
                }
            }

            return taskList;
        }
    }
}
