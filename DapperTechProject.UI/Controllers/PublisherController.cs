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

        public async Task<IActionResult> PublisherList(int page = 1, int? categoryId = null, bool? status = null)
        {
            int pageSize = 12;

            // Filtre dropdownları için tüm kategorileri getiriyoruz
            ViewBag.Categories = await _categoryRepository.GetCategoriesPagedAsync(1, 0, true);

            var values = await _publisherRepository.GetPublishersWithCategoriesAsync(page, pageSize, categoryId, status); //filtrelenmiş ve sayfalama yapılmış yayıncıları getiriyoruz


            //İstatistikler için toplam kayıt sayısını getiriyoruz
            int totalCount = await _publisherRepository.GetTotalPublisherCountAsync(categoryId, status); //tablonun altındaki yer için toplam * kayıt bulundu sayısını getiriyoruz seçilen parametrelere göre

            ViewBag.GrandTotal = await _publisherRepository.GetTotalPublisherCountAsync(null, null); // Sistemdeki tüm yayıncılar

            ViewBag.ActiveCount = await _publisherRepository.GetTotalPublisherCountAsync(null, true); //sistemdeki aktif yayıncılar

            //Viewbag ile sayfaya gönderiyoruz
            ViewBag.TotalCount = totalCount;
            ViewBag.SelectedCategory = categoryId;
            ViewBag.CurrentStatus = status;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);

            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> CreatePublisher()
        {
            ViewBag.Categories = await _categoryRepository.GetCategoriesPagedAsync(1, 0, true); //1. sayfadan başlayarak tüm aktif kategorileri getiriyoruz
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
            ViewBag.Categories = await _categoryRepository.GetCategoriesPagedAsync(1, 0, true);

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