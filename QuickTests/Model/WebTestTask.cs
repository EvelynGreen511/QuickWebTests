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

        private TestType task;
        private String customer;
        private String url;
        private String expectedHttpStatus;

        public WebTestTask()  {  }

        public WebTestTask(TestType task, string customer, string url, string expectedHttpStatus)
        {
            this.task = task;
            this.customer = customer;
            this.url = url;
            this.expectedHttpStatus = expectedHttpStatus;
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
