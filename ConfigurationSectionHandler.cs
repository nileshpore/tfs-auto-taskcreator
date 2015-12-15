using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSTaskCreator
{

    public class MySection : ConfigurationSection
    {
        [ConfigurationProperty("Tasks", Options = ConfigurationPropertyOptions.IsRequired)]
        public TasksCollection Tasks
        {
            get
            {
                return (TasksCollection)this["Tasks"];
            }
        }
    }

    [ConfigurationCollection(typeof(TaskElement), AddItemName = "Task")]
    public class TasksCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new TaskElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return ((TaskElement)element).Title;
        }
    }

    public class TaskElement : ConfigurationElement
    {
        private const string ProjectKey = "project";
        private const string TitleKey = "title";
        private const string ActivityKey = "activity";
        private const string StateKey = "state";
        private const string AreaPathKey = "areaPath";
        private const string TeamKey = "team";
        private const string RemainingWorkKey = "remainingWork";        
        

        [ConfigurationProperty(ProjectKey, IsKey = true)]
        public string Project
        {
            get
            {
                return (string)base[ProjectKey];
            }
        }

        [ConfigurationProperty(TitleKey, IsRequired = true)]
        public string Title
        {
            get
            {
                return (string)base[TitleKey];
            }
        }

        [ConfigurationProperty(ActivityKey, IsRequired = true)]
        public string Activity
        {
            get
            {
                return (string)base[ActivityKey];
            }
        }

        [ConfigurationProperty(StateKey, IsRequired = true)]
        public string State
        {
            get
            {
                return (string)base[StateKey];
            }
        }

        [ConfigurationProperty(AreaPathKey, IsRequired = true)]
        public string AreaPath
        {
            get
            {
                return (string)base[AreaPathKey];
            }
        }

        [ConfigurationProperty(TeamKey, IsRequired = true)]
        public string Team
        {
            get
            {
                return (string)base[TeamKey];
            }
        }

        [ConfigurationProperty(RemainingWorkKey, IsRequired = true)]
        public string RemainingWork
        {
            get
            {
                return (string)base[RemainingWorkKey];
            }
        }
    }
}
