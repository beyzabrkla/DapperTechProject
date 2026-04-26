using AutoMapper;
using DapperTechProject.DTOLayer.AdImpressionDTOs;
using DapperTechProject.DTOLayer.CampaignDTOs;
using DapperTechProject.DTOLayer.CateogoryDTOs;
using DapperTechProject.DTOLayer.PublisherDTOs;
using DapperTechProject.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperTechProject.BusinessLayer.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<AdImpression, ResultAdImpressionDTO>().ReverseMap();

            CreateMap<Campaign, ResultCampaignDTO>().ReverseMap();
            CreateMap<Campaign, CreateCampaignDTO>().ReverseMap();
            CreateMap<Campaign, UpdateCampaignDTO>().ReverseMap();
            CreateMap<Campaign, GetByIdCampaignDTO>().ReverseMap();
        
            CreateMap<Category, CreateCategoryDTO>().ReverseMap();
            CreateMap<Category, GetByIdCategoryDTO>().ReverseMap();
            CreateMap<Category, ResultCategoryDTO>().ReverseMap();
            CreateMap<Category, UpdateCategoryDTO>().ReverseMap();

            CreateMap<Publisher, CreatePublisherDTO>().ReverseMap();
            CreateMap<Publisher, GetByIdPublisherDTO>()
                            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName)) // Eğer Entity'de CategoryName ise eşle
                            .ReverseMap();
            CreateMap<Publisher, ResultPublisherDTO>().ReverseMap();
            CreateMap<Publisher, UpdatePublisherDTO>().ReverseMap();
        }
    }
}
