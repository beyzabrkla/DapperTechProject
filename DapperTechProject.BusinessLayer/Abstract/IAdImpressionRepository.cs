using DapperTechProject.DTOLayer.AdImpressionDTOs;
using DapperTechProject.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperTechProject.BusinessLayer.Abstract
{
    public interface IAdImpressionRepository
    {
        Task<List<ResultAdImpressionDTO>> GetAdImpressionsWithPagingAsync(int pageNumber, int pageSize, int? regionId, bool? isClicked, int? campaignId, int? publisherId);
        Task<int> GetFilteredCountAsync(int? regionId, bool? isClicked, int? campaignId, int? publisherId);
        Task<int> GetTotalClickCountAsync();
        Task<int> GetTotalAdImpressionCountAsync();
        Task<List<Region>> GetAllRegionsAsync();
        Task<List<Campaign>> GetAllCampaignsAsync();
        Task<List<Publisher>> GetAllPublishersAsync();

    }
}
