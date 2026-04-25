using DapperTechProject.BusinessLayer.Abstract;
using DapperTechProject.DTOLayer.PublisherDTOs;
using Microsoft.AspNetCore.Mvc;

namespace DapperTechProject.UI.Controllers
{
    public class PublisherController : Controller
    {
        private readonly IPublisherRepository _publisherRepository;

        public PublisherController(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        public async Task<IActionResult> PublisherList()
        {
            var publishers = await _publisherRepository.GetAllPublisherAsync();
            var model = publishers.Select(x => new ResultPublisherDTO
            {
                PublisherID = x.PublisherID,
                WebsiteURL = x.WebsiteURL,
                CategoryID = x.CategoryID,
                // Eğer CategoryName'i başka bir tablodan (Join ile) çekiyorsan 
                // burada o değeri de atamalısın.
            }).ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult CreatePublisher()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePublisher(CreatePublisherDTO createPublisherDTO)
        {
            await _publisherRepository.CreatePublisherAsync(createPublisherDTO);
            return RedirectToAction("PublisherList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePublisher(int id)
        {
            var publisher = await _publisherRepository.GetByIdPublisherAsync(id);
            return View(publisher);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePublisher(UpdatePublisherDTO updatePublisherDTO)
        {
            await _publisherRepository.UpdatePublisherAsync(updatePublisherDTO);
            return RedirectToAction("PublisherList");
        }

        public async Task<IActionResult> DeletePublisher(int id)
        {
            await _publisherRepository.DeletePublisherAsync(id);
            return RedirectToAction("PublisherList");
        }
    }
}
