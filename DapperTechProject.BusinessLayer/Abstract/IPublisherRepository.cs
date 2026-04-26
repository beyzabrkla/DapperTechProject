using DapperTechProject.DTOLayer.PublisherDTOs;
using DapperTechProject.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperTechProject.BusinessLayer.Abstract
{
    public interface IPublisherRepository
    {
        Task<List<ResultPublisherDTO>> GetPublishersWithCategoriesAsync(int pageNumber, int pageSize, int? categoryId, bool? status);
        Task<int> GetTotalPublisherCountAsync(int? categoryId, bool? status); // Toplam yayıncı sayısını getiren metot
        Task CreatePublisherAsync(CreatePublisherDTO createPublisherDTO); 
        Task UpdatePublisherAsync(UpdatePublisherDTO updatePublisherDTO);
        Task<Publisher> GetByIdPublisherAsync(int id); // ID'ye göre yayıncıyı getiren metot
        Task DeletePublisherAsync(int id);
    }
}
