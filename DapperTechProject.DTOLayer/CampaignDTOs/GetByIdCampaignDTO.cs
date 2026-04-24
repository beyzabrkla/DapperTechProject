using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperTechProject.DTOLayer.CampaignDTOs
{
    public class GetByIdCampaignDTO
    {
        public int CampaignID { get; set; }
        public string CampaignName { get; set; }
        public decimal? Budget { get; set; } // Görselde NULL göründüğü için nullable yaptık
        public string ClientName { get; set; }
    }
}
