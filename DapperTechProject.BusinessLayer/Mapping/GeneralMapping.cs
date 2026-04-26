using AutoMapper;
using DapperTechProject.DTOLayer.AdImpressionDTOs;
using DapperTechProject.DTOLayer.CampaignDTOs;
using DapperTechProject.DTOLayer.CampaignTypeDTOs;
using DapperTechProject.DTOLayer.CateogoryDTOs;
using DapperTechProject.DTOLayer.PublisherDTOs;
using DapperTechProject.DTOLayer.RegionDTOs;
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

            CreateMap<Region, ResultRegionDTO>().ReverseMap();

            CreateMap<CampaignType, ResultCampaignTypeDTO>().ReverseMap();

            CreateMap<Category, CreateCategoryDTO>().ReverseMap();
            CreateMap<Category, GetByIdCategoryDTO>().ReverseMap();
            CreateMap<Category, ResultCategoryDTO>().ReverseMap();
            CreateMap<Category, UpdateCategoryDTO>().ReverseMap();

            CreateMap<Publisher, CreatePublisherDTO>().ReverseMap();
            CreateMap<Publisher, GetByIdPublisherDTO>()
                            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName)) 
                            .ReverseMap();
            CreateMap<Publisher, ResultPublisherDTO>().ReverseMap();
            CreateMap<Publisher, UpdatePublisherDTO>().ReverseMap();
        }
    }
}
