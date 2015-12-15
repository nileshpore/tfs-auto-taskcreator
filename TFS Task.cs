using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TFSTaskCreator
{
    public partial class TFSTaskCreator : Form
    {
        public TFSTaskCreator()
        {
            InitializeComponent();
        }

        private void buttonCreateTask_Click(object sender, EventArgs e)
        {
            int pbiId = int.Parse(textBoxPBI.Text);
            Execute(pbiId, "UI");
            
        }

        public void Execute(int pbiId, string mode)
        {
            var configurationHandler = (MySection)ConfigurationManager.GetSection("mySection");
            string tfsUrl = ConfigurationManager.AppSettings["tfsUrl"];
            string userName = ConfigurationManager.AppSettings["userName"];
            string password = ConfigurationManager.AppSettings["password"];
            string domain = ConfigurationManager.AppSettings["domain"];
            MessageHandler message = new MessageHandler(mode);

            try
            {
                TFSActionExecutor tfs = new TFSActionExecutor(tfsUrl, userName, password, domain);
                if (!tfs.VerifyAuthentication())
                {
                    message.Show("Authentication failed. Verify you credential.");
                }
                else
                {
                    if (tfs.checkPBIExists(pbiId))
                    {
                        foreach (TaskElement taskElement in configurationHandler.Tasks)
                        {
                            var task = tfs.CreateTask(taskElement, pbiId);
                            tfs.AddLink(task, pbiId);
                        }
                        message.Show("Task created successfully.");
                    }
                    else
                    {
                        message.Show("PBI does not exist. Verify PBI Id or Its type.");
                    }
                }
            }
            catch (Exception ex)
            {
                message.Show("Error Occured: \n" + ex.Message);
            }
        }
    }
}
