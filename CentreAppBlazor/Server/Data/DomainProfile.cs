using AutoMapper;
using CentreAppBlazor.Shared.Dto;
using CentreAppBlazor.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentreAppBlazor.Server.Data
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<SalesTotalByDayView, SalesReturnsbyDayDto>();
            CreateMap<ReturnTotalByDayView, SalesReturnsbyDayDto>();
        }
    }
   
}