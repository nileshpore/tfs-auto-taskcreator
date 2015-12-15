using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TFSTaskCreator
{
   public class MessageHandler: Form
    {
       string mode;
       public MessageHandler(string mode)
       {
           this.mode = mode;           
       }

       public void Show(string message)
       {
           switch (mode)
           {
               case "CMD":
                   Console.WriteLine(message);
                   break;
               case "UI":
                   MessageBox.Show(message);
                   break;
               default:
                   break;
           }
       }

    }
}
