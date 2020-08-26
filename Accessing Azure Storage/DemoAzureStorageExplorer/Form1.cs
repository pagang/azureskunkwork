using AzureUtils;
using AzureUtils.Model;
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
            AzureStorageManager azStorageMgr = new AzureStorageManager("<YOUR CONN STRING FROM AZURE>", "BdpFamilySanitizer");
            DesignAutomationRecord[] dars = await azStorageMgr.ReadAutomationRecord("DesignAutomationLog");

            logBox.Items.Clear();
            foreach(DesignAutomationRecord dar in dars)
            {
                logBox.Items.Add($"{dar.WorkItemId}  {dar.InputModelName} { dar.InputSource} {dar.WorkItemStatus}");
            }


        }
    }
}
