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
        Task<List<Publisher>> GetAllPublisherAsync();
        Task<Publisher> GetByIdPublisherAsync(int id);
        Task CreatePublisherAsync(CreatePublisherDTO createPublisherDTO);
        Task UpdatePublisherAsync(UpdatePublisherDTO updatePublisherDTO);
        Task DeletePublisherAsync(int id);
    }
}
