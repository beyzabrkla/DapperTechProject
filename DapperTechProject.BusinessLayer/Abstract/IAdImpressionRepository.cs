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
        Task<List<ResultAdImpressionDTO>> GetAdImpressionsWithPagingAsync(int pageNumber, int pageSize, int? regionId, bool? isClicked, int? campaignId, int? publisherId); // Sayfalama ve Filtreleme ile Reklam Gösterimlerini Getir
        Task<int> GetFilteredCountAsync(int? regionId, bool? isClicked, int? campaignId, int? publisherId); // Filtrelenmiş Kayıt Sayısını Getir
        Task<int> GetTotalClickCountAsync(); //Toplam Tıklama Sayısını Getir
        Task<int> GetTotalAdImpressionCountAsync(); // Toplam Gösterim Sayısını Getir
        Task<List<Region>> GetAllRegionsAsync(); // Tüm Bölgeleri Getir
        Task<List<Campaign>> GetAllCampaignsAsync(); // Tüm Kampanyaları Getir
        Task<List<Publisher>> GetAllPublishersAsync(); // Tüm Yayıncıları Getir
        Task<List<dynamic>> GetRegionPerformanceAsync(); // Bölgelere Göre Performans Verisi
        Task<List<dynamic>> GetTopSpendingCampaignsAsync(); // En Çok Harcama Yapan Kampanyalar
        Task<List<dynamic>> GetTopEfficiencyPublishersAsync(); // En Verimli Yayıncılar (Tıklama başına en düşük maliyet getirenler)
        Task<List<dynamic>> GetCategoryDistributionAsync(); //Sektörel Dağılım: Kategori bazlı toplam gösterim sayıları
        Task<decimal> GetGlobalAvgCPCAsync(); //Global Ortalama Tıklama Maliyeti (Global Avg CPC)
    }
}
