namespace DapperTechProject.DTOLayer.CampaignDTOs
{
    public class ResultCampaignDTO
    {
        public int CampaignID { get; set; }
        public string CampaignName { get; set; }
        public decimal? Budget { get; set; }
        public string ClientName { get; set; }

        public int RegionID { get; set; }
        public int TypeID { get; set; }

        public string RegionName { get; set; }
        public string TypeName { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}