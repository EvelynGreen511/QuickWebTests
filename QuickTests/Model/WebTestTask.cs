using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTests
{
    class WebTestTask
    {
        public enum TestType
        {
            AVAILABILTIY
        }

        public WebTestTask()  {  }

        public WebTestTask(TestType task, string customer, string url, string expectedHttpStatus)
        {
            Task = task;
            Customer = customer;
            Url = url;
            ExpectedHttpStatus = expectedHttpStatus;
        }

        public String Customer
        {
            get; set;
        }

        public String Url
        {
            get; set;
        }

        public String ExpectedHttpStatus
        {
            get; set;
        }

        public TestType Task
        {
            get; set;
        }
    }
}
