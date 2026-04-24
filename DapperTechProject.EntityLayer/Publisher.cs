using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperTechProject.EntityLayer
{
    public class Publisher
    {
        public int PublisherID { get; set; }
        public string WebsiteURL { get; set; }
        public string Category { get; set; }
        public int CategoryID { get; set; }
    }
}
