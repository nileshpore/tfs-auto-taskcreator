using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TFSTaskCreator
{
    public class TFSActionExecutor
    {
        private string tfsUrl;
        private string userName;
        private string password;
        private string domain;
        private TfsTeamProjectCollection tfs;

        public TFSActionExecutor(string tfsUrl, string userName, string password, string domain)
        {
            this.tfsUrl = tfsUrl;
            this.userName = userName;
            this.password = password;
            this.domain = domain;

            Uri tfsUri = new Uri(tfsUrl);
            NetworkCredential cred = new NetworkCredential(userName, password, domain);
            tfs = new TfsTeamProjectCollection(tfsUri, cred);
        }

        public bool VerifyAuthentication()
        {
            try
            {
                tfs.EnsureAuthenticated();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public WorkItem CreateTask(TaskElement taskElement, int pbiId)
        {       
            var workItemStore = tfs.GetService<WorkItemStore>();
            var backlogItem = workItemStore.GetWorkItem(pbiId);
            var project = workItemStore.Projects[taskElement.Project];
            var taskType = project.WorkItemTypes["Task"];
            var task = new WorkItem(taskType);
            task.Title = backlogItem.Id.ToString() + ": " + taskElement.Title;
            task.State = taskElement.State;
            task.AreaPath = taskElement.AreaPath;
            task.IterationPath = backlogItem.IterationPath;
            task.Fields["Remaining Work"].Value = taskElement.RemainingWork;
            task.Fields["Assigned To"].Value = backlogItem.Fields["Assigned To"].Value;
            task.Fields["Activity"].Value = taskElement.Activity;
            task.Fields["Backlog Priority"].Value = backlogItem.Fields["Backlog Priority"].Value;

            if (taskElement.Project.Equals("ESM3"))
            {
                task.Fields["Team"].Value = taskElement.Team;
            }
            else if (taskElement.Project.Equals("ESM2"))
            {
                task.Fields["Origin"].Value = backlogItem.Fields["Origin"].Value;                                                                   
            }

            task.Validate();
            task.Save();
            return task;
        }

        public void AddLink(WorkItem task, int pbiId)
        {
            var workItemStore = tfs.GetService<WorkItemStore>();
            var backlogItem = workItemStore.GetWorkItem(pbiId);
            var linkType = workItemStore.WorkItemLinkTypes["System.LinkTypes.Hierarchy"];
            var taskLink = new WorkItemLink(linkType.ReverseEnd, backlogItem.Id);
            task.Links.Add(taskLink);
            task.Save();
            backlogItem.Save();
        }

        public bool checkPBIExists(int pbiId)
        {
            try
            {
                var workItemStore = tfs.GetService<WorkItemStore>();
                var backlogItem = workItemStore.GetWorkItem(pbiId);
                backlogItem.Validate();

                int priority = 0;
                int maxPriorityAllowed = int.Parse(ConfigurationManager.AppSettings["maxPriorityAllowed"]);
                if (!string.IsNullOrWhiteSpace(backlogItem.Fields["Backlog Priority"].Value.ToString()))
                    int.TryParse(backlogItem.Fields["Backlog Priority"].Value.ToString(), out priority);


                if (backlogItem.Type.Name.Equals("Product Backlog Item") && priority < maxPriorityAllowed)
                    return true;
                else
                    return false;
            }
            catch 
            {

                return false;
            }
            
        }
    }
}
