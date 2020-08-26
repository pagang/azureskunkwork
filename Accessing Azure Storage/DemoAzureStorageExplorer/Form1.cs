using AzureUtils;
using AzureUtils.Model;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoAzureStorageExplorer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnReadLog_Click(object sender, EventArgs e)
        {
            AzureStorageManager azStorageMgr = new AzureStorageManager(Environment.GetEnvironmentVariable("STORAGECONNECTIONSTRING"), "BdpFamilySanitizer");
            DesignAutomationRecord[] dars = await azStorageMgr.ReadAutomationRecord("DesignAutomationLog");

            logBox.Items.Clear();
            foreach(DesignAutomationRecord dar in dars)
            {
                logBox.Items.Add($"{dar.WorkItemId}  {dar.InputModelName} { dar.InputSource} {dar.WorkItemStatus}");
            }


        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string baseUrl = "https://bdptestwebapi.azurewebsites.net";
            string path = "/api/AzureDemo/AutomationRecord/";

            //you need a client instance
            RestClient client = new RestClient(baseUrl);

            //use this for each different path
            RestRequest request = new RestRequest(path, Method.GET);

            //this is not really needed here but you can use it to post a body
            request.AddParameter("application/json", null, ParameterType.RequestBody);
            
            //execute a request
            IRestResponse response = await client.ExecuteAsync(request);

            //you can also deserialise into a class (check the docs or ping me)
            dynamic resp = JsonConvert.DeserializeObject(response.Content);

            logBox.Items.Clear();
            foreach (var logItem in resp)
            {
                string workItemId = Convert.ToString(logItem.workItemId);
                string inputModelName = Convert.ToString(logItem.inputModelName);
                string workItemStatus = Convert.ToString(logItem.workItemStatus);
                logBox.Items.Add($"{workItemId} {inputModelName} {workItemStatus}");
            }

        }
    }
}
