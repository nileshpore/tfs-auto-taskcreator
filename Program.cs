using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TFSTaskCreator
{
    static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new TFSTaskCreator());
            }
            else
            {
                TFSTaskCreator tfs = new TFSTaskCreator();
                tfs.Execute(int.Parse(args[0]),"CMD");
            }

        }        
    }
}
