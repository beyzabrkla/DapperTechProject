using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperTechProject.DTOLayer.PublisherDTOs
{
    public class CreatePublisherDTO
    {
        public string WebsiteURL { get; set; }
        public int CategoryID { get; set; }
        public bool Status { get; set; }

    }
}
