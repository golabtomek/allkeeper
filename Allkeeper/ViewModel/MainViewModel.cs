using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allkeeper.ViewModel
{
    public class MainViewModel 
    {
        public Notes notes { get; set; } = new Notes();
        public Clipboard clipboard { get; set; } = new Clipboard();
        public Window window { get; set; } = new Window();
        
    }
}
