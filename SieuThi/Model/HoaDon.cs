using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SieuThi.Model
{
   public class HoaDon
    {
        public string MaHD { get; set; }
        public List<String> CTHD { get; set; }

        public HoaDon()
        {
            CTHD=new List<string>();
        }
    }
}
