using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ExplorerApp
{
    public class File
    {
        public string Name { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public DateTime CreateDate { get; set; }
        public BitmapImage Image { get; set; }
    }
}
