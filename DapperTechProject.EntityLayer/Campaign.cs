using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperTechProject.EntityLayer
{
    public class Campaign
    {
        public int CampaignID { get; set; }
        public string CampaignName { get; set; }
        public decimal? Budget { get; set; }
        public string ClientName { get; set; }
        public int RegionID { get; set; }
        public int TypeID { get; set; }  
        public DateTime CreatedDate { get; set; }
    }
}
