namespace DapperTechProject.DTOLayer.AdImpressionDTOs
{
    public class ResultAdImpressionDTO
    {
        public int ImpressionID { get; set; }
        public bool IsClicked { get; set; }
        public decimal CostPerClick { get; set; }
        public DateTime Timestamp { get; set; }

        public string CampaignName { get; set; }
        public string PublisherName { get; set; }
        public string RegionName { get; set; }
    }
}