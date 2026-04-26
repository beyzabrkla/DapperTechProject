using DapperTechProject.DTOLayer.CateogoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperTechProject.BusinessLayer.Abstract
{
    public interface ICategoryRepository
    {
        Task<List<ResultCategoryDTO>> GetCategoriesPagedAsync(int page, int pageSize, bool? status, string search = null);
        Task<int> GetTotalCategoryCountAsync(bool? status);
        Task CreateCategoryAsync(CreateCategoryDTO createCategoryDTO);
        Task<GetByIdCategoryDTO> GetCategoryByIdAsync(int id);
        Task UpdateCategoryAsync(UpdateCategoryDTO updateCategoryDTO);
        Task DeleteCategoryAsync(int id);
        Task<string> GetMostPopularCategoryNameAsync(); //En çok kullanılan kategorinin adını döndürüyor

    }
}
