using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperTechProject.DTOLayer.AdImpressionDTOs
{
    public class ResultAdImpressionDTO
    {
        public int ImpressionID { get; set; }
        public string CampaignName { get; set; } // Campaigns tablosundan gelecek
        public string WebsiteURL { get; set; }   // Publishers tablosundan gelecek
        public string Category { get; set; }     // Publishers tablosundan gelecek
        public bool IsClicked { get; set; }
        public decimal CostPerClick { get; set; }
        public string UserCountry { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
