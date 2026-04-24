using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperTechProject.EntityLayer
{
    public class AdImpression
    {
        public int ImpressionID { get; set; }
        public int CampaignID { get; set; }
        public int PublisherID { get; set; }
        public bool IsClicked { get; set; }
        public decimal CostPerClick { get; set; }
        public string UserCountry { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
