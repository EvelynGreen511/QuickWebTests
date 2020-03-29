using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Net;

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

            Application xlApp = new Application();
            Workbook xlWorkbook = xlApp.Workbooks.Open(excelPath);
            foreach(Worksheet sheet in xlWorkbook.Sheets )
            {
                if(sheet.Name.Equals("Testliste"))
                {
                    foreach(ListObject table in sheet.ListObjects)
                    {
                        if(table.Name.Equals("Testliste"))
                        {
                            foreach (ListRow row in table.ListRows)
                            {
                                WebTestTask task = new WebTestTask();

                                foreach (ListColumn col in table.ListColumns)
                                {                                  
                                    var element = table.Range[row.Index, col.Index];

                                    if (element.Text.Equals("TestScope") || element.Text.Equals("Customer") || element.Text.Equals("URL") || element.Text.Equals("ExpectedHttpStatus"))
                                    {
                                        
                                    }
                                    else
                                    {
                                    if (col.Name.Equals("TestScope")) {
                                            if(element.Text.Equals("AVAILABILITY"))
                                            { 
                                                    task.Task = WebTestTask.TestType.AVAILABILTIY;
                                            }
                                        }
                                        else if (col.Name.Equals("Customer"))
                                        {
                                            task.Customer = element.Text;
                                        }
                                        else if (col.Name.Equals("URL"))
                                        {
                                            task.Url = element.Text;
                                        }
                                        else if (col.Name.Equals("ExpectedHttpStatus"))
                                        {
                                            task.ExpectedHttpStatus = element.Text;
                                        }
                                    }
                                }

                                if(!String.IsNullOrWhiteSpace(task.Url))
                                {
                                    taskList.Add(task);
                                }
                            }
                        }
                    }
                }
            }

            return taskList;
        }
    }
}
