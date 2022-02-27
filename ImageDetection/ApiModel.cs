using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageDetection
{
    public class ApiModel
    {
        public Resp[] responses { get; set; }
    }

    public class Resp
    {
        public TextAnn[] textAnnotations { get; set; }
    }

    public class TextAnn
    {
        public string description { get; set; }
    }
}
