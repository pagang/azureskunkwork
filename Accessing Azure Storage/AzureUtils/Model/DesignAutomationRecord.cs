using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureUtils.Model
{
    public class DesignAutomationRecord : TableEntity
    {

        public DesignAutomationRecord()
        {
        }

        public string WorkItemId { get; set; }
        public string ParentActivity { get; set; }
        public string WorkItemStatus { get; set; }
        public DateTime TimeQueued { get; set; }
        public string InputSource { get; set; }
        public string InputModelName { get; set; }
        public string RuleSetName { get; set; }
    }
}
