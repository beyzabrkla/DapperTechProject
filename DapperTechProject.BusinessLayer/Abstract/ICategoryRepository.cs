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
        Task<List<ResultCategoryDTO>> GetAllCategoriesAsync();

        Task CreateCategoryAsync(CreateCategoryDTO createCategoryDTO);

        Task UpdateCategoryAsync(UpdateCategoryDTO updateCategoryDTO);

        Task DeleteCategoryAsync(int id);

        Task<GetByIdCategoryDTO> GetCategoryByIdAsync(int id);
    }
}
