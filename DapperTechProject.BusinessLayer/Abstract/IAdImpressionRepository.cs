using DapperTechProject.DTOLayer.AdImpressionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperTechProject.BusinessLayer.Abstract
{
    public interface IAdImpressionRepository
    {
        Task<List<ResultAdImpressionDTO>> GetAdImpressionsWithPagingAsync(int pageNumber, int pageSize);
        Task<int> GetTotalClickCountAsync();
    }
}
