using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperTechProject.DTOLayer.PublisherDTOs
{
    public class GetByIdPublisherDTO
    {
        public int PublisherID { get; set; }
        public string WebsiteURL { get; set; }
        public string CategoryName { get; set; }
        public int CategoryID { get; set; }
        public bool Status { get; set; }

    }
}
