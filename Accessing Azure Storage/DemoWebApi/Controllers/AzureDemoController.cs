using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureUtils.Model;
using AzureUtils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using DemoWebApi.Controllers.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoWebApi.Controllers
{
    [Route("api/[controller]/AutomationRecord")]
    [ApiController]
    public class AzureDemoController : ControllerBase
    {
        private static readonly string _logTable = "DesignAutomationLog";

        // GET: api/<AzureDemoController>
        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            AzureStorageManager azStorageMgr = new AzureStorageManager(Globals.GetAppSetting("STORAGECONNECTIONSTRING"), "BdpFamilySanitizer");
            DesignAutomationRecord[] dar = await azStorageMgr.ReadAutomationRecord(_logTable);
            return Ok(dar);
        }

        // GET api/<AzureDemoController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            AzureStorageManager azStorageMgr = new AzureStorageManager(Globals.GetAppSetting("STORAGECONNECTIONSTRING"), "BdpFamilySanitizer");
            DesignAutomationRecord[] dar = await azStorageMgr.ReadAutomationRecord(_logTable, id);
            return Ok(dar);
        }

        // POST add an entry to the log
        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] DesignAutomationRecord dar)
        {
            //Persist the activity in Azure Table Storage                 
            Console.WriteLine("Saving record to Azure " + dar);

            //set the queueing time
            dar.TimeQueued = DateTime.UtcNow;

            //set a unique id
            dar.RowKey = Guid.NewGuid().ToString();

            //log the operation
            AzureStorageManager azStorageMgr = new AzureStorageManager(Globals.GetAppSetting("STORAGECONNECTIONSTRING"), "BdpFamilySanitizer"); 
            _ = await azStorageMgr.SaveAutomationRecord(_logTable, dar);

            return Ok();
        }

        // PUT update workitems log
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] string status)
        {
            AzureStorageManager azStorageMgr = new AzureStorageManager(Globals.GetAppSetting("STORAGECONNECTIONSTRING"), "BdpFamilySanitizer");
            DesignAutomationRecord[] dar = await azStorageMgr.ReadAutomationRecord(_logTable, id);
            IActionResult res = await azStorageMgr.UpdateLoggedWorkItemStatus(status, dar.First<DesignAutomationRecord>(), _logTable);
            return res;
        }
    }
}
