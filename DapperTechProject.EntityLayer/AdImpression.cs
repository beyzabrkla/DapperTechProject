namespace DapperTechProject.EntityLayer
{
    public class AdImpression
    {
        public int ImpressionID { get; set; }
        public int CampaignID { get; set; }
        public int PublisherID { get; set; }
        public int RegionID { get; set; }
        public bool IsClicked { get; set; }
        public decimal CostPerClick { get; set; }
        public DateTime Timestamp { get; set; }
    }
}