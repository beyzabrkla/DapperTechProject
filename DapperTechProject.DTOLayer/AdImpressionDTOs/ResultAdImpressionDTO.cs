namespace DapperTechProject.DTOLayer.AdImpressionDTOs
{
    // DTO Katmanı
    public class ResultAdImpressionDTO
    {
        public int ImpressionID { get; set; }
        public DateTime ImpressionDate { get; set; } 
        public string DeviceType { get; set; }      
        public string OS { get; set; }             
        public string Region { get; set; }        
        public decimal Cost { get; set; }            

        public string CampaignName { get; set; }
        public string WebsiteURL { get; set; }
    }
}