using AutoMapper;
using DapperTechProject.BusinessLayer.Abstract;
using DapperTechProject.DTOLayer.PublisherDTOs;
using Microsoft.AspNetCore.Mvc;

namespace DapperTechProject.UI.Controllers
{
    public class PublisherController : Controller
    {
        private readonly IPublisherRepository _publisherRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public PublisherController(IPublisherRepository publisherRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _publisherRepository = publisherRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> PublisherList(int page = 1, int? categoryId = null)
        {
            int pageSize = 12;

            var categories = await _categoryRepository.GetAllCategoriesAsync();
            ViewBag.Categories = categories.ToList();

            int totalCount = await _publisherRepository.GetTotalPublisherCountAsync(categoryId); // Toplam yayıncı sayısını kategoriye göre alıyoruz
            var values = await _publisherRepository.GetPublishersWithCategoriesAsync(page, pageSize, categoryId); // Yayıncıları kategori bilgisiyle birlikte alıyoruz

            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = totalCount;
            ViewBag.SelectedCategory = categoryId;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);

            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> CreatePublisher()
        {
            ViewBag.Categories = (await _categoryRepository.GetAllCategoriesAsync()).ToList();
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
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            ViewBag.Categories = categories.ToList();

            var publisher = await _publisherRepository.GetByIdPublisherAsync(id);

            if (publisher == null) return RedirectToAction("PublisherList");

            var model = _mapper.Map<GetByIdPublisherDTO>(publisher);

            return View(model);
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